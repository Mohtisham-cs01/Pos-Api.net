using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace taskApi.Models
{
    
    public class SalesMaster
    {
        [Key]
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }


        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }

        
    }
  
}
