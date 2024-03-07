using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace taskApi.Models
{
    public class SalesDetails
    {
        [Key]
        public int SalesDetailsId { get; set; }
        public int InvoiceId { get; set; } // Foreign key
        public string Item { get; set; }
        public decimal Cost { get; set; }
    }

    
}
