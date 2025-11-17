// namespace BankingApp.Tests;
// using System;
// using Xunit;
//
// public class AccountTest
// {
//     private Account source;
//     private Account destination;
//
//     public AccountTest()
//     {
//         source = new Account();
//         source.Deposit(200.0f);
//         
//         destination = new Account();
//         destination.Deposit(150.0f);
//     }
//
//     [Fact]
//     public void TransferFunds_Pass()
//     {
//         // act
//         source.TransferFunds(destination, 100.0f);
//         
//         // assert
//         Assert.Equal(250.0f, destination.Balance);
//         Assert.Equal(100.0f, source.Balance);
//     }
//     
//     [Theory]
//     [InlineData(150, 0, 78)]
//     [InlineData(2000, 1000, 1500)]
//     [InlineData(20, 0, 10)]
//     public void TransferMinFunds_Pass(float sourceInitial, float destinationInitial, float transferAmount)
//     {
//         // arrange
//         var source = new Account(sourceInitial);
//         var destination = new Account(destinationInitial);
//         
//         // act
//         source.TransferMinFunds(destination, transferAmount);
//         
//         // assert
//         Assert.Equal(destinationInitial + transferAmount, destination.Balance);
//         Assert.Equal(sourceInitial - transferAmount, source.Balance);
//     }
//
//     [Theory]
//     [InlineData(150, 0, 78)]
//     [InlineData(2000, 1000, 1500)]
//     [InlineData(20, 0, 10)]
//     [InlineData(10, 10, 10)]
//     public void TransferMinFundsFail_ThrowsException(float sourceInitial, float destinationInitial, float transferAmount)
//     {
//         var source = new Account();
//         var destination = new Account();
//         
//         source.Deposit(sourceInitial);
//         destination.Deposit(destinationInitial);
//     
//         Assert.Throws<NotEnoughFundsException>(() =>
//             source.TransferMinFunds(destination, transferAmount)
//         );
//     }
//
//     [Theory]
//     [MemberData(nameof(CombinatorialTestData))]
//     public void TransferMinFundsFailAll_ThrowsException(int sourceInitial, int destinationInitial, int transferAmount)
//     {
//         var source = new Account();
//         source.Deposit(sourceInitial);
//         var destination = new Account();
//         destination.Deposit(destinationInitial);
//     
//         Assert.Throws<NotEnoughFundsException>(() =>
//             source.TransferMinFunds(destination, transferAmount)
//         );
//     }
//     
//     public static IEnumerable<object[]> CombinatorialTestData =>
//         new List<object[]>
//         {
//             new object[] { 200, 0, 190 },
//             new object[] { 200, 0, 190 },
//             new object[] { 0, 0, 0 },
//             new object[] { 10, 10, 10 },
//             new object[] { 10, 10, 10 },
//             new object[] { 10, 10, 10 },
//             new object[] { 10, 10, 10 },
//             new object[] { 10, 10, 10 },
//         };
// }