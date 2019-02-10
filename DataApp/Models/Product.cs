using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    public class Product
    {
        public Product() { }
        public Product(long id) => Id = id;
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public Colors Color { get; set; }
        public bool InStock { get; set; }
        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public IEnumerable<ProductShipmentJunction> ProductShipments { get; set; }
    }
    public enum Colors
    {
        Red, Green, Blue
    }
}
