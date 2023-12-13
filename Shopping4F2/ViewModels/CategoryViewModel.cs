using CommunityToolkit.Mvvm.ComponentModel;
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
    public class CategoryViewModel : ObservableObject
    {
        private Models.ProductGroup _group;
        public ICommand CollapseCommand { get; set; }
        public bool Expanded
        {
            get => _group.Expanded;
            set
            {
                if (_group.Expanded != value) {
                    _group.Expanded = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Arrow));
                }
            }
        }
        public string Arrow { get => Expanded ? Models.Icons.DownArrowIcon : Models.Icons.UpArrowIcon; }
        public string Text { get => _group.Category; }
        public ObservableCollection<ProductViewModel> Products {get => _group.Products;}

        public CategoryViewModel()
        {
            _group = new Models.ProductGroup();
        }

        public CategoryViewModel(Models.ProductGroup group)
        {
            _group = group;
            CollapseCommand = new Command(Collapse);
        }

        private void Collapse()
        {
            Expanded = !Expanded;
        }
    }
}
