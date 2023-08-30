namespace Website.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public string Photo { get; set; }

        public ProductModel(int id, string name, string description, string company, float price, string type, string photo)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Company = company;
            this.Price = price;
            this.Type = type;
            this.Photo = photo;
        }
    }
}
