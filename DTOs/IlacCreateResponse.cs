namespace Pharmacy_Backend.DTOs
{
    public class IlacCreateResponse 
    {
        public long Id { get; set; }  // Veritabanından dönecek otomatik artan Id
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
