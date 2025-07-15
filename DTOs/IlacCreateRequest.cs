namespace Pharmacy_Backend.DTOs
{
    public class IlacCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public int Stock { get; set; }
    }
}
