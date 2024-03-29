﻿using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            //Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            //var (statusCode, message) = exception switch
            //{
            //    IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            //    _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
            //};

            //return Problem(statusCode: statusCode, detail: message);
            return Problem();
        }
    }
}
