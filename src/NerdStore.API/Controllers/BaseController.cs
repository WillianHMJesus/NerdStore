using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private List<string> GetModelStateError()
        {
            var messages = new List<string>();
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            foreach (var erro in errors)
            {
                var errorMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                messages.Add(errorMessage);
            }

            return messages;
        }

        private List<string> GetExceptionError(Exception exception)
        {
            var errorMessage = string.IsNullOrEmpty(exception?.Message) ? 
                exception?.InnerException?.Message : 
                exception.Message;

            return new List<string> { errorMessage };
        }

        protected IActionResult ResponseModelStateError()
        {
            return BadRequest(new { Errors = GetModelStateError() });
        }

        protected IActionResult ResponseException(Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case nameof(DomainException):
                    return BadRequest(new { Errors = GetExceptionError(exception) });
                default:
                    return StatusCode(500, exception);
            }
        }
    }
}
