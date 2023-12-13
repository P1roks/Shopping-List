using CommunityToolkit.Mvvm.Input;
using Shopping4F2.Models;
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
    class ProductsCategoriesViewModel: IQueryAttributable
    {
        public ObservableCollection<CategoryViewModel> Categories { get; private set; }
        public ICommand AddProductCommand { get; private set; }
        public ICommand AddCategoryCommand { get; private set; }

        public ProductsCategoriesViewModel() 
        {
            Categories = new();
            AddProductCommand = new AsyncRelayCommand(AddProduct);
            AddCategoryCommand = new AsyncRelayCommand(AddCategory);
            LoadCategories();
        }

        public void LoadCategories()
        {
            Categories.Clear();
            var categories = Storage.GetProducts().Select(p => new ProductViewModel(p, true)).GroupBy(p => p.Category);
            foreach(var category in categories)
            {
                var catViewModel = new CategoryViewModel(
                    new Models.ProductGroup(
                        category.Key,
                        new ObservableCollection<ProductViewModel>(category.Select(g => g).OrderBy(p => p.Bought))
                    )
                );
                Categories.Add(catViewModel);
            }
        }

        private async Task AddProduct()
        {
            await Shell.Current.GoToAsync(nameof(Views.AddProductView));
        }

        private async Task AddCategory()
        {
            string category = await Shell.Current.DisplayPromptAsync("Add Category", "Enter New Category Name");
            Storage.AddCategory(category);
            Models.Product.Categories.Add(category);
        }

        private ObservableCollection<ProductViewModel> getGroupFromCategory(string category) =>
            Categories.FirstOrDefault(c => c.Text == category)?.Products;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            /*
            if (query.ContainsKey("added_product"))
            {
                Models.Product prod = (Models.Product)query["added_product"];
                string category = prod.category;
                var group = getGroupFromCategory(category);
                if(group == null)
                {
                    Categories.Add(new CategoryViewModel(new Models.ProductGroup(prod)));
                }
                else
                {
                    group.Add(new ProductViewModel(prod,true));
                }
            }
            */

            ProductViewModel product = query.TryGetValue("product", out var p) ? (ProductViewModel)p : null;
            if (product == null) { return; }

            string cat = product.Category;
            if (query.ContainsKey("deleted"))
            {
                getGroupFromCategory(cat).Remove(product);
            }
            if (query.ContainsKey("bought"))
            {
                var group = getGroupFromCategory(cat);
                var idx = group.IndexOf(product);

                if ((bool)query["bought"])
                {
                    group.Move(idx, group.Count - 1);
                }
                else
                {
                    group.Move(idx, 0);
                }
            }
        }
    }
}
