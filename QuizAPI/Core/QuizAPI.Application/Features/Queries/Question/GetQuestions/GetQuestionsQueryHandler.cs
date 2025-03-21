using MediatR;
using Microsoft.Extensions.Logging;
using QuizAPI.Application.Repositories;
using QuizAPI.Application.Services.Redis;

namespace QuizAPI.Application.Features.Queries.Question.GetQuestions;

public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQueryRequest, List<GetQuestionsQueryResponse>>
{
    private readonly IQuestionReadRepository _questionReadRepository;
    private readonly ILogger<GetQuestionsQueryHandler> _logger;
    private readonly ICacheService _cacheService;

    public GetQuestionsQueryHandler(IQuestionReadRepository questionReadRepository, ILogger<GetQuestionsQueryHandler> logger, ICacheService cacheService)
    {
        _questionReadRepository = questionReadRepository;
        _logger = logger;
        _cacheService = cacheService;
    }
    //Temelde yapılan şey şu: bir cachekey belirlendi.(Veriler rediste key-value şeklinde saklanır.)
    //Daha sonra cache serviste bu key var mı diye kontrol edildi.
    // Eğer veri varsa getirildi ve logInfo ile bilgi verildi.
    //Key yoksa veri dbden getiriliyor ve belirlenen cacheKey ile cacheleniyor.
    //Sonraki isteklerde veri dbden değil cacheten geliyor olacak.

    public async Task<List<GetQuestionsQueryResponse>> Handle(GetQuestionsQueryRequest request, CancellationToken cancellationToken)
    {
        var cacheKey= "RandomQuestions";
        var cachedQuestions = await _cacheService.GetAsync<List<GetQuestionsQueryResponse>>(cacheKey);
        if(cachedQuestions!=null && cachedQuestions.Any())
        {
            _logger.LogInformation("Cacheten veriler getirildi");
            return cachedQuestions;
        }

        var questions = await _questionReadRepository.GetAllAsync(options: null, include: null, tracking: false);
        if (questions == null || !questions.Any())
            throw new InvalidOperationException("No questions available to retrieve.");

        var random5qns = questions
            .OrderBy(x => Guid.NewGuid())
            .Take(5)
            .Select(x => new GetQuestionsQueryResponse(
                x.Id,
                x.InWords,
                x.ImageName,
                new List<string>
                {
                    x.Option1,
                    x.Option2,
                    x.Option3,
                    x.Option4
                }
            ))
            .ToList();
        await _cacheService.SetAsync(cacheKey, random5qns, TimeSpan.FromMinutes(10),TimeSpan.FromMinutes(5));
        _logger.LogInformation("DBden veriler getirildi ve cachelendi");
        return random5qns;
       
    }
  
 
}
    



