namespace Pharmacy_Backend.DTOs
{
    public class IlacUpdateModel
    {
        public long Id { get; set; } // Zorunlu çünkü hangi ilacı güncelleyeceğimizi bilmeliyiz
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Barcode { get; set; }
        public int? Stock { get; set; }
    }
}
