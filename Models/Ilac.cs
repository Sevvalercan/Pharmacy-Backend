using System.ComponentModel.DataAnnotations.Schema;
using Pharmacy_Backend.Core.Entities;

namespace Pharmacy_Backend.Models

{
    [Table("Medicines")]
    public class Ilac: BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }
        [Column("Barcode")]
        public string Barcode { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Stock")]
        public int Stock { get; set; }


    }
}
