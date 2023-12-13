using Shopping4F2.Views;

namespace Shopping4F2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddProductView), typeof(AddProductView));
            Routing.RegisterRoute(nameof(AddRecipeView), typeof(AddRecipeView));
        }
    }
}