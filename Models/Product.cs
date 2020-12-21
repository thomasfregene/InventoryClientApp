using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryClientApp.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQty { get; set; }
    }
}
