using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WorkFlow_SIG10._1.Models
{
    public class ModuleInfo { public string Titulo { get; set; } = string.Empty; public string Url { get; set; } = string.Empty; public string Icon { get; set; } = string.Empty; }
    public class KpiInfo { public string Value { get; set; } = string.Empty; public string Title { get; set; } = string.Empty; public string Icon { get; set; } = string.Empty; public string Color { get; set; } = string.Empty; }
    public class ActivityItem { public string Icon { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string Time { get; set; } = string.Empty; }

    public class ChartJsData
    {
        [JsonPropertyName("labels")]
        public List<string> Labels { get; set; } = new();
        [JsonPropertyName("datasets")]
        public List<ChartJsDataset> Datasets { get; set; } = new();
    }

    public class ChartJsDataset
    {
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<double?> Data { get; set; } = new(); // Using double? to allow nulls
        [JsonPropertyName("backgroundColor")]
        public List<string> BackgroundColor { get; set; } = new();
        [JsonPropertyName("borderColor")]
        public List<string> BorderColor { get; set; } = new();
        [JsonPropertyName("borderWidth")]
        public int? BorderWidth { get; set; }
        [JsonPropertyName("fill")]
        public bool? Fill { get; set; }
        [JsonPropertyName("tension")]
        public double? Tension { get; set; }
        [JsonPropertyName("pointRadius")]
        public int? PointRadius { get; set; }
        [JsonPropertyName("pointBackgroundColor")]
        public List<string> PointBackgroundColor { get; set; } = new();
        [JsonPropertyName("pointBorderColor")]
        public List<string> PointBorderColor { get; set; } = new();
    }
}
