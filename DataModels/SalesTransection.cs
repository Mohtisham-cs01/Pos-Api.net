using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskApi.Models;

namespace taskApi.DataModels
{
    //public class SalesMaster
    public class SalesTransection
    {
        public int CustomerId { get; set; }
        public List<int> ItemIds { get; set; }
    }

}
