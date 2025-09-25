# Lab Exercise #2 - Console Applications

This folder contains four C# console applications as specified in the lab manual.

## Exercise 2.1 - Tax Calculator
**File:** `Exercise2_1_TaxCalculator.cs`

A console application that calculates tax based on income brackets:
- Less than $10,000: 5% tax
- $10,000 to $100,000: 8% tax  
- More than $100,000: 8.5% tax

## Exercise 2.2 - Calculator Menu
**File:** `Exercise2_2_Calculator.cs`

A console calculator with menu options:
- Press 1 for addition
- Press 2 for subtraction
- Press 3 for multiplication
- Press 4 for division
- Press 5 to exit

## Exercise 2.3 - Pattern with 'x'
**File:** `Exercise2_3_PatternX.cs`

Prints a diamond-like pattern using 'x' characters:
```
       xxxxxxxxxxxxxxx
        xxxxxxxxxxxxx
         xxxxxxxxxxx
          xxxxxxxxx
           xxxxxxx
            xxxxx
             xxx
              x
```

## Exercise 2.4 - Number Pattern
**File:** `Exercise2_4_NumberPattern.cs`

Prints numbers 1-15 in a triangular pattern using for loops:
```
1
2 3
4 5 6
7 8 9 10
11 12 13 14 15
```

## How to Run

### Option 1: Use the batch file
Run `RunExercises.bat` to compile and choose which exercise to run.

### Option 2: Manual compilation
```cmd
csc Exercise2_1_TaxCalculator.cs
Exercise2_1_TaxCalculator.exe

csc Exercise2_2_Calculator.cs
Exercise2_2_Calculator.exe

csc Exercise2_3_PatternX.cs
Exercise2_3_PatternX.exe

csc Exercise2_4_NumberPattern.cs
Exercise2_4_NumberPattern.exe
```

## Requirements Met
- ✅ All exercises use console applications
- ✅ Exercise 2.1 implements tax calculation with proper brackets
- ✅ Exercise 2.2 implements calculator with menu system
- ✅ Exercise 2.3 creates the specified 'x' pattern
- ✅ Exercise 2.4 uses for loops to create number pattern 1-15