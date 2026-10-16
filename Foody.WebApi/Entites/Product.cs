using System.Net.NetworkInformation;

namespace Foody.WebApi.Entites
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId{ get; set; }
        public Category? Category { get; set; }
    }
}
