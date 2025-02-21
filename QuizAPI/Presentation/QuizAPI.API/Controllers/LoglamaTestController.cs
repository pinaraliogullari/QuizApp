using Microsoft.AspNetCore.Mvc;
using System;

namespace QuizAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorTestController : ControllerBase
    {
        [HttpGet]
        [Route("throw-error")]
        public async Task<IActionResult> ThrowError()
        {
            
            throw new InvalidOperationException("Test hata mesajı - Hata fırlatıldı.");
        }

       
        [HttpGet]
        [Route("success")]
        public async Task<IActionResult> Success()
        {
            return Ok("Başarılı işlem.");
        }
    }
}
