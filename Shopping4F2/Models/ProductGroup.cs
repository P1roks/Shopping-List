using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4F2.ViewModels;

namespace Shopping4F2.Models
{
    public class ProductGroup
    {
        public ObservableCollection<ProductViewModel> Products { get; private set; }
        public string Category { get; private set; }
        public bool Expanded { get; set; }

        public ProductGroup(string category, ObservableCollection<ProductViewModel> products)
        {
            Products = products;
            Category = category;
            Expanded = true;
        } 

        public ProductGroup(Product product)
        {
            Products = new ObservableCollection<ProductViewModel>() { new ProductViewModel(product, true)};
            Category = product.category;
            Expanded = true;
        }

        public ProductGroup() 
        {
            Products = new();
            Category = "N/A";
            Expanded = false;
        }
    }
}
