using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CheboksaryHalfMarathon.WebAplication.DTO
{
    public class UserCreationOptionsDto
    {
        [JsonProperty("userEmail")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; }
        [JsonProperty("userPassword")]
        public string UserPassword { get; set; }
        [JsonProperty("userRole")]
        public string UserRole { get; set; }
    }
}
