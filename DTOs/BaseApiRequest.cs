namespace Pharmacy_Backend.DTOs
{
    public class BaseApiRequest
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public long UserId { get; set; }

    }
}
