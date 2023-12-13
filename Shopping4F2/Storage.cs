using Shopping4F2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shopping4F2
{
    internal static class Storage
    {
        private static readonly string _fileName = Path.Combine(FileSystem.AppDataDirectory, "shopping.xml");
        public static XDocument XML;

        static Storage()
        {
            try { XML = XDocument.Load(_fileName); }
            catch (FileNotFoundException)
            {
                XML = new(new XElement("ShoppingList"))
                {
                    Declaration = new XDeclaration("1.0", "utf-8", "true")
                };
                XML.Root.Add(
                    new XElement("Categories",
                    from category in new string[3]{ "Fruits", "Vegetables", "N/A"} select new XElement("Category",category)));
                XML.Root.Add(new XElement("Shops"));
                XML.Root.Add(new XElement("Products"));
                var sampleRecipe1 = new Recipe(
                        "Koreczki z pomidorami, mozzarellą i bazylią",
                        "Wszystkie składniki nadziać na wykałaczkę w kolejności według uznania",
                        new List<Product> {
                            new Product("Pomidor", "pcs", "Vegetables", 12),
                            new Product("Kuleczka Mozarelli", "pcs", "N/A", 12),
                            new Product("Bazylia", "pcs", "N/A", 24),
                        }
                    );
                var sampleRecipe2 = new Recipe(
                        "Sałatka jarzynowa",
                        "- Ziemniaki, marchewkę i pietruszkę umyć (nie obierać), włożyć do garnka, zalać wodą, posolić i gotować pod przykryciem do miękkości, przez ok. 40 minut.\r\n- Odcedzić, ostudzić, obrać ze skórek i pokroić w kosteczkę, włożyć do dużej miski.\r\n- Jajka ugotować na twardo (ok. 5 - 6 minut licząc od zagotowania się wody), pokroić w kosteczkę, dodać do miski z jarzynami.\r\n- Ogórki, cebulę oraz jabłko obrać i pokroić w kosteczkę, dodać do miski. Wsypać dobrze odsączony i osuszony groszek.\r\n- Wymieszać z majonezem i musztardą",
                        new List<Product> {
                            new Product("Ziemniak", "pcs", "Vegetables", 3),
                            new Product("Marchew", "pcs", "Vegetables", 5),
                            new Product("Jabłko", "pcs", "N/A", 4),
                            new Product("Majonez", "tbsp", "N/A", 1),
                        }
                    );
                XML.Root.Add(new XElement("Recipes",
                        sampleRecipe1.ToXML(),
                        sampleRecipe2.ToXML()
                    ));

                XML.Save(_fileName);
            }
        }

        public static void SaveTo(string location)
        {
            XML.Save(location);
        }

        public static void Import(string location)
        {
            Trace.WriteLine(location);
            File.Copy(location, _fileName, true);
            XML = XDocument.Load(_fileName);
            Product.Categories = GetCategories();
            Product.Shops = GetShops();
        }

        // Products
        public static IEnumerable<Product> GetProducts() =>
            from product in XML.Root.Element("Products").Elements("Product")
             select new Product(product);

        public static XElement GetProduct(Guid guid) =>
            (from product in XML.Root.Element("Products").Elements("Product")
            where (Guid)product.Element("ID") == guid select product).FirstOrDefault();

        public static void SaveProduct(Product product, string fieldName, object newVal)
        {
            var productXML = GetProduct(product.id);
            productXML.Element(fieldName).Value = newVal.ToString();
            XML.Save(_fileName);
        }

        public static void DeleteProduct(Product product)
        {
            GetProduct(product.id).Remove();
            XML.Save(_fileName);
        }

        public static void AddProduct(Product product)
        {
            XML.Root.Element("Products").Add(product.ToXML());
            XML.Save(_fileName);
        }

        // Recipes
        public static IEnumerable<Recipe> GetRecipes() =>
            from recipe in XML.Root.Element("Recipes").Elements("Recipe")
                select new Recipe(recipe);

        public static void AddRecipe(Recipe recipe)
        {
            XML.Root.Element("Recipes").Add(recipe.ToXML());
            XML.Save(_fileName);
        }

        // Generic
        private static List<string> getList(string root, string child) =>
            (from val in XML.Root.Element(root).Elements(child)
            select val.Value).ToList();

        public static void AddList(string root, string child, string val)
        {
            XML.Root.Element(root).Add(new XElement(child, val));
            XML.Save(_fileName);
        }

        // Categories
        public static List<string> GetCategories() => getList("Categories", "Category");
        public static void AddCategory(string category) => AddList("Categories", "Category", category);

        // Shops
        public static List<string> GetShops() => getList("Shops", "Shop");
        public static void AddShop(string shop) => AddList("Shops", "Shop", shop);
    }
}
