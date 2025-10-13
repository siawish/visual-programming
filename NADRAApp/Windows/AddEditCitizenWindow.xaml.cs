using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NADRAApp.Models;
using NADRAApp.Services;

namespace NADRAApp.Windows
{
    public partial class AddEditCitizenWindow : Window
    {
        private readonly CitizenService _citizenService;
        private readonly ImageProcessingService _imageService;
        private Citizen _citizen;
        private string? _photoPath;

        public AddEditCitizenWindow(CitizenService citizenService, ImageProcessingService imageService, Citizen? citizen = null)
        {
            InitializeComponent();
            _citizenService = citizenService;
            _imageService = imageService;
            _citizen = citizen ?? new Citizen();
            
            DataContext = this;
            LoadCitizenData();
        }

        public string WindowTitle => _citizen.Id == 0 ? "Add New Citizen" : "Edit Citizen";
        public Citizen Citizen => _citizen;

        private void LoadCitizenData()
        {
            if (!string.IsNullOrEmpty(_citizen.PhotoPath))
            {
                _photoPath = _citizen.PhotoPath;
                PhotoImage.Source = _imageService.LoadImageFromPath(_photoPath);
            }
        }

        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select citizen photo"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Save the photo and get the path
                    _photoPath = _imageService.SaveCitizenPhoto(openFileDialog.FileName, _citizen.CNIC);
                    PhotoImage.Source = _imageService.LoadImageFromPath(_photoPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading photo: {ex.Message}", "Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TakePhoto_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Camera functionality would be implemented here using DirectShow or similar.", 
                          "Feature Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_photoPath))
            {
                _imageService.DeleteImage(_photoPath);
                _photoPath = null;
            }
            PhotoImage.Source = null;
        }

        private void ScanFingerprint_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("🔮 Fingerprint Scanner Integration\n\n" +
                          "This feature will be implemented in future versions with:\n" +
                          "• Biometric fingerprint scanner integration\n" +
                          "• Real-time fingerprint capture\n" +
                          "• Advanced biometric matching algorithms\n" +
                          "• Secure fingerprint template storage\n\n" +
                          "Currently under development for enhanced security.", 
                          "Future Feature", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearFingerprint_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fingerprint data will be cleared when the feature is implemented.", 
                          "Future Feature", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                _citizen.PhotoPath = _photoPath;

                bool success;
                if (_citizen.Id == 0)
                {
                    success = await _citizenService.AddCitizenAsync(_citizen);
                }
                else
                {
                    success = await _citizenService.UpdateCitizenAsync(_citizen);
                }

                if (success)
                {
                    MessageBox.Show("Citizen saved successfully!", "Success", 
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to save citizen. CNIC might already exist.", "Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving citizen: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(_citizen.CNIC) || _citizen.CNIC.Length != 13)
            {
                MessageBox.Show("CNIC must be exactly 13 digits.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(_citizen.FirstName))
            {
                MessageBox.Show("First name is required.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(_citizen.LastName))
            {
                MessageBox.Show("Last name is required.", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_citizen.DateOfBirth == default || _citizen.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show("Please enter a valid date of birth (minimum age 18).", "Validation Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


    }
}