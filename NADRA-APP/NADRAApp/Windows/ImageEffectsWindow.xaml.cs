using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NADRAApp.Services;

namespace NADRAApp.Windows
{
    public partial class ImageEffectsWindow : Window
    {
        private readonly ImageProcessingService _imageService;
        private string? _originalImagePath;
        private BitmapImage? _processedImage;

        public ImageEffectsWindow()
        {
            InitializeComponent();
            _imageService = new ImageProcessingService();
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an image file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _originalImagePath = openFileDialog.FileName;
                    
                    var bitmap = _imageService.LoadImageFromPath(_originalImagePath);
                    OriginalImage.Source = bitmap;
                    ProcessedImage.Source = bitmap;
                    
                    StatusText.Text = $"Image loaded: {Path.GetFileName(openFileDialog.FileName)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void ApplyEffect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_originalImagePath))
            {
                MessageBox.Show("Please load an image first.", "No Image", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ProcessingProgressBar.Visibility = Visibility.Visible;
                ProcessingProgressBar.IsIndeterminate = true;
                StatusText.Text = "Processing image...";

                var selectedEffect = ((ComboBoxItem)EffectComboBox.SelectedItem).Content.ToString();
                
                await Task.Run(() =>
                {
                    switch (selectedEffect?.ToLower())
                    {
                        case "original":
                            _processedImage = _imageService.LoadImageFromPath(_originalImagePath);
                            break;
                        case "blur":
                            _processedImage = _imageService.ApplyVisualEffects(_originalImagePath, "blur");
                            break;
                        case "edge detection":
                            _processedImage = _imageService.ApplyVisualEffects(_originalImagePath, "edge");
                            break;
                        case "emboss":
                            _processedImage = _imageService.ApplyVisualEffects(_originalImagePath, "emboss");
                            break;
                        case "sepia":
                            _processedImage = _imageService.ApplyVisualEffects(_originalImagePath, "sepia");
                            break;
                    }
                });

                ProcessedImage.Source = _processedImage;
                StatusText.Text = $"Applied {selectedEffect} effect";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing image: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Error processing image";
            }
            finally
            {
                ProcessingProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        private async void DetectFaces_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_originalImagePath))
            {
                MessageBox.Show("Please load an image first.", "No Image", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ProcessingProgressBar.Visibility = Visibility.Visible;
                ProcessingProgressBar.IsIndeterminate = true;
                StatusText.Text = "Detecting faces...";

                await Task.Run(() =>
                {
                    _processedImage = _imageService.DetectFaces(_originalImagePath);
                });

                ProcessedImage.Source = _processedImage;
                StatusText.Text = "Face detection completed";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error detecting faces: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Error detecting faces";
            }
            finally
            {
                ProcessingProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            if (_processedImage == null)
            {
                MessageBox.Show("No processed image to save.", "No Image", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JPEG files (*.jpg)|*.jpg|PNG files (*.png)|*.png",
                Title = "Save processed image"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var encoder = saveFileDialog.FilterIndex == 1 ? 
                        (BitmapEncoder)new JpegBitmapEncoder() : 
                        new PngBitmapEncoder();
                    
                    encoder.Frames.Add(BitmapFrame.Create(_processedImage));
                    
                    using var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    encoder.Save(fileStream);
                    
                    StatusText.Text = $"Image saved: {Path.GetFileName(saveFileDialog.FileName)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}", "Error", 
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}