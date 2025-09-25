@echo off
echo Lab Exercise #2 - All Tasks
echo ============================

echo.
echo Compiling all exercises...
csc Exercise2_1_TaxCalculator.cs
csc Exercise2_2_Calculator.cs
csc Exercise2_3_PatternX.cs
csc Exercise2_4_NumberPattern.cs

echo.
echo Choose an exercise to run:
echo 1. Exercise 2.1 - Tax Calculator
echo 2. Exercise 2.2 - Calculator Menu
echo 3. Exercise 2.3 - Pattern with 'x'
echo 4. Exercise 2.4 - Number Pattern
echo 5. Exit

set /p choice="Enter your choice (1-5): "

if "%choice%"=="1" (
    echo.
    echo Running Exercise 2.1 - Tax Calculator
    Exercise2_1_TaxCalculator.exe
) else if "%choice%"=="2" (
    echo.
    echo Running Exercise 2.2 - Calculator Menu
    Exercise2_2_Calculator.exe
) else if "%choice%"=="3" (
    echo.
    echo Running Exercise 2.3 - Pattern with 'x'
    Exercise2_3_PatternX.exe
) else if "%choice%"=="4" (
    echo.
    echo Running Exercise 2.4 - Number Pattern
    Exercise2_4_NumberPattern.exe
) else if "%choice%"=="5" (
    echo Goodbye!
) else (
    echo Invalid choice!
)

pause