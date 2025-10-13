using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace NADRAApp.Services
{
    public class ImageProcessingService
    {
        private readonly string _imagesDirectory;

        public ImageProcessingService()
        {
            _imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            Directory.CreateDirectory(_imagesDirectory);
            Directory.CreateDirectory(Path.Combine(_imagesDirectory, "Citizens"));
            Directory.CreateDirectory(Path.Combine(_imagesDirectory, "Fingerprints"));
            Directory.CreateDirectory(Path.Combine(_imagesDirectory, "Processed"));
        }

        public BitmapImage LoadImageFromPath(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                return CreatePlaceholderImage();
            }

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
            catch
            {
                return CreatePlaceholderImage();
            }
        }

        public string SaveCitizenPhoto(string sourceImagePath, string cnic)
        {
            try
            {
                var extension = Path.GetExtension(sourceImagePath);
                var fileName = $"citizen_{cnic}_{DateTime.Now:yyyyMMdd_HHmmss}{extension}";
                var destinationPath = Path.Combine(_imagesDirectory, "Citizens", fileName);
                
                File.Copy(sourceImagePath, destinationPath, true);
                return destinationPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save citizen photo: {ex.Message}");
            }
        }

        public string SaveFingerprintImage(string sourceImagePath, string cnic)
        {
            try
            {
                var extension = Path.GetExtension(sourceImagePath);
                var fileName = $"fingerprint_{cnic}_{DateTime.Now:yyyyMMdd_HHmmss}{extension}";
                var destinationPath = Path.Combine(_imagesDirectory, "Fingerprints", fileName);
                
                File.Copy(sourceImagePath, destinationPath, true);
                return destinationPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save fingerprint image: {ex.Message}");
            }
        }

        public BitmapImage ApplyVisualEffects(string imagePath, string effectType = "blur")
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                return CreatePlaceholderImage();
            }

            try
            {
                // For now, return the original image
                // In full implementation, this would use OpenCV to apply effects
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                
                // Save processed image
                var processedPath = SaveProcessedImage(bitmap, effectType);
                return LoadImageFromPath(processedPath);
            }
            catch
            {
                return CreatePlaceholderImage();
            }
        }

        public BitmapImage DetectFaces(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                return CreatePlaceholderImage();
            }

            try
            {
                // For now, return the original image
                // In full implementation, this would use OpenCV face detection
                return LoadImageFromPath(imagePath);
            }
            catch
            {
                return CreatePlaceholderImage();
            }
        }

        public string ResizeAndSaveImage(string sourceImagePath, int width, int height, string cnic)
        {
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(sourceImagePath, UriKind.Absolute);
                bitmap.DecodePixelWidth = width;
                bitmap.DecodePixelHeight = height;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();

                var extension = Path.GetExtension(sourceImagePath);
                var fileName = $"resized_{cnic}_{width}x{height}_{DateTime.Now:yyyyMMdd_HHmmss}{extension}";
                var destinationPath = Path.Combine(_imagesDirectory, "Citizens", fileName);

                SaveBitmapImageToFile(bitmap, destinationPath);
                return destinationPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to resize image: {ex.Message}");
            }
        }

        private string SaveProcessedImage(BitmapImage bitmap, string effectType)
        {
            var fileName = $"processed_{effectType}_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
            var destinationPath = Path.Combine(_imagesDirectory, "Processed", fileName);
            
            SaveBitmapImageToFile(bitmap, destinationPath);
            return destinationPath;
        }

        private void SaveBitmapImageToFile(BitmapImage bitmap, string filePath)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            
            using var fileStream = new FileStream(filePath, FileMode.Create);
            encoder.Save(fileStream);
        }

        private BitmapImage CreatePlaceholderImage()
        {
            // Return null - will show empty image space
            return null;
        }

        public void DeleteImage(string? imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    File.Delete(imagePath);
                }
                catch
                {
                    // Ignore deletion errors
                }
            }
        }
    }
}