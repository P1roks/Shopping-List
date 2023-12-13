using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4F2.ViewModels
{
    public class RecipeViewModel
    {
        private Models.Recipe _recipe;
        public string Name { get => _recipe.name; }
        public string Description { get => _recipe.description; }
        public List<Models.Product> Products { get => _recipe.products; }

        public ICommand ImportProductsCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        public RecipeViewModel()
        {
            _recipe = new Models.Recipe();
        }

        public RecipeViewModel(Models.Recipe recipe)
        {
            _recipe = recipe;
            ImportProductsCommand = new AsyncRelayCommand(ImportProducts);
        }

        private async Task ImportProducts()
        {
            foreach(var product in Products)
            {
                Storage.AddProduct(product);
            }

            await Shell.Current.GoToAsync("//CategoryList");
        }

        private void Remove()
        {

        }
    }
}
