using System.ComponentModel.DataAnnotations;

namespace TestMVC.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? Quantity { get; set; }
        public string? Avaliablity { get; set; }
    }
}
