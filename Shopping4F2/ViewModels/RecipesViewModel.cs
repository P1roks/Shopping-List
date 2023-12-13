using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4F2.ViewModels
{
    class RecipesViewModel
    {
        public ObservableCollection<RecipeViewModel> Recipes { get; private set; }

        public ICommand AddRecipeCommand { get; private set; }

        public RecipesViewModel()
        {
            Recipes = new(); 
            AddRecipeCommand = new AsyncRelayCommand(AddRecipe);
            LoadRecipes();
        }

        public void LoadRecipes()
        {
            Recipes.Clear();
            foreach(var recipe in Storage.GetRecipes())
            {
                Recipes.Add(new RecipeViewModel(recipe));
            }
        }

        private async Task AddRecipe()
        {
            await Shell.Current.GoToAsync(nameof(Views.AddRecipeView));
        }
    }
}
