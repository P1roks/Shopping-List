using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4F2.ViewModels
{
    public class ProductViewModel: ObservableObject
    {
        private Models.Product _product;
        private bool _visible;
        public bool IsCategory { get; set; }
        private string Location { get => IsCategory ? "//CategoryList" : "//ProductsList"; }

        public ICommand DeleteCommand { get; private set; }
        public ICommand IncreaseCommand { get; private set; }
        public ICommand DecreaseCommand { get; private set; }
        public ICommand SwitchBoughtCommand { get; private set; }

        public uint Count 
        {
            get => _product.count;
            set
            {
                if (_product.count != value) {
                    _product.count = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Visible { 
            get => _visible;
            set
            {
                if(_visible != value)
                {
                    _visible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Bought { get => _product.bought; }
        public Color BoughtColor { get => _product.bought ? Colors.Lime : Colors.Gray; }
        public double Opacity { get => _product.bought ? 0.7 : 1.0; }
        public string Name { get => _product.name; }
        public string Unit { get => _product.unit; }
        public string Category { get => _product.category; }
        public double OptionalOpacity { get => _product.required ? 1 : 0; }
        public string Store { get => _product.shop; }
        public Guid Id { get => _product.id; }

        public ProductViewModel()
        {
            _product = new Models.Product();
            _visible = true;
        }

        public ProductViewModel(Models.Product product, bool isCategory)
        {
            _product = product; 
            DeleteCommand = new AsyncRelayCommand(Delete);
            SwitchBoughtCommand = new AsyncRelayCommand(SwitchBought);
            IncreaseCommand = new Command(Increase);
            DecreaseCommand = new Command(Decrease);
            IsCategory = isCategory;
            _visible = true;
        }

        private void Increase()
        {
            Count += 1;
            Storage.SaveProduct(_product, "Count", Count);
        }

        private void Decrease()
        {
            if (Count == 1) return;
            Count -= 1;
            Storage.SaveProduct(_product, "Count", Count);
        }

        private async Task Delete()
        {
            Storage.DeleteProduct(_product);
            var queryParams = new Dictionary<string, object>()
            {
                {"product",this},
                {"deleted",null},
            };
            await Shell.Current.GoToAsync(Location,queryParams);
        }

        private async Task SwitchBought()
        {
            _product.bought = !_product.bought;
            OnPropertyChanged(nameof(BoughtColor));
            OnPropertyChanged(nameof(Opacity));
            Storage.SaveProduct(_product, "Bought", _product.bought);
            var queryParams = new Dictionary<string, object>()
            {
                {"product",this},
                {"bought", _product.bought},
            };
            await Shell.Current.GoToAsync(Location, queryParams);
        }
    }
}
