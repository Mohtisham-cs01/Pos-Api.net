using System.ComponentModel.DataAnnotations;

namespace taskApi.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public string ItemDesc { get; set; }

        public decimal ItemCost { get; set; }
    }
  
}
