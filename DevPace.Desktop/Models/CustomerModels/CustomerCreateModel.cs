using System;
using System.Text.Json.Serialization;

namespace DevPace.Desktop.Models.CustomerModels
{
    public class CustomerCreateModel
    {
        [JsonInclude]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonInclude]
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }

        [JsonInclude]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonInclude]
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
