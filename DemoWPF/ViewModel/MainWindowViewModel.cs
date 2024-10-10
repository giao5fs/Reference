using DemoWPF.Model;
using DemoWPF.MVVM;
using System.Collections.ObjectModel;

namespace DemoWPF.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; }
        public RelayCommand AddCommand => new RelayCommand(_ => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(_ => DeleteItem(), _ => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(_ => SaveItem(), _ => CanSave());

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Item>
            {
                new Item
                {
                    Name = "Name",
                    SerialNumber = Guid.NewGuid().ToString(),
                    Quantity = 1,
                }
            };
        }

        private Item? selectedItem;

        public Item? SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private void AddItem()
        {
            Items.Add(new Item
            {
                Name = "New Item",
                SerialNumber = "XXXXX11",
                Quantity = 1,
            });
        }

        private void DeleteItem()
        {
            Items.Remove(selectedItem);
        }

        private void SaveItem()
        {
            // Storing to DB or File
        }

        private bool CanSave()
        {
            return true;
        }
    }
}
