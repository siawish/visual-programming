using System;
using System.Linq;
using System.Windows;
using NADRAApp.Data;
using NADRAApp.Services;
using NADRAApp.ViewModels;
using NADRAApp.Windows;

namespace NADRAApp
{
    public partial class App : Application
    {
        public static NADRAContext? DatabaseContext { get; private set; }
        public static CitizenService? CitizenService { get; private set; }
        public static ImageProcessingService? ImageService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            try
            {
                // Initialize services first
                ImageService = new ImageProcessingService();
                DatabaseContext = new NADRAContext();
                CitizenService = new CitizenService(DatabaseContext);
                
                // Test database connection and create tables
                try
                {
                    DatabaseContext.Database.EnsureCreated();
                    
                    // Check if we need to seed data
                    if (!DatabaseContext.Citizens.Any())
                    {
                        SeedDatabase();
                    }
                    
                    System.Diagnostics.Debug.WriteLine("✅ Database connected successfully!");
                }
                catch (Exception dbEx)
                {
                    MessageBox.Show($"⚠️ Database connection failed: {dbEx.Message}\n\n" +
                                  "Please ensure XAMPP MySQL server is running.", 
                                  "Database Connection", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(1);
                    return;
                }

                // Start main window with login panel
                var mainWindow = new MainWindow();
                var viewModel = new MainViewModel(CitizenService, ImageService);
                mainWindow.DataContext = viewModel;
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application startup error: {ex.Message}", "Startup Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void SeedDatabase()
        {
            try
            {
                var citizens = new[]
                {
                    new Models.Citizen
                    {
                        CNIC = "1234567890123",
                        FirstName = "Ahmed",
                        LastName = "Ali",
                        FatherName = "Muhammad Ali",
                        DateOfBirth = new DateTime(1990, 5, 15),
                        Gender = "Male",
                        Address = "House 123, Street 45, Sector G-9",
                        City = "Islamabad",
                        Province = "Islamabad",
                        PhoneNumber = "0300-1234567",
                        Email = "ahmed.ali@email.com",
                        BloodGroup = "B+",
                        MaritalStatus = "Married",
                        Occupation = "Software Engineer",
                        RegistrationDate = DateTime.Now.AddDays(-30),
                        IsActive = true
                    },
                    new Models.Citizen
                    {
                        CNIC = "9876543210987",
                        FirstName = "Fatima",
                        LastName = "Khan",
                        FatherName = "Abdul Khan",
                        DateOfBirth = new DateTime(1985, 8, 22),
                        Gender = "Female",
                        Address = "Flat 456, Block B, Gulshan-e-Iqbal",
                        City = "Karachi",
                        Province = "Sindh",
                        PhoneNumber = "0321-9876543",
                        Email = "fatima.khan@email.com",
                        BloodGroup = "A+",
                        MaritalStatus = "Single",
                        Occupation = "Doctor",
                        RegistrationDate = DateTime.Now.AddDays(-25),
                        IsActive = true
                    },
                    new Models.Citizen
                    {
                        CNIC = "1111222233334",
                        FirstName = "Hassan",
                        LastName = "Sheikh",
                        FatherName = "Omar Sheikh",
                        DateOfBirth = new DateTime(1992, 12, 10),
                        Gender = "Male",
                        Address = "House 789, Model Town",
                        City = "Lahore",
                        Province = "Punjab",
                        PhoneNumber = "0333-1111222",
                        Email = "hassan.sheikh@email.com",
                        BloodGroup = "O+",
                        MaritalStatus = "Single",
                        Occupation = "Teacher",
                        RegistrationDate = DateTime.Now.AddDays(-20),
                        IsActive = true
                    }
                };

                DatabaseContext!.Citizens.AddRange(citizens);
                DatabaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error seeding database: {ex.Message}", "Database Error", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DatabaseContext?.Dispose();
            base.OnExit(e);
        }
    }
}