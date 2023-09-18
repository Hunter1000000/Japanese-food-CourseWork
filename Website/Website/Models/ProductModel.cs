using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public string PhotoPath { get; set; }

        public ProductModel()
        {

        }

        public ProductModel(string name, string description, string company, float price, string type, string photo)
        {
            Name = name;
            Description = description;
            Company = company;
            Price = price;
            Type = type;
            PhotoPath = photo;
        }
    }
}
