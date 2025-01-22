using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.ChartWebApp.Models;

namespace SEPDotNetCore.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            ApexChartPieChartModel model = new ApexChartPieChartModel();
            model.Series = new int[] { 44, 55, 13, 43, 22 };
            model.Labels = new string[] { "Team A", "Team B", "Team C", "Team D", "Team E" };
            return View(model);
        }

        public IActionResult MixedChart()
        {
            var model = new ApexChartMixedChartModel
            {
                Series = new List<SeriesData>
            {
                new SeriesData
                {
                    Name = "TEAM A",
                    Type = "column",
                    Data = new int[] { 23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30 }
                },
                new SeriesData
                {
                    Name = "TEAM B",
                    Type = "area",
                    Data = new int[] { 44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43 }
                },
                new SeriesData
                {
                    Name = "TEAM C",
                    Type = "line",
                    Data = new int[] { 30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39 }
                }
            },
                Labels = new string[] { "01/01/2003", "02/01/2003", "03/01/2003", "04/01/2003", "05/01/2003", "06/01/2003", "07/01/2003", "08/01/2003", "09/01/2003", "10/01/2003", "11/01/2003" }
            };

            return View(model);
        }

        public IActionResult DonutChart()
        {         
            var model = new DonutChartModel
            {
                Labels = new List<string> { "Norway", "Germany", "USA", "Sweden", "Netherlands", "ROC", "Austria", "Canada", "Japan" },
                Series = new List<int> { 16, 12, 8, 8, 8, 6, 7, 4, 3 }
            };

           
            return View(model);
        }

        public IActionResult ColumnChart()
        {
         
            var model = new ColumnChartModel
            {
                Brands = new List<string> { "Toyota", "Volkswagen", "Volvo", "Tesla", "Hyundai", "MG", "Skoda", "BMW", "Ford", "Nissan" },
                Sales = new List<int> { 1795, 1242, 1074, 832, 593, 509, 471, 442, 385, 371 }
            };
            return View(model);
        }

        public IActionResult BorderChart()
        {
          
            var model = new BorderChartModel
            {
                Labels = new List<string> { "January", "February", "March", "April", "May", "June", "July" },
                Dataset1 = new List<int> { 65, 59, 80, 81, 56, 55, 40 }, 
                Dataset2 = new List<int> { 28, 48, 40, 19, 86, 27, 90 }  
            };

            return View(model);
        }

        public IActionResult RadarChart()
        {
           
            var model = new RadarChartModel
            {
                Labels = new List<string> { "Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running" },
                Dataset1 = new List<int> { 65, 59, 90, 81, 56, 55, 40 },  
                Dataset2 = new List<int> { 28, 48, 40, 19, 96, 27, 100 }  
            };

            return View(model);
        }

        public IActionResult BoxWhiskerChart()
        {
         
            var model = new BoxWhiskerChartModel
            {
                Labels = new List<string> { "Registered Nurse", "Web Developer", "System Analyst", "Application Engineer", "Aerospace Engineer", "Dentist" },
                DataPoints = new List<List<int>>
            {
                new List<int> { 46360, 55320, 82490, 101650, 71000 },
                new List<int> { 83133, 91830, 115828, 128982, 101381 },
                new List<int> { 51910, 60143, 115056, 135450, 85800 },
                new List<int> { 63364, 71653, 91120, 100556, 80757 },
                new List<int> { 82725, 94361, 118683, 129191, 107142 },
                new List<int> { 116777, 131082, 171679, 194336, 146794 }
            }
            };

          
            return View(model);
        }

        public IActionResult LineChart()
        {
          
            var model = new LineChartModel
            {
                Dates = new List<DateTime>
            {
                new DateTime(2016, 1, 1),
                new DateTime(2016, 2, 1),
                new DateTime(2016, 3, 1),
                new DateTime(2016, 4, 1),
                new DateTime(2016, 5, 1),
                new DateTime(2016, 6, 1),
                new DateTime(2016, 7, 1),
                new DateTime(2016, 8, 1),
                new DateTime(2016, 9, 1),
                new DateTime(2016, 10, 1),
                new DateTime(2016, 11, 1),
                new DateTime(2016, 12, 1)
            },
                Prices = new List<double> { 61, 71, 55, 50, 65, 85, 68, 28, 34, 24, 44, 34 },
                IndexLabels = new List<string> { "gain", "gain", "loss", "loss", "gain", "gain", "loss", "loss", "gain", "loss", "gain", "loss" },
                MarkerTypes = new List<string> { "triangle", "triangle", "cross", "cross", "triangle", "triangle", "cross", "cross", "triangle", "cross", "triangle", "cross" },
                MarkerColors = new List<string> { "#6B8E23", "#6B8E23", "tomato", "tomato", "#6B8E23", "#6B8E23", "tomato", "tomato", "#6B8E23", "tomato", "#6B8E23", "tomato" }
            };

           
            return View(model);
        }
    }       
}
