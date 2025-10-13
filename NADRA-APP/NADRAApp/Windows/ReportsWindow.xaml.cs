using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using NADRAApp.Data;
using NADRAApp.Models;

namespace NADRAApp.Windows
{
    public partial class ReportsWindow : Window
    {
        private List<object> _currentReportData = new();

        public ReportsWindow()
        {
            InitializeComponent();
            FromDatePicker.SelectedDate = DateTime.Now.AddMonths(-1);
            ToDatePicker.SelectedDate = DateTime.Now;
        }

        private async void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportProgressBar.Visibility = Visibility.Visible;
                ReportProgressBar.IsIndeterminate = true;
                StatusText.Text = "Generating report...";

                using var context = new NADRAContext();
                var selectedReportType = ((System.Windows.Controls.ComboBoxItem)ReportTypeComboBox.SelectedItem).Content.ToString();
                
                await Task.Run(() =>
                {
                    switch (selectedReportType)
                    {
                        case "All Citizens":
                            _currentReportData = context.Citizens.Where(c => c.IsActive).Cast<object>().ToList();
                            break;
                        case "Citizens by Gender":
                            _currentReportData = context.Citizens
                                .Where(c => c.IsActive)
                                .GroupBy(c => c.Gender)
                                .Select(g => new { Gender = g.Key, Count = g.Count() })
                                .Cast<object>().ToList();
                            break;
                        case "Citizens by Province":
                            _currentReportData = context.Citizens
                                .Where(c => c.IsActive)
                                .GroupBy(c => c.Province)
                                .Select(g => new { Province = g.Key, Count = g.Count() })
                                .Cast<object>().ToList();
                            break;
                        case "Citizens by Age Group":
                            var today = DateTime.Today;
                            _currentReportData = context.Citizens
                                .Where(c => c.IsActive)
                                .AsEnumerable()
                                .GroupBy(c => GetAgeGroup(today.Year - c.DateOfBirth.Year))
                                .Select(g => new { AgeGroup = g.Key, Count = g.Count() })
                                .Cast<object>().ToList();
                            break;
                        case "Registration Statistics":
                            _currentReportData = context.Citizens
                                .Where(c => c.IsActive)
                                .GroupBy(c => c.RegistrationDate.Date)
                                .Select(g => new { Date = g.Key, Registrations = g.Count() })
                                .OrderBy(x => x.Date)
                                .Cast<object>().ToList();
                            break;
                    }
                });

                ReportDataGrid.ItemsSource = _currentReportData;
                UpdateStatistics();
                StatusText.Text = $"Report generated successfully. {_currentReportData.Count} records found.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Error generating report";
            }
            finally
            {
                ReportProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateStatistics()
        {
            using var context = new NADRAContext();
            var citizens = context.Citizens.Where(c => c.IsActive).ToList();
            
            TotalCitizensText.Text = $"Total Citizens: {citizens.Count}";
            MaleCitizensText.Text = $"Male Citizens: {citizens.Count(c => c.Gender == "Male")}";
            FemaleCitizensText.Text = $"Female Citizens: {citizens.Count(c => c.Gender == "Female")}";
            
            if (citizens.Any())
            {
                var averageAge = citizens.Average(c => DateTime.Today.Year - c.DateOfBirth.Year);
                AverageAgeText.Text = $"Average Age: {averageAge:F1} years";
            }

            var thisMonth = DateTime.Now.AddDays(-30);
            var newRegistrations = citizens.Count(c => c.RegistrationDate >= thisMonth);
            NewRegistrationsText.Text = $"New Registrations (Last 30 days): {newRegistrations}";

            var provinceStats = citizens
                .GroupBy(c => c.Province)
                .Select(g => new { Province = g.Key, Count = g.Count() })
                .ToList();
            
            ProvinceStatsListBox.ItemsSource = provinceStats;
        }

        private string GetAgeGroup(int age)
        {
            return age switch
            {
                < 18 => "Under 18",
                >= 18 and < 30 => "18-29",
                >= 30 and < 45 => "30-44",
                >= 45 and < 60 => "45-59",
                _ => "60+"
            };
        }

        private void ExportReport_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentReportData.Any())
            {
                MessageBox.Show("Please generate a report first.", "No Data", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedFormat = ((System.Windows.Controls.ComboBoxItem)ExportFormatComboBox.SelectedItem).Content.ToString();
            var saveFileDialog = new SaveFileDialog();

            switch (selectedFormat)
            {
                case "PDF":
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.DefaultExt = "pdf";
                    break;
                case "Excel":
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.DefaultExt = "xlsx";
                    break;
                case "CSV":
                    saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    saveFileDialog.DefaultExt = "csv";
                    break;
            }

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    switch (selectedFormat)
                    {
                        case "CSV":
                            ExportToCsv(saveFileDialog.FileName);
                            break;
                        case "PDF":
                        case "Excel":
                            MessageBox.Show($"{selectedFormat} export functionality would be implemented using appropriate libraries (iTextSharp for PDF, EPPlus for Excel).", 
                                          "Feature Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                    
                    StatusText.Text = $"Report exported to {Path.GetFileName(saveFileDialog.FileName)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting report: {ex.Message}", "Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExportToCsv(string fileName)
        {
            var csv = new StringBuilder();
            
            if (_currentReportData.Any())
            {
                var firstItem = _currentReportData.First();
                var properties = firstItem.GetType().GetProperties();
                
                // Header
                csv.AppendLine(string.Join(",", properties.Select(p => p.Name)));
                
                // Data
                foreach (var item in _currentReportData)
                {
                    var values = properties.Select(p => p.GetValue(item)?.ToString() ?? "").ToArray();
                    csv.AppendLine(string.Join(",", values));
                }
            }
            
            File.WriteAllText(fileName, csv.ToString());
        }
    }
}