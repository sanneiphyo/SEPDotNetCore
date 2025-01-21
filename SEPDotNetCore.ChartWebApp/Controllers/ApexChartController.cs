using Microsoft.AspNetCore.Mvc;

namespace SEPDotNetCore.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
