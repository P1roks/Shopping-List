using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4F2.ViewModels
{
    class ProductsViewModel: IQueryAttributable
    {
        public ObservableCollection<ProductViewModel> Products { get; private set; }
        public ICommand AddProductCommand { get; private set; }
        public ICommand AddCategoryCommand { get; private set; }
        public ICommand FilterStoreCommand { get; private set; }
        public ICommand SortCommand { get; private set; }

        public ProductsViewModel() 
        {
            Products = new();
            AddProductCommand = new AsyncRelayCommand(AddProduct);
            AddCategoryCommand = new AsyncRelayCommand(AddCategory);
            FilterStoreCommand = new AsyncRelayCommand(FilterStore);
            SortCommand = new AsyncRelayCommand(SortBy);
            LoadProducts();
        }

        public void LoadProducts()
        {
            Products.Clear();
            foreach(var prod in
                Storage.GetProducts().Select(p => new ProductViewModel(p, false)).Where(p => !p.Bought).OrderBy(p => p.Category))
            {
                Products.Add(prod);
            }
        }

        private async Task AddProduct()
        {
            await Shell.Current.GoToAsync(nameof(Views.AddProductView));
        }

        private async Task AddCategory()
        {
            string category = await Shell.Current.DisplayPromptAsync("Add Category", "Enter New Category Name");
            if(Models.Product.Categories.Contains(category)) { return; }
            Storage.AddCategory(category);
            Models.Product.Categories.Add(category);
        }

        private async Task FilterStore()
        {
            string store = await Shell.Current.DisplayActionSheet("Choose store to filter by", "Cancel", "Unfilter", Models.Product.Shops.ToArray());
            if (store == "Unfilter") {
                foreach (var item in Products)
                {
                    item.Visible = true; 
                }
            }
            else
            {
                foreach (var item in Products)
                {
                    item.Visible = store == item.Store; 
                }
            }
        }

        private async Task SortBy()
        {
            string by = await Shell.Current.DisplayActionSheet("Sort by", "Cancel", null, "Category", "Name", "Count");
            var sortable = new List<ProductViewModel>(Products);

            switch (by)
            {
                case "Category":
                    sortable.Sort(delegate(ProductViewModel p1, ProductViewModel p2) { return p1.Category.CompareTo(p2.Category); });
                    break;
                case "Name":
                    sortable.Sort(delegate(ProductViewModel p1, ProductViewModel p2) { return p1.Name.CompareTo(p2.Name); });
                    break;
                case "Count":
                    sortable.Sort(delegate(ProductViewModel p1, ProductViewModel p2) { return p2.Count.CompareTo(p1.Count); });
                    break;
                default:
                    return;
            }

            for(int i  = 0; i < sortable.Count; ++i)
            {
                Products.Move(Products.IndexOf(sortable[i]), i);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            /*
            if (query.ContainsKey("added_product"))
            {
                Models.Product prod = (Models.Product)query["added_product"];
                Products.Add(new ProductViewModel(prod, false));
            }
            */

            ProductViewModel product = query.TryGetValue("product", out var p) ? (ProductViewModel)p : null;

            if (product == null) { return; }

            if (query.ContainsKey("deleted") || query.ContainsKey("bought"))
            {
                Products.Remove(product);
            }
        }
    }
}
