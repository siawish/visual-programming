@echo off
echo ========================================
echo          Lab 3 - Exercise Runner
echo ========================================
echo.

:menu
echo Select an exercise to run:
echo 1. Exercise 3.1 - Odd/Even Numbers Table
echo 2. Exercise 3.2 - Palindrome Checker
echo 3. Exercise 3.3 - Student Records
echo 4. Exercise 3.4 - Bubble Sort
echo 5. Exercise 3.5 - Array Addition
echo 6. Exercise 3.6 - Circle Calculator
echo 7. Run All Exercises
echo 8. Exit
echo.

set /p choice="Enter your choice (1-8): "

if "%choice%"=="1" goto ex1
if "%choice%"=="2" goto ex2
if "%choice%"=="3" goto ex3
if "%choice%"=="4" goto ex4
if "%choice%"=="5" goto ex5
if "%choice%"=="6" goto ex6
if "%choice%"=="7" goto all
if "%choice%"=="8" goto exit

echo Invalid choice. Please try again.
echo.
goto menu

:ex1
echo.
echo Running Exercise 3.1...
cd Exercise3_1
dotnet run
cd ..
echo.
goto menu

:ex2
echo.
echo Running Exercise 3.2...
cd Exercise3_2
dotnet run
cd ..
echo.
goto menu

:ex3
echo.
echo Running Exercise 3.3...
cd Exercise3_3
dotnet run
cd ..
echo.
goto menu

:ex4
echo.
echo Running Exercise 3.4...
cd Exercise3_4
dotnet run
cd ..
echo.
goto menu

:ex5
echo.
echo Running Exercise 3.5...
cd Exercise3_5
dotnet run
cd ..
echo.
goto menu

:ex6
echo.
echo Running Exercise 3.6...
cd Exercise3_6
dotnet run
cd ..
echo.
goto menu

:all
echo.
echo Running all exercises...
echo.

echo === Exercise 3.1 ===
cd Exercise3_1
dotnet run
cd ..

echo.
echo === Exercise 3.2 ===
cd Exercise3_2
dotnet run
cd ..

echo.
echo === Exercise 3.3 ===
cd Exercise3_3
dotnet run
cd ..

echo.
echo === Exercise 3.4 ===
cd Exercise3_4
dotnet run
cd ..

echo.
echo === Exercise 3.5 ===
cd Exercise3_5
dotnet run
cd ..

echo.
echo === Exercise 3.6 ===
cd Exercise3_6
dotnet run
cd ..

echo.
echo All exercises completed!
echo.
goto menu

:exit
echo.
echo Goodbye!
pause