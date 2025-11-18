# 📘 BankingApp Testing Project

This repository contains the materials developed for the Software Application Testing lab. The project started from a base class provided by the instructor and was extended through multiple layers of testing, additional validations, and a small console application to interact with the system.

## 📂 Project Structure
### 1. Library Project (BankingApp)

This project contains the original base class received from the professor, which has been modified and extended during the assignment.

Main work includes:

- Updating and refining existing methods.

- Adding new custom exceptions:

  - NegativeAmountException

  - MinimumBalanceException

  - NotEnoughFundsException
    
  - IncorrectCurrecnyConverter
 
  - AccountFrozenException

- Improving validation logic such as:

  - Withdrawal conditions (min balance, insufficient funds)

  - Deposit validation

  - Transfer logic with currency conversion support

### 2. Test Project (BankingApp.Tests)

This project contains the full suite of tests required in the assignment.

Types of tests included:

- Unit Tests

  - Target individual methods and behaviors.

  - Include boundary value analysis for critical conditions.

- Domain Tests

  - Use domain-specific scenarios to validate real-world use cases.

- Mock Tests

  - Based on a custom interface created for the project.

  - Use mocking to isolate dependencies and verify behavior.

  - A custom interface was created to support mocking (INotifier).

### 3. Console Application (BankingApp.ConsoleUI)

A separate project containing a small interactive console menu that demonstrates how the banking library works.

The menu includes the following options:

1. Create New Account

2. Deposit Funds

3. Withdraw Funds

4. Transfer Funds (to another account)

5. Transfer EUR → RON (with currency converter)

6. Simulate Balance With Fees (GetBalanceWithFees)

7. View All Transaction History

8. View Transaction History (Filtered by Date)

9. Freeze Account

10. reeze Account

11. Exit Application

The console application provides:

- User-friendly navigation

- Input validation

- Exception handling (including all custom exceptions)

- A simple way to test the features manually

All functionality is implemented in **Program.cs**.

## 🧪 Testing Approach

The testing part of the project covers:

- Happy path testing

- Boundary testing

- Exception testing

- Mock-based behavior testing

- Valid and invalid currency conversion

Tools & techniques used:

- xUnit

- Test doubles

- Mocking framework (Moq)

## ▶️ How to Run

Open the solution in Visual Studio / Rider.

Build all projects.

Run the test project to verify correctness.

Run the console application to interact with the banking system.

## 📎 Credits

Base class provided by the course instructor.
All modifications, tests, and additional features implemented by Adelin Sima.
