﻿using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using QuizAPI.Application.Repositories;

namespace QuizAPI.Application.Features.Queries.Question.GetQuestions;

public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQueryRequest, List<GetQuestionsQueryResponse>>
{
    private readonly IQuestionReadRepository _questionReadRepository;
    private readonly ILogger<GetQuestionsQueryHandler> _logger;

    public GetQuestionsQueryHandler(IQuestionReadRepository questionReadRepository, ILogger<GetQuestionsQueryHandler> logger)
    {
        _questionReadRepository = questionReadRepository;
        _logger = logger;
    }

    public async Task<List<GetQuestionsQueryResponse>> Handle(GetQuestionsQueryRequest request, CancellationToken cancellationToken)
    {
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
        return random5qns;
       
    }
  
 
}
    



