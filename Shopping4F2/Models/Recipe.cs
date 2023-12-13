using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shopping4F2.Models
{
    public class Recipe
    {
        public Guid id;
        public string name;
        public string description;
        public List<Product> products;

        public Recipe() 
        {
            id = Guid.NewGuid();
            name = string.Empty;
            description = string.Empty;
            products = new();
        }

        public Recipe(string name, string description, List<Product> products)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.description = description;
            this.products = products;
        }

        public Recipe(XElement recipe)
        {
            id = (Guid)recipe.Element("ID");
            name = recipe.Element("Name").Value;
            description = recipe.Element("Description").Value;
            products = new(from product in recipe.Element("Products").Elements("Product") select new Product(product));
        }

        public XElement ToXML() =>
            new("Recipe",
                   new XElement("ID", id),
                   new XElement("Name", name),
                   new XElement("Description", description),
                   new XElement("Products", from product in products select product.ToXML())
                );
    }
}
