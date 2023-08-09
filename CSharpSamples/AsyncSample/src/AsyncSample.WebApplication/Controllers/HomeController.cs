using AsyncSample.WebApplication.Models;
using AsyncSample.WebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace AsyncSample.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<NumberRange> _numberRanges = new List<NumberRange>()
        {
            new NumberRange(FromNumber: 1, ToNumber: 5_000_000),
            new NumberRange(FromNumber: 5_000_000, ToNumber: 10_000_000),
            new NumberRange(FromNumber: 10_000_000, ToNumber: 20_000_000),
            new NumberRange(FromNumber: 20_000_000, ToNumber: 30_000_000),
            new NumberRange(FromNumber: 30_000_000, ToNumber: 40_000_000),
        };

        public HomeController(ILogger<HomeController> logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NormalSync()
        {
            StringBuilder result = new StringBuilder();

            Stopwatch sw = Stopwatch.StartNew();
            foreach (NumberRange range in _numberRanges)
            {
                result.AppendLine(MathService.GetPrimesNumbers(range.FromNumber, range.ToNumber));
            }
            sw.Stop();

            result.AppendLine($"Elapsed Time: {sw.Elapsed}");
            ViewBag.Result = result.ToString().Replace("\r\n", "<br>");

            return View("Index");
        }


        public async Task<IActionResult> NormalAsync()
        {
            StringBuilder result = new StringBuilder();

            Stopwatch sw = Stopwatch.StartNew();
            foreach (NumberRange range in _numberRanges)
            {
                // We can use Task.Run(() => NonAsyncMethod()) for nonAsyncMethod
                // If the method is Async we don't need to use Task.Run()
                result.AppendLine(await Task.Run(() => MathService.GetPrimesNumbers(range.FromNumber, range.ToNumber)));
            }
            sw.Stop();

            result.AppendLine($"Elapsed Time: {sw.Elapsed}");
            ViewBag.Result = result.ToString().Replace("\r\n", "<br>");

            return View("Index");
        }

        public async Task<IActionResult> ParallelAsync()
        {
            StringBuilder result = new StringBuilder();
            List<Task<string>> tasks = new List<Task<string>>();

            Stopwatch sw = Stopwatch.StartNew();
            foreach (NumberRange range in _numberRanges)
            {
                // Running all tasks in parallel
                tasks.Add(Task.Run(() => MathService.GetPrimesNumbers(range.FromNumber, range.ToNumber)));
            }

            // Waiting for all tasks to be finished.
            var taskResults = await Task.WhenAll(tasks);

            foreach (string task in taskResults)
            {
                result.AppendLine(task);
            }

            sw.Stop();

            result.AppendLine($"Elapsed Time: {sw.Elapsed}");
            ViewBag.Result = result.ToString().Replace("\r\n", "<br>");

            return View("Index");
        }
    }
}