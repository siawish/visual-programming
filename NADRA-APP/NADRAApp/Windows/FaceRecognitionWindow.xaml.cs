using Microsoft.Win32;
using NADRAApp.Data;
using NADRAApp.Models;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NADRAApp.Windows
{
    public class CitizenFaceData
    {
        public Citizen Citizen { get; set; } = new Citizen();
        public OpenCvSharp.Rect FaceRect { get; set; }
        public double MatchConfidence { get; set; }
    }

    public partial class FaceRecognitionWindow : System.Windows.Window
    {
        private readonly NADRAContext _context;
        private Mat? queryImage;
        private string? currentImagePath;
        private List<CitizenFaceData> citizenFaceDatabase = new List<CitizenFaceData>();
        private CascadeClassifier? faceCascade;

        public FaceRecognitionWindow()
        {
            InitializeComponent();
            _context = new NADRAContext();
            InitializeFaceDetection();
            LoadCitizenDatabase();
        }

        private void InitializeFaceDetection()
        {
            try
            {
                string cascadePath = GetHaarCascadePath();
                if (!string.IsNullOrEmpty(cascadePath))
                {
                    faceCascade = new CascadeClassifier(cascadePath);
                    if (faceCascade.Empty())
                    {
                        faceCascade?.Dispose();
                        faceCascade = null;
                        StatusText.Text = "⚠️ Face detection not available - cascade file error";
                    }
                }
                else
                {
                    StatusText.Text = "⚠️ Face detection not available - cascade file missing";
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"⚠️ Face detection error: {ex.Message}";
            }
        }

        private string GetHaarCascadePath()
        {
            string[] possiblePaths = {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "haarcascade_frontalface_alt.xml"),
                Path.Combine(Directory.GetCurrentDirectory(), "Data", "haarcascade_frontalface_alt.xml"),
                @"Data\haarcascade_frontalface_alt.xml"
            };

            return possiblePaths.FirstOrDefault(File.Exists) ?? string.Empty;
        }

        private async void LoadCitizenDatabase()
        {
            try
            {
                DatabaseStatusText.Text = "Database: Loading citizen records...";
                
                await Task.Run(() =>
                {
                    var citizens = _context.Citizens.ToList();
                    
                    foreach (var citizen in citizens)
                    {
                        if (!string.IsNullOrEmpty(citizen.PhotoPath) && File.Exists(citizen.PhotoPath))
                        {
                            try
                            {
                                var faces = DetectFaces(citizen.PhotoPath);
                                if (faces.Length > 0)
                                {
                                    citizenFaceDatabase.Add(new CitizenFaceData
                                    {
                                        Citizen = citizen,
                                        FaceRect = faces[0] // Use first detected face
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing citizen {citizen.FirstName} {citizen.LastName}: {ex.Message}");
                            }
                        }
                    }
                });

                DatabaseStatusText.Text = $"Database: {citizenFaceDatabase.Count} citizens with photos loaded";
            }
            catch (Exception ex)
            {
                DatabaseStatusText.Text = $"Database error: {ex.Message}";
            }
        }

        private OpenCvSharp.Rect[] DetectFaces(string imagePath)
        {
            if (faceCascade == null || faceCascade.Empty())
            {
                return new OpenCvSharp.Rect[0];
            }

            using Mat image = new Mat(imagePath);
            return DetectFaces(image);
        }

        private OpenCvSharp.Rect[] DetectFaces(Mat image)
        {
            if (faceCascade == null || faceCascade.Empty())
            {
                return new OpenCvSharp.Rect[0];
            }

            using Mat grayImage = new Mat();
            Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            
            var faces = faceCascade.DetectMultiScale(
                grayImage,
                scaleFactor: 1.1,
                minNeighbors: 3,
                flags: HaarDetectionTypes.ScaleImage,
                minSize: new OpenCvSharp.Size(30, 30)
            );
            
            return faces;
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
                Title = "Select a photo for face recognition"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    currentImagePath = openFileDialog.FileName;
                    queryImage = new Mat(currentImagePath);
                    
                    QueryImage.Source = queryImage.ToBitmapSource();
                    SearchButton.IsEnabled = true;
                    
                    ClearResults();
                    StatusText.Text = "✅ Photo loaded successfully. Click 'Search Database' to find matches.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                    StatusText.Text = "❌ Error loading photo";
                }
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (queryImage == null)
            {
                MessageBox.Show("Please load a photo first.", "No Photo", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (citizenFaceDatabase.Count == 0)
            {
                MessageBox.Show("No citizen records with photos found in database.", "Empty Database", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                SearchButton.IsEnabled = false;
                StatusText.Text = "🔍 Searching database for face matches...";
                ClearResults();

                await Task.Run(() =>
                {
                    var queryFaces = DetectFaces(queryImage);
                    
                    if (queryFaces.Length == 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            StatusText.Text = "❌ No faces detected in the uploaded photo";
                            DisplayNoFacesFound();
                        });
                        return;
                    }

                    var matches = new List<CitizenFaceData>();
                    
                    foreach (var queryFace in queryFaces)
                    {
                        var bestMatch = FindBestMatch(queryImage, queryFace);
                        if (bestMatch != null)
                        {
                            matches.Add(bestMatch);
                        }
                    }

                    Dispatcher.Invoke(() =>
                    {
                        if (matches.Count > 0)
                        {
                            StatusText.Text = $"✅ Found {matches.Count} potential match(es) in database";
                            DisplayMatches(matches);
                        }
                        else
                        {
                            StatusText.Text = "❌ No matches found in database (confidence below 65%)";
                            DisplayNoMatches();
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during face recognition: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "❌ Face recognition error";
            }
            finally
            {
                SearchButton.IsEnabled = true;
            }
        }

        private CitizenFaceData? FindBestMatch(Mat queryImage, OpenCvSharp.Rect queryFace)
        {
            double bestSimilarity = 0;
            CitizenFaceData? bestMatch = null;
            
            using Mat queryFaceRegion = new Mat(queryImage, queryFace);
            using Mat queryProcessed = PreprocessFace(queryFaceRegion);
            
            foreach (var citizenFace in citizenFaceDatabase)
            {
                try
                {
                    using Mat citizenImage = new Mat(citizenFace.Citizen.PhotoPath);
                    using Mat citizenFaceRegion = new Mat(citizenImage, citizenFace.FaceRect);
                    using Mat citizenProcessed = PreprocessFace(citizenFaceRegion);
                    
                    double templateSimilarity = CalculateTemplateSimilarity(queryProcessed, citizenProcessed);
                    double histogramSimilarity = CalculateHistogramSimilarity(queryProcessed, citizenProcessed);
                    double structuralSimilarity = CalculateStructuralSimilarity(queryProcessed, citizenProcessed);
                    
                    double combinedSimilarity = (templateSimilarity * 0.4) + 
                                              (histogramSimilarity * 0.3) + 
                                              (structuralSimilarity * 0.3);
                    
                    if (combinedSimilarity > bestSimilarity)
                    {
                        bestSimilarity = combinedSimilarity;
                        bestMatch = citizenFace;
                        bestMatch.MatchConfidence = combinedSimilarity;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing citizen {citizenFace.Citizen.FirstName} {citizenFace.Citizen.LastName}: {ex.Message}");
                }
            }
            
            return bestSimilarity > 0.65 ? bestMatch : null;
        }

        private Mat PreprocessFace(Mat faceRegion)
        {
            Mat processed = new Mat();
            using Mat gray = new Mat();
            
            Cv2.CvtColor(faceRegion, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Resize(gray, processed, new OpenCvSharp.Size(128, 128));
            Cv2.EqualizeHist(processed, processed);
            Cv2.GaussianBlur(processed, processed, new OpenCvSharp.Size(3, 3), 0);
            
            return processed;
        }

        private double CalculateTemplateSimilarity(Mat query, Mat database)
        {
            using Mat result = new Mat();
            Cv2.MatchTemplate(query, database, result, TemplateMatchModes.CCoeffNormed);
            Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out _);
            return Math.Max(0, maxVal);
        }

        private double CalculateHistogramSimilarity(Mat query, Mat database)
        {
            using Mat queryHist = new Mat();
            using Mat dbHist = new Mat();
            
            int[] histSize = { 256 };
            Rangef[] ranges = { new Rangef(0, 256) };
            
            Cv2.CalcHist(new Mat[] { query }, new int[] { 0 }, null, queryHist, 1, histSize, ranges);
            Cv2.CalcHist(new Mat[] { database }, new int[] { 0 }, null, dbHist, 1, histSize, ranges);
            
            Cv2.Normalize(queryHist, queryHist, 0, 1, NormTypes.MinMax);
            Cv2.Normalize(dbHist, dbHist, 0, 1, NormTypes.MinMax);
            
            double similarity = Cv2.CompareHist(queryHist, dbHist, HistCompMethods.Correl);
            return Math.Max(0, similarity);
        }

        private double CalculateStructuralSimilarity(Mat query, Mat database)
        {
            using Mat diff = new Mat();
            Cv2.Absdiff(query, database, diff);
            
            Scalar meanError = Cv2.Mean(diff);
            double mse = meanError.Val0;
            
            return 1.0 / (1.0 + mse / 255.0);
        }

        private void DisplayMatches(List<CitizenFaceData> matches)
        {
            ResultsPanel.Children.Clear();
            
            foreach (var match in matches.OrderByDescending(m => m.MatchConfidence))
            {
                var matchPanel = CreateMatchPanel(match);
                ResultsPanel.Children.Add(matchPanel);
            }
        }

        private StackPanel CreateMatchPanel(CitizenFaceData match)
        {
            var panel = new StackPanel { Margin = new Thickness(0, 0, 0, 15) };
            
            // Match header with confidence
            var headerPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
            
            var matchIcon = new TextBlock 
            { 
                Text = "✅", 
                FontSize = 16, 
                Margin = new Thickness(0, 0, 5, 0) 
            };
            headerPanel.Children.Add(matchIcon);
            
            var confidenceText = new TextBlock 
            { 
                Text = $"Match: {match.MatchConfidence:P1} confidence",
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Green,
                FontSize = 14
            };
            headerPanel.Children.Add(confidenceText);
            
            panel.Children.Add(headerPanel);
            
            // Citizen photo
            try
            {
                using Mat citizenImage = new Mat(match.Citizen.PhotoPath);
                using Mat faceRegion = new Mat(citizenImage, match.FaceRect);
                
                var citizenPhoto = new Image 
                { 
                    Source = faceRegion.ToBitmapSource(),
                    Width = 120,
                    Height = 120,
                    Margin = new Thickness(0, 0, 0, 10)
                };
                panel.Children.Add(citizenPhoto);
            }
            catch (Exception ex)
            {
                var errorText = new TextBlock 
                { 
                    Text = "Photo unavailable",
                    FontStyle = FontStyles.Italic,
                    Foreground = Brushes.Gray
                };
                panel.Children.Add(errorText);
            }
            
            // Citizen basic info
            var nameText = new TextBlock 
            { 
                Text = $"{match.Citizen.FirstName} {match.Citizen.LastName}",
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 5)
            };
            panel.Children.Add(nameText);
            
            var cnicText = new TextBlock 
            { 
                Text = $"CNIC: {match.Citizen.CNIC}",
                FontSize = 12,
                Foreground = Brushes.DarkBlue,
                Margin = new Thickness(0, 0, 0, 5)
            };
            panel.Children.Add(cnicText);
            
            // View details button
            var detailsButton = new Button 
            { 
                Content = "📋 View Full Details",
                Background = new SolidColorBrush(Color.FromRgb(45, 106, 79)),
                Foreground = Brushes.White,
                Padding = new Thickness(10, 5, 10, 5),
                Margin = new Thickness(0, 10, 0, 0),
                Tag = match.Citizen
            };
            detailsButton.Click += DetailsButton_Click;
            panel.Children.Add(detailsButton);
            
            // Add separator
            var separator = new Border 
            { 
                Height = 1,
                Background = Brushes.LightGray,
                Margin = new Thickness(0, 15, 0, 0)
            };
            panel.Children.Add(separator);
            
            return panel;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Citizen citizen)
            {
                DisplayCitizenDetails(citizen);
            }
        }

        private void DisplayCitizenDetails(Citizen citizen)
        {
            CitizenDetailsPanel.Children.Clear();
            
            // Citizen photo
            if (!string.IsNullOrEmpty(citizen.PhotoPath) && File.Exists(citizen.PhotoPath))
            {
                try
                {
                    var photoImage = new Image();
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(citizen.PhotoPath, UriKind.Absolute);
                    bitmap.DecodePixelWidth = 200;
                    bitmap.EndInit();
                    photoImage.Source = bitmap;
                    photoImage.Width = 200;
                    photoImage.Height = 200;
                    photoImage.Margin = new Thickness(0, 0, 0, 15);
                    CitizenDetailsPanel.Children.Add(photoImage);
                }
                catch (Exception ex)
                {
                    var errorText = new TextBlock 
                    { 
                        Text = "Photo unavailable",
                        FontStyle = FontStyles.Italic,
                        Foreground = Brushes.Gray,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 0, 0, 15)
                    };
                    CitizenDetailsPanel.Children.Add(errorText);
                }
            }
            
            // Citizen details
            var details = new Dictionary<string, string>
            {
                { "👤 Full Name", $"{citizen.FirstName} {citizen.LastName}" },
                { "👨 Father Name", citizen.FatherName ?? "Not provided" },
                { "🆔 CNIC", citizen.CNIC },
                { "📧 Email", citizen.Email ?? "Not provided" },
                { "📱 Phone", citizen.PhoneNumber ?? "Not provided" },
                { "🏠 Address", citizen.Address ?? "Not provided" },
                { "🎂 Date of Birth", citizen.DateOfBirth.ToString("dd/MM/yyyy") },
                { "⚧ Gender", citizen.Gender ?? "Not specified" },
                { "🏙️ City", citizen.City ?? "Not provided" },
                { "🗺️ Province", citizen.Province ?? "Not provided" },
                { "🩸 Blood Group", citizen.BloodGroup ?? "Not provided" },
                { "💍 Marital Status", citizen.MaritalStatus ?? "Not provided" },
                { "💼 Occupation", citizen.Occupation ?? "Not provided" },
                { "📅 Registration Date", citizen.RegistrationDate.ToString("dd/MM/yyyy HH:mm") }
            };
            
            foreach (var detail in details)
            {
                var detailPanel = new StackPanel { Margin = new Thickness(0, 0, 0, 10) };
                
                var labelText = new TextBlock 
                { 
                    Text = detail.Key,
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 12,
                    Foreground = Brushes.DarkSlateGray
                };
                detailPanel.Children.Add(labelText);
                
                var valueText = new TextBlock 
                { 
                    Text = detail.Value,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 0),
                    TextWrapping = TextWrapping.Wrap
                };
                detailPanel.Children.Add(valueText);
                
                CitizenDetailsPanel.Children.Add(detailPanel);
            }
        }

        private void DisplayNoFacesFound()
        {
            ResultsPanel.Children.Clear();
            
            var noFacesPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 50, 0, 0) };
            
            var icon = new TextBlock 
            { 
                Text = "😔",
                FontSize = 48,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            noFacesPanel.Children.Add(icon);
            
            var message = new TextBlock 
            { 
                Text = "No faces detected in the uploaded photo",
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            noFacesPanel.Children.Add(message);
            
            var suggestion = new TextBlock 
            { 
                Text = "Please try uploading a clearer photo with visible faces",
                FontSize = 12,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };
            noFacesPanel.Children.Add(suggestion);
            
            ResultsPanel.Children.Add(noFacesPanel);
        }

        private void DisplayNoMatches()
        {
            ResultsPanel.Children.Clear();
            
            var noMatchPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 50, 0, 0) };
            
            var icon = new TextBlock 
            { 
                Text = "🔍",
                FontSize = 48,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            noMatchPanel.Children.Add(icon);
            
            var message = new TextBlock 
            { 
                Text = "No matches found in database",
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            noMatchPanel.Children.Add(message);
            
            var suggestion = new TextBlock 
            { 
                Text = "The person may not be registered in the NADRA database\nor the photo quality may be insufficient for recognition",
                FontSize = 12,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center
            };
            noMatchPanel.Children.Add(suggestion);
            
            ResultsPanel.Children.Add(noMatchPanel);
        }

        private void ClearResults()
        {
            ResultsPanel.Children.Clear();
            CitizenDetailsPanel.Children.Clear();
            
            var placeholderText = new TextBlock 
            { 
                Text = "Search results will appear here",
                FontStyle = FontStyles.Italic,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 50, 0, 0)
            };
            ResultsPanel.Children.Add(placeholderText);
            
            var detailsPlaceholder = new TextBlock 
            { 
                Text = "Select a match to view citizen details",
                FontStyle = FontStyles.Italic,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 50, 0, 0)
            };
            CitizenDetailsPanel.Children.Add(detailsPlaceholder);
        }

        private void RefreshDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            citizenFaceDatabase.Clear();
            LoadCitizenDatabase();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            QueryImage.Source = null;
            queryImage?.Dispose();
            queryImage = null;
            currentImagePath = null;
            SearchButton.IsEnabled = false;
            ClearResults();
            StatusText.Text = "Load a photo to begin face recognition";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            queryImage?.Dispose();
            faceCascade?.Dispose();
            _context?.Dispose();
            base.OnClosed(e);
        }
    }
}