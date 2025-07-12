using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Pharmacy_Backend.Models
{
    public class BaseEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Column("status")]
        public bool Status { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [Column("deleted_date")]

        public DateTime DeletedDate { get; set; }

    }
}
