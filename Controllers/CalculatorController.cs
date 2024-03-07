using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestAtlas.Services;
using TestAtlas.ViewModels;

namespace TestAtlas.Controllers
{
    public class CalculatorController : Controller
    {
        private ICalculator<MonthlyCalculatorViewModel> _monthlyCalculator;
        private ICalculator<DailyCalculatorViewModel> _dailyCalculator;

        public CalculatorController(ICalculator<MonthlyCalculatorViewModel> monthlyCalculator,
        ICalculator<DailyCalculatorViewModel> dailyCalculator)
        {
            _dailyCalculator = dailyCalculator;
            _monthlyCalculator = monthlyCalculator;
        }

        [HttpPost]
        public async Task<IActionResult> CalculateMonthly(MonthlyCalculatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultModel = await _monthlyCalculator.Calculate(model);
                return View("Result", resultModel);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CalculateDaily(DailyCalculatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultModel = await _dailyCalculator.Calculate(model);
                return View("Result", resultModel);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }
            return View("Index");
        }

        [Route("Calculate/Monthly")]
        [HttpGet]
        public IActionResult Monthly()
        {
            return View();
        }

        [Route("Calculate/Daily")]
        [HttpGet]
        public IActionResult Daily()
        {
            return View();
        }
    }
}
