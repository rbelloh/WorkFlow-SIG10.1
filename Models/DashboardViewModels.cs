using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WorkFlow_SIG10._1.Models
{
    public class ModuleInfo { public string Titulo { get; set; } public string Url { get; set; } public string Icon { get; set; } }
    public class KpiInfo { public string Value { get; set; } public string Title { get; set; } public string Icon { get; set; } public string Color { get; set; } }
    public class ActivityItem { public string Icon { get; set; } public string Description { get; set; } public string Time { get; set; } }

    public class ChartJsData
    {
        [JsonPropertyName("labels")]
        public List<string> Labels { get; set; }
        [JsonPropertyName("datasets")]
        public List<ChartJsDataset> Datasets { get; set; }
    }

    public class ChartJsDataset
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("data")]
        public List<double?> Data { get; set; } // Using double? to allow nulls
        [JsonPropertyName("backgroundColor")]
        public List<string> BackgroundColor { get; set; }
        [JsonPropertyName("borderColor")]
        public List<string> BorderColor { get; set; }
        [JsonPropertyName("borderWidth")]
        public int? BorderWidth { get; set; }
        [JsonPropertyName("fill")]
        public bool? Fill { get; set; }
        [JsonPropertyName("tension")]
        public double? Tension { get; set; }
    }
}
