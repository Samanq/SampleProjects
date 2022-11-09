using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc;

namespace AngleSharpSample.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private const string html = @"
                                    <!DOCTYPE html>
<html>
<title>Online HTML Editor</title>

<head>
    <title>Test Page</title>
</head>

<body>
    <style>
        .student-card {
            background-color: #c9c9c9;
        }
    </style>
    <h1>Students</h1>
    <div>
        <div class=""student-card"">
            <p>John</p>
            <p>18</p>
        </div>
        <div class=""student-card"">
            <p>Jane</p>
            <p>20</p>
        </div>
        <div class=""student-card"">
            <p>Peter</p>
            <p>25</p>
        </div>
    </div>
</body>

</html>";

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Configuring the Browing context.
            using var context = BrowsingContext.New(Configuration.Default);
            // Loading the document
            using var doc = await context.OpenAsync(req => req.Content(html));

            // Get elements by tag
            var liElements = doc.QuerySelectorAll("div");
            var allDivs = doc.GetElementsByTagName("div");

            // Get elements by calss name
            var studentCards = doc.GetElementsByClassName("student-card");

            // Get Elements by css selector
            var cards = doc.QuerySelector("div.student-card");

            // TODO Read all students and return them
            return NoContent();
        }
    }
}
