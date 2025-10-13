using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NADRAApp.Windows
{
    public partial class LoginWindow : Window
    {
        // Default credentials (in a real system, these would be in a database)
        private const string DEFAULT_USERNAME = "admin";
        private const string DEFAULT_PASSWORD = "admin123";
        
        public bool IsAuthenticated { get; private set; } = false;

        public LoginWindow()
        {
            InitializeComponent();
            
            // Set default password for demo
            PasswordBox.Password = DEFAULT_PASSWORD;
            
            // Handle Enter key press
            this.KeyDown += LoginWindow_KeyDown;
            UsernameTextBox.KeyDown += LoginWindow_KeyDown;
            PasswordBox.KeyDown += LoginWindow_KeyDown;
            
            // Focus on username field
            UsernameTextBox.Focus();
            UsernameTextBox.SelectAll();
        }

        private void LoginWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginButton.IsEnabled = false;
                LoginButton.Content = "🔄 Authenticating...";
                StatusMessage.Text = "Verifying credentials...";
                StatusMessage.Foreground = Brushes.Blue;
                
                // Process UI updates
                Application.Current.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Render);
                
                string username = UsernameTextBox.Text.Trim();
                string password = PasswordBox.Password;
                
                if (string.IsNullOrEmpty(username))
                {
                    ShowError("Please enter a username");
                    return;
                }
                
                if (string.IsNullOrEmpty(password))
                {
                    ShowError("Please enter a password");
                    return;
                }
                
                // Validate credentials
                if (ValidateCredentials(username, password))
                {
                    StatusMessage.Text = "✅ Login successful! Opening NADRA system...";
                    StatusMessage.Foreground = Brushes.Green;
                    
                    IsAuthenticated = true;
                    
                    // Process UI updates before closing
                    Application.Current.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Render);
                    
                    DialogResult = true;
                    Close();
                }
                else
                {
                    ShowError("❌ Invalid username or password");
                    
                    // Clear password field on failed login
                    PasswordBox.Password = "";
                    PasswordBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Login error: {ex.Message}");
            }
            finally
            {
                LoginButton.IsEnabled = true;
                LoginButton.Content = "🔐 Login to System";
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            // In a real system, this would check against a database with hashed passwords
            return username.Equals(DEFAULT_USERNAME, StringComparison.OrdinalIgnoreCase) && 
                   password == DEFAULT_PASSWORD;
        }

        private void ShowError(string message)
        {
            StatusMessage.Text = message;
            StatusMessage.Foreground = Brushes.Red;
            
            // Simple error display without animation to avoid threading issues
        }

        protected override void OnClosed(EventArgs e)
        {
            // Don't automatically shutdown - let App.xaml.cs handle it
            base.OnClosed(e);
        }
    }
}