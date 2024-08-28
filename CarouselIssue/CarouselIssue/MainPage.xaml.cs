using System.ComponentModel;
using System.Collections.ObjectModel;

namespace CarouselIssue
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new Issue17283ViewModel();
        }

      
    }
    public class Issue17283ViewModel : INotifyPropertyChanged
    {
        ObservableCollection<string> _items;
        public ObservableCollection<string> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        int _position;
        public int Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public Command ReloadItemsCommand { get; set; }
        public Command GoToLastItemCommand { get; set; }

        public Issue17283ViewModel()
        {
            Items = new ObservableCollection<string> { "1", "2", "3", "4", "5" };
            ReloadItemsCommand = new Command(ReloadItems);
            GoToLastItemCommand = new Command(() => Position = Items.Count - 1);
        }

        async void ReloadItems()
        {
            var currentPosition = Position;
            Items = new ObservableCollection<string> { "1", "2", "3", "4", "5last" };
            await Task.Delay(300);
            Position = currentPosition;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
