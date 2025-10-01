# Lab Exercise 5 - Visual Programming

This project contains all 5 exercises from Lab Exercise 5, implementing Windows Forms applications in C#.

## Exercises Implemented

### Exercise 1: Form Load and Close Events
- **File**: `Exercise1_FormLoadClose.cs`
- **Features**: 
  - Shows a message when form loads
  - Confirms before closing the form
  - Uses Form_Load and FormClosing events

### Exercise 2: Hide Form
- **File**: `Exercise2_HideForm.cs`
- **Features**:
  - Hide button to make form invisible
  - Show button to make form visible again
  - Demonstrates form visibility control

### Exercise 3: Circle Calculator
- **File**: `Exercise3_CircleCalculator.cs`
- **Features**:
  - Input field for radius
  - Calculates area using formula: π × r²
  - Calculates circumference using formula: 2 × π × r
  - Displays results in labels
  - Error handling for invalid input

### Exercise 4: Table Calculator
- **File**: `Exercise4_TableCalculator.cs`
- **Features**:
  - Input field for number
  - Generates multiplication table (1-10)
  - Displays table in a ListBox
  - Error handling for invalid input

### Exercise 5: Temperature Converter
- **File**: `Exercise5_TemperatureConverter.cs`
- **Features**:
  - Input field for Fahrenheit temperature
  - Converts to Celsius using formula: (F - 32) × (5/9)
  - Displays converted temperature
  - Error handling for invalid input

## How to Run

1. Make sure you have .NET 6.0 or later installed
2. Open command prompt in the project directory
3. Run the following commands:

```bash
dotnet build
dotnet run
```

## Project Structure

- `Program.cs` - Main entry point with menu form
- `Exercise1_FormLoadClose.cs` - Exercise 1 implementation
- `Exercise2_HideForm.cs` - Exercise 2 implementation
- `Exercise3_CircleCalculator.cs` - Exercise 3 implementation
- `Exercise4_TableCalculator.cs` - Exercise 4 implementation
- `Exercise5_TemperatureConverter.cs` - Exercise 5 implementation
- `LabExercise5.csproj` - Project configuration file

## Features

- Clean, professional Windows Forms interface
- Proper error handling and validation
- User-friendly message boxes
- Consistent styling across all forms
- Main menu for easy navigation between exercises

## Technical Details

- **Framework**: .NET 6.0 Windows Forms
- **Language**: C#
- **IDE Compatibility**: Visual Studio, VS Code, Rider
- **Platform**: Windows

All exercises follow Windows Forms best practices and include proper event handling, input validation, and user feedback.