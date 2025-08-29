using Newtonsoft.Json;

namespace CheboksaryHalfMarathon.WebAplication.DTO
{
    public class UserDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }
        [JsonProperty("userPassword")]
        public string UserPassword { get; set; }
        [JsonProperty("userRole")]
        public string UserRole { get; set; }
        [JsonProperty("userVersion")]
        public int UserVersion { get; set; }
        [JsonProperty("userRegistrationDate")]
        public DateTimeOffset UserRegistrationDate { get; set; }
    }
}
