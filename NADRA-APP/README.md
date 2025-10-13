# NADRA Database Management System

## Overview
A comprehensive .NET WPF application designed to manage citizen records for the National Database and Registration Authority (NADRA). The application features a modern, professional interface with real-time clock, instant logout functionality, advanced OpenCV image processing, face recognition capabilities, and comprehensive reporting system.

## Features

### Core Functionality
- **Citizen Management**: Complete CRUD operations for citizen records
- **Advanced Search**: Multi-field search and filtering capabilities
- **User Authentication**: Secure login system with role-based access
- **Data Validation**: Comprehensive input validation and CNIC verification

### OpenCV Integration
- **Face Recognition**: Advanced face recognition system with real-time camera feed
- **Image Effects**: Apply various visual effects (blur, edge detection, emboss, sepia)
- **Face Detection**: Automatic face detection in citizen photos using Haar cascades
- **Image Processing**: Resize, enhance, and manipulate images
- **Real-time Processing**: Live image processing with preview
- **Camera Integration**: Live camera feed for face recognition and photo capture

### Reporting System
- **Multiple Report Types**: Citizens by gender, province, age group, registration statistics
- **Export Options**: PDF, Excel, and CSV export capabilities
- **Statistical Analysis**: Comprehensive statistics and data visualization
- **Date Range Filtering**: Generate reports for specific time periods

### UI/UX Features
- **Modern WPF Interface**: Clean, professional design with custom styling and green theme
- **Real-time Clock**: Live 12-hour format clock with AM/PM display in header
- **Instant Logout**: One-click logout without confirmation dialogs
- **Responsive Layout**: Adaptive interface that works on different screen sizes
- **Professional Header**: Clean header with NADRA branding, clock, admin info, and logout
- **Progress Indicators**: Real-time feedback for long-running operations
- **Status Updates**: Comprehensive status bar with citizen count, connection status, and copyright
- **Smooth Animations**: Loading states and transitions for better user experience

## Technical Architecture

### Technologies Used
- **.NET 8.0**: Latest .NET framework for optimal performance
- **WPF (Windows Presentation Foundation)**: Rich desktop application framework
- **Entity Framework Core**: Modern ORM for database operations
- **OpenCV Sharp**: Computer vision and image processing
- **BCrypt.NET**: Secure password hashing
- **SQL Server LocalDB**: Embedded database for development

### Project Structure
```
NADRAApp/
├── Models/
│   ├── Citizen.cs          # Citizen entity model
│   └── User.cs             # User authentication model
├── Data/
│   ├── NADRAContext.cs     # Entity Framework context
│   └── haarcascade_frontalface_alt.xml # Face detection model
├── Services/
│   ├── CitizenService.cs   # Business logic for citizen operations
│   └── ImageProcessingService.cs # OpenCV image processing
├── ViewModels/
│   └── MainViewModel.cs    # MVVM pattern implementation
├── Windows/
│   ├── MainWindow.xaml     # Main application window with real-time clock
│   ├── AddEditCitizenWindow.xaml # Citizen data entry and photo management
│   ├── ImageEffectsWindow.xaml   # OpenCV image effects demo
│   ├── FaceRecognitionWindow.xaml # Face recognition with camera feed
│   └── ReportsWindow.xaml  # Comprehensive reporting interface
├── Converters/
│   └── ImagePathConverter.cs # Image path conversion utilities
└── App.xaml               # Application configuration

Root Directory/
├── NADRAApp/              # Main application folder
├── database_setup.sql     # Database initialization script
├── NADRAApp.sln          # Visual Studio solution file
└── README.md             # Project documentation
```

## Database Schema

### Citizens Table
- **Id**: Primary key (auto-increment)
- **CNIC**: Unique 13-digit identifier
- **Personal Info**: Name, father's name, DOB, gender
- **Contact Info**: Address, city, province, phone, email
- **Additional Info**: Blood group, marital status, occupation
- **Biometric Data**: Photo and fingerprint (binary data)
- **Metadata**: Registration date, active status

### Users Table
- **Id**: Primary key (auto-increment)
- **Username**: Unique login identifier
- **PasswordHash**: BCrypt hashed password
- **Profile Info**: Full name, email, role
- **Metadata**: Creation date, last login, active status

## Installation & Setup

### Prerequisites
- Windows 10/11
- .NET 8.0 SDK
- Visual Studio 2022 (recommended)
- SQL Server LocalDB

### Installation Steps
1. Clone or download the project
2. Open `NADRAApp.sln` in Visual Studio
3. Restore NuGet packages (Entity Framework, OpenCV, etc.)
4. Build the solution
5. Run the NADRAApp project
6. Database will be created automatically on first run

### Default Login Credentials
- **Username**: admin
- **Password**: admin123

## Usage Guide

### Getting Started
1. Launch the application
2. Login with default credentials (admin/admin123)
3. The main dashboard displays all registered citizens with real-time clock
4. Use the search functionality to find specific records
5. Notice the professional header with live clock showing current time in 12-hour format

### Adding New Citizens
1. Click "Add Citizen" button
2. Fill in all required fields (marked with *)
3. Upload citizen photo (optional)
4. Save the record

### Face Recognition System
1. Click "Face Recognition" button
2. Allow camera access when prompted
3. Position face in front of camera
4. System will detect and recognize faces in real-time
5. Compare with stored citizen photos for identification

### Image Processing Demo
1. Click "Image Effects" button
2. Load an image file
3. Select desired effect from dropdown
4. Apply effect and view results
5. Save processed image

### Generating Reports
1. Click "Reports" button
2. Select report type and date range
3. Click "Generate Report"
4. View results in data grid or statistics tab
5. Export to desired format (CSV, PDF, Excel)

## Key Features Demonstration

### Maximum Toolbox Utilization
The application demonstrates extensive use of WPF controls:
- **DataGrid**: For displaying citizen records
- **ComboBox**: For dropdowns (gender, province, etc.)
- **DatePicker**: For date selection
- **TextBox/PasswordBox**: For data input
- **Button**: Various action buttons with custom styling
- **Image**: For photo display and processing
- **ProgressBar**: For operation feedback
- **StatusBar**: For status information
- **TabControl**: For organizing report views
- **GroupBox**: For logical grouping
- **Border**: For visual separation
- **StackPanel/Grid**: For layout management

### OpenCV Visual Effects & Face Recognition
The application includes dedicated modules showcasing:
- **Face Recognition**: Real-time face recognition using camera feed
- **Face Detection**: Haar cascade face detection in images
- **Gaussian Blur**: Smooth image blurring
- **Edge Detection**: Canny edge detection algorithm
- **Emboss Effect**: 3D embossed appearance
- **Sepia Tone**: Vintage photo effect
- **Live Camera Feed**: Real-time video processing and analysis

### Advanced Database Operations
- **Entity Framework Integration**: Modern ORM approach
- **LINQ Queries**: Type-safe database queries
- **Async Operations**: Non-blocking database operations
- **Data Validation**: Comprehensive input validation
- **Relationship Management**: Foreign key relationships

## Security Features
- **Password Hashing**: BCrypt for secure password storage
- **Input Validation**: Prevents SQL injection and XSS
- **Role-Based Access**: Different user roles and permissions
- **Data Encryption**: Sensitive data protection

## Performance Optimizations
- **Async/Await Pattern**: Non-blocking UI operations
- **Lazy Loading**: Efficient data loading strategies
- **Image Optimization**: Automatic image resizing
- **Memory Management**: Proper disposal of resources

## Recent Updates (2025)
- **Real-time Clock**: Added live 12-hour format clock with AM/PM in header
- **Instant Logout**: Removed confirmation dialog for seamless logout experience
- **Face Recognition**: Implemented advanced face recognition with camera integration
- **Professional UI**: Enhanced header design with proper spacing and modern styling
- **Updated Copyright**: Application now shows "NADRA © 2025"
- **Improved UX**: Streamlined user interactions and faster navigation

## Future Enhancements
- **Biometric Scanner Integration**: Real fingerprint scanning
- **Advanced Face Recognition**: Machine learning-based face matching
- **Advanced Reporting**: Charts and graphs with data visualization
- **Multi-language Support**: Localization for multiple languages
- **Cloud Integration**: Azure/AWS connectivity for data synchronization
- **Mobile App**: Companion mobile application
- **AI Integration**: Artificial intelligence for data analysis and predictions

## Troubleshooting

### Common Issues
1. **Database Connection**: Ensure SQL Server LocalDB is installed
2. **OpenCV Errors**: Verify OpenCV runtime is properly installed
3. **Camera Access**: Allow camera permissions for face recognition features
4. **Image Loading**: Check image file formats and permissions
5. **Performance**: Close unnecessary applications for better performance
6. **Clock Display**: Clock updates every second and shows current system time

### Support
For technical support or questions, please refer to the documentation or contact the development team.

## Application Screenshots

<img width="1917" height="1078" alt="image" src="https://github.com/user-attachments/assets/3ce968cd-ce10-4eae-8d4d-bf611c126b6f" />

<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/ea4db9dc-33da-4d46-be8a-cdb9ecf45a66" />

<img width="1174" height="1037" alt="image" src="https://github.com/user-attachments/assets/6d73ed59-6f78-48ac-ae51-25314570f19e" />

<img width="1178" height="898" alt="image" src="https://github.com/user-attachments/assets/c2c70e57-2d8e-4a40-899f-7086b7231e1b" />

<img width="1779" height="1044" alt="image" src="https://github.com/user-attachments/assets/6fa8ad64-cf9f-4a8b-bf70-3a085449bd87" />

<img width="1787" height="1040" alt="image" src="https://github.com/user-attachments/assets/10935d10-36a3-4bb9-900f-cc9c2a33b84e" />


### Main Interface
- Professional header with NADRA branding
- Real-time clock displaying current time (12-hour format with AM/PM)
- Administrator information and instant logout button
- Clean, modern design with green theme

### Key Features Showcase
- **Login Screen**: Secure authentication with professional branding
- **Citizen Management**: Complete CRUD operations with photo support
- **Face Recognition**: Real-time camera feed with face detection
- **Image Effects**: OpenCV-powered image processing
- **Reports**: Comprehensive reporting with export capabilities

## License
This project is developed for educational purposes and demonstrates various .NET and WPF technologies including real-time UI updates, OpenCV integration, and modern application design patterns.

---
**NADRA © 2025** - National Database & Registration Authority Management System
