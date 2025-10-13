using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using NADRAApp.ViewModels;
using NADRAApp.Windows;

namespace NADRAApp
{
    public partial class MainWindow : Window
    {
        private const string DEFAULT_USERNAME = "admin";
        private const string DEFAULT_PASSWORD = "admin123";
        private System.Windows.Threading.DispatcherTimer? clockTimer;

        public MainWindow()
        {
            InitializeComponent();
            
            // Don't pre-fill password for security
            
            // Handle Enter key press for login
            this.KeyDown += MainWindow_KeyDown;
            UsernameTextBox.KeyDown += MainWindow_KeyDown;
            PasswordBox.KeyDown += MainWindow_KeyDown;
            
            // Focus on username field
            UsernameTextBox.Focus();
            UsernameTextBox.SelectAll();
            
            // Initialize clock (but don't start until after login)
            InitializeClock();
        }

        private void InitializeClock()
        {
            clockTimer = new System.Windows.Threading.DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimer_Tick;
        }

        private void ClockTimer_Tick(object? sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("h:mm tt");
        }

        private void StartClock()
        {
            if (clockTimer != null)
            {
                ClockTextBlock.Text = DateTime.Now.ToString("h:mm tt");
                clockTimer.Start();
            }
        }

        private void StopClock()
        {
            clockTimer?.Stop();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && LoginPanel.Visibility == Visibility.Visible)
            {
                LoginButton_Click(sender, e);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Show loading state
                LoginButton.IsEnabled = false;
                LoginButton.Content = "🔄 AUTHENTICATING...";
                LoginStatusMessage.Text = "Verifying credentials...";
                LoginStatusMessage.Foreground = Brushes.Blue;
                
                // Force UI update to show loading state
                await Dispatcher.InvokeAsync(() => { }, System.Windows.Threading.DispatcherPriority.Render);
                
                string username = UsernameTextBox.Text.Trim();
                string password = PasswordBox.Password;
                
                if (string.IsNullOrEmpty(username))
                {
                    ShowLoginError("Please enter a username");
                    return;
                }
                
                if (string.IsNullOrEmpty(password))
                {
                    ShowLoginError("Please enter a password");
                    return;
                }
                
                // Add delay to show loading animation
                await Task.Delay(800);
                
                // Validate credentials
                if (ValidateCredentials(username, password))
                {
                    LoginStatusMessage.Text = "✅ Login successful! Loading system...";
                    LoginStatusMessage.Foreground = Brushes.Green;
                    
                    // Show success state briefly
                    await Task.Delay(500);
                    
                    // Hide login panel and show main panel
                    LoginPanel.Visibility = Visibility.Collapsed;
                    MainPanel.Visibility = Visibility.Visible;
                    
                    // Start the real-time clock
                    StartClock();
                }
                else
                {
                    ShowLoginError("❌ Invalid username or password");
                    PasswordBox.Password = "";
                    PasswordBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowLoginError($"Login error: {ex.Message}");
            }
            finally
            {
                LoginButton.IsEnabled = true;
                LoginButton.Content = "🔐 LOGIN TO SYSTEM";
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            return username.Equals(DEFAULT_USERNAME, StringComparison.OrdinalIgnoreCase) && 
                   password == DEFAULT_PASSWORD;
        }

        private void ShowLoginError(string message)
        {
            LoginStatusMessage.Text = message;
            LoginStatusMessage.Foreground = Brushes.Red;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Stop the clock
                StopClock();
                
                // Clear login fields
                UsernameTextBox.Text = "admin";
                PasswordBox.Password = "";
                LoginStatusMessage.Text = "";
                
                // Show login panel and hide main panel
                MainPanel.Visibility = Visibility.Collapsed;
                LoginPanel.Visibility = Visibility.Visible;
                
                // Focus on password field for re-login
                PasswordBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during logout: {ex.Message}", "Logout Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reportsWindow = new ReportsWindow();
                reportsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening reports: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FaceRecognition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var faceRecognitionWindow = new FaceRecognitionWindow();
                faceRecognitionWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening face recognition: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddCitizen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.CitizenService != null && App.ImageService != null)
                {
                    var addWindow = new AddEditCitizenWindow(App.CitizenService, App.ImageService);
                    if (addWindow.ShowDialog() == true)
                    {
                        // Refresh the data
                        if (DataContext is MainViewModel viewModel)
                        {
                            viewModel.LoadCitizensCommand.Execute(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening add citizen window: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditCitizen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext is MainViewModel viewModel && viewModel.SelectedCitizen != null)
                {
                    if (App.CitizenService != null && App.ImageService != null)
                    {
                        var editWindow = new AddEditCitizenWindow(App.CitizenService, App.ImageService, viewModel.SelectedCitizen);
                        if (editWindow.ShowDialog() == true)
                        {
                            // Refresh the data
                            viewModel.LoadCitizensCommand.Execute(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit citizen window: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            // Stop the clock timer when window closes
            StopClock();
            clockTimer = null;
            base.OnClosed(e);
        }
    }
}