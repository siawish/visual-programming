using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NADRAApp.Models;
using NADRAApp.Services;

namespace NADRAApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CitizenService _citizenService;
        private readonly ImageProcessingService _imageService;
        private ObservableCollection<Citizen> _citizens;
        private Citizen? _selectedCitizen;
        private string _searchText = string.Empty;
        private bool _isLoading;

        public MainViewModel(CitizenService citizenService, ImageProcessingService imageService)
        {
            _citizenService = citizenService;
            _imageService = imageService;
            _citizens = new ObservableCollection<Citizen>();
            
            LoadCitizensCommand = new AsyncRelayCommand(LoadCitizens);
            SearchCommand = new AsyncRelayCommand(SearchCitizens);
            AddCitizenCommand = new RelayCommand(() => AddNewCitizen());
            EditCitizenCommand = new RelayCommand(() => EditCitizen(), () => SelectedCitizen != null);
            DeleteCitizenCommand = new RelayCommand(async () => await DeleteCitizen(), () => SelectedCitizen != null);
            
            _ = LoadCitizens();
        }

        public ObservableCollection<Citizen> Citizens
        {
            get => _citizens;
            set { _citizens = value; OnPropertyChanged(); }
        }

        public Citizen? SelectedCitizen
        {
            get => _selectedCitizen;
            set { _selectedCitizen = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public ICommand LoadCitizensCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AddCitizenCommand { get; }
        public ICommand EditCitizenCommand { get; }
        public ICommand DeleteCitizenCommand { get; }

        private async Task LoadCitizens()
        {
            IsLoading = true;
            try
            {
                var citizens = await _citizenService.GetAllCitizensAsync();
                Citizens.Clear();
                foreach (var citizen in citizens)
                {
                    Citizens.Add(citizen);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading citizens: {ex.Message}", "Database Error", 
                                             System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SearchCitizens()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadCitizens();
                return;
            }

            IsLoading = true;
            try
            {
                var citizens = await _citizenService.SearchCitizensAsync(SearchText);
                Citizens.Clear();
                foreach (var citizen in citizens)
                {
                    Citizens.Add(citizen);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void AddNewCitizen()
        {
            // Open Add Citizen window
        }

        private void EditCitizen()
        {
            if (SelectedCitizen != null)
            {
                // Open Edit Citizen window
            }
        }

        private async Task DeleteCitizen()
        {
            if (SelectedCitizen != null)
            {
                var result = System.Windows.MessageBox.Show(
                    $"Are you sure you want to delete {SelectedCitizen.FirstName} {SelectedCitizen.LastName}?",
                    "Confirm Delete", 
                    System.Windows.MessageBoxButton.YesNo, 
                    System.Windows.MessageBoxImage.Question);
                
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    await _citizenService.DeleteCitizenAsync(SelectedCitizen.Id);
                    await LoadCitizens();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { System.Windows.Input.CommandManager.RequerySuggested += value; }
            remove { System.Windows.Input.CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
        public void Execute(object? parameter) => _execute();
    }

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { System.Windows.Input.CommandManager.RequerySuggested += value; }
            remove { System.Windows.Input.CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke() ?? true);

        public async void Execute(object? parameter)
        {
            if (_isExecuting) return;
            
            _isExecuting = true;
            try
            {
                await _execute();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Command execution error: {ex.Message}", "Error", 
                                             System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                _isExecuting = false;
                System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}