namespace SEPDotNetCore.ChartWebApp.Models
{
    public class ApexChartPieChartModel
    {
        public int[] Series { get; set; }
        public string[] Labels { get; set; }
    }

    public class ApexChartMixedChartModel
    {
        public List<SeriesData> Series { get; set; }
        public string[] Labels { get; set; }
    }

    public class SeriesData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int[] Data { get; set; }
    }

    public class DonutChartModel
    {
        public List<string> Labels { get; set; }
        public List<int> Series { get; set; }
    }

    public class ColumnChartModel
    {
        public List<string> Brands { get; set; }
        public List<int> Sales { get; set; }
    }
    public class BorderChartModel
    {
        public List<string> Labels { get; set; }
        public List<int> Dataset1 { get; set; }
        public List<int> Dataset2 { get; set; }
    }

    public class RadarChartModel
    {
        public List<string> Labels { get; set; }
        public List<int> Dataset1 { get; set; }
        public List<int> Dataset2 { get; set; }
    }

    public class BoxWhiskerChartModel
    {
        public List<string> Labels { get; set; }
        public List<List<int>> DataPoints { get; set; }
    }

    public class LineChartModel
    {
        public List<DateTime> Dates { get; set; } 
        public List<double> Prices { get; set; }  
        public List<string> IndexLabels { get; set; } 
        public List<string> MarkerTypes { get; set; }  
        public List<string> MarkerColors { get; set; } 
    }
}