using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shopping4F2.Models
{
    public class Product
    {
        public static List<string> Categories;
        public static List<string> Shops;
        public Guid id;
        public string name;
        public string unit;
        public string category;
        public string shop;
        public uint count;
        public bool required;
        public bool bought;

        public string Name { get => name; }
        public string Unit { get => unit; }
        public uint Count { get => count; }

        static Product()
        {
            Categories = Storage.GetCategories();
            Shops = Storage.GetShops();
        }

        public Product()
        {
            id = Guid.NewGuid();
            name = string.Empty;
            unit = string.Empty;
            category = "N/A";
            shop = null;
            count = 1;
            required = false;
        }

        public Product(string name, string unit, string category, uint count)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.unit = unit;
            this.category = category;
            this.shop = null;
            this.count = count;
            this.required = false;
        }

        public Product(string name, string unit, string category, string shop, uint count, bool required)
        {
            if (!Categories.Contains(category))
            {
                throw new Exception("Provided category is invalid!");
            }

            this.id = Guid.NewGuid();
            this.name = name;
            this.unit = unit;
            this.category = category;
            this.shop = shop;
            this.count = count;
            this.required = required;

            if (shop != null && !Shops.Contains(shop))
            {
                Shops.Add(shop);
                Storage.AddShop(shop);
            }
            Storage.AddProduct(this);
        }

        public Product(XElement product)
        {
            id = (Guid)product.Element("ID");
            name = product.Element("Name").Value;
            unit = product.Element("Unit").Value;
            category = product.Element("Category").Value;
            count = (uint)product.Element("Count");
            shop = product.Element("Shop").Value;
            required = (bool)product.Element("Optional");
            bought = (bool)product.Element("Bought");
        }

        public XElement ToXML() =>
            new("Product",
                    new XElement("ID", id),
                    new XElement("Name", name),
                    new XElement("Unit", unit),
                    new XElement("Category", category),
                    new XElement("Count", count),
                    new XElement("Shop", shop),
                    new XElement("Optional", required),
                    new XElement("Bought", bought)
                );

        public static XElement CategoriesToXML() =>
            new("Categories", from category in Categories select new XElement("Category",category));
    }
}
