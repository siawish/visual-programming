-- NADRA Database Setup Script for XAMPP MySQL
-- Run this in phpMyAdmin or MySQL command line

-- Drop and recreate database to ensure clean state
DROP DATABASE IF EXISTS nadra_database;
CREATE DATABASE nadra_database;
USE nadra_database;

-- Create Citizens table
CREATE TABLE Citizens (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CNIC VARCHAR(13) NOT NULL UNIQUE,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    FatherName VARCHAR(100),
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Address VARCHAR(200),
    City VARCHAR(50),
    Province VARCHAR(50),
    PhoneNumber VARCHAR(15),
    Email VARCHAR(100),
    PhotoPath VARCHAR(500),
    RegistrationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    BloodGroup VARCHAR(50),
    MaritalStatus VARCHAR(20),
    Occupation VARCHAR(100)
);

-- Create Users table
CREATE TABLE IF NOT EXISTS Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Role VARCHAR(20) NOT NULL DEFAULT 'Operator',
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    LastLogin DATETIME
);

-- Insert default admin user (password: admin123)
INSERT INTO Users (Username, PasswordHash, FullName, Email, Role, CreatedDate, IsActive) 
VALUES ('admin', '$2a$11$rQZJKjKjKjKjKjKjKjKjKOeH8.8H8.8H8.8H8.8H8.8H8.8H8.8H8.8H8.', 'System Administrator', 'admin@nadra.gov.pk', 'Administrator', NOW(), TRUE)
ON DUPLICATE KEY UPDATE Username = Username;

-- Insert sample citizen data
INSERT INTO Citizens (CNIC, FirstName, LastName, FatherName, DateOfBirth, Gender, Address, City, Province, PhoneNumber, Email, BloodGroup, MaritalStatus, Occupation, RegistrationDate, IsActive) VALUES
('1234567890123', 'Ahmed', 'Ali', 'Muhammad Ali', '1990-05-15', 'Male', 'House 123, Street 45, Sector G-9', 'Islamabad', 'Islamabad', '0300-1234567', 'ahmed.ali@email.com', 'B+', 'Married', 'Software Engineer', NOW(), TRUE),
('9876543210987', 'Fatima', 'Khan', 'Abdul Khan', '1985-08-22', 'Female', 'Flat 456, Block B, Gulshan-e-Iqbal', 'Karachi', 'Sindh', '0321-9876543', 'fatima.khan@email.com', 'A+', 'Single', 'Doctor', NOW(), TRUE),
('1111222233334', 'Hassan', 'Sheikh', 'Omar Sheikh', '1992-12-10', 'Male', 'House 789, Model Town', 'Lahore', 'Punjab', '0333-1111222', 'hassan.sheikh@email.com', 'O+', 'Single', 'Teacher', NOW(), TRUE),
('5555666677778', 'Ayesha', 'Malik', 'Tariq Malik', '1988-03-18', 'Female', 'House 321, University Town', 'Peshawar', 'KPK', '0345-5555666', 'ayesha.malik@email.com', 'AB+', 'Married', 'Lawyer', NOW(), TRUE),
('9999888877776', 'Omar', 'Farooq', 'Saleem Farooq', '1995-07-05', 'Male', 'House 654, Satellite Town', 'Quetta', 'Balochistan', '0300-9999888', 'omar.farooq@email.com', 'B-', 'Single', 'Engineer', NOW(), TRUE)
ON DUPLICATE KEY UPDATE CNIC = CNIC;

-- Create indexes for better performance
CREATE INDEX idx_citizens_cnic ON Citizens(CNIC);
CREATE INDEX idx_citizens_name ON Citizens(FirstName, LastName);
CREATE INDEX idx_citizens_active ON Citizens(IsActive);
CREATE INDEX idx_users_username ON Users(Username);

-- Show tables
SHOW TABLES;

-- Show sample data
SELECT COUNT(*) as TotalCitizens FROM Citizens;
SELECT COUNT(*) as TotalUsers FROM Users;

SELECT 'Database setup completed successfully!' as Status;