using AutofacSample.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutofacSample.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailsController : ControllerBase
{
    private readonly IEmailService _emailService;

    // Injecting IEmailService
    public EmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult GetEmail()
    {
        return Ok(_emailService.ReadEmail());
    }

    [HttpPost]
    public IActionResult Post(string text)
    {
        return Ok(_emailService.SendEmail(text));
    }
}
