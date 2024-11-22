using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Application.Features.Queries.Question.GetQuestions;

namespace QuizAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var request = new GetQuestionsQueryRequest();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
