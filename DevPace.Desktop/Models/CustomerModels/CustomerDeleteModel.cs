using System;
using System.Text.Json.Serialization;

namespace DevPace.Desktop.Models.CustomerModels
{
    public class CustomerDeleteModel
    {
        [JsonInclude]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}