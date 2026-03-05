using FortunaPrimigenia.Api.Controllers;
using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FortunaPrimigenia.Api.Tests.Unit.Controllers;

public class TransactionControllerTests
{
    private readonly TransactionsController _transactionsController;
    private readonly Mock<ITransactionService> _transactionServiceMock;

    public TransactionControllerTests()
    {
        _transactionServiceMock = new Mock<ITransactionService>();
        _transactionsController = new TransactionsController(_transactionServiceMock.Object);
    }

    [Fact]
    public async Task CreateMultipleTransactionAsync_ReturnsCreatedTransactions()
    {
        // Arrange
        var createTransactionsDtos = new List<CreateTransactionDto>
        {
            new() { Payee = "Payee 1" },
            new() { Payee = "Payee 2" }
        };

        var transactions = createTransactionsDtos.Select(t => new Transaction { Payee = t.Payee }).ToList();

        _transactionServiceMock.Setup(service => service.CreateMultipleTransactionsAsync(createTransactionsDtos))
            .ReturnsAsync(transactions);

        // Act
        var result = await _transactionsController.CreateMultipleTransactionAsync(createTransactionsDtos);

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTransactions = Assert.IsType<List<Transaction>>(okResult.Value);
        Assert.Equal(createTransactionsDtos.Count, returnedTransactions.Count);
    }

    [Fact]
    public async Task CreateMultipleTransactionAsync_ReturnsBadRequest_WhenInvalidDtos()
    {
        // Arrange
        _transactionServiceMock.Setup(service =>
                service.CreateMultipleTransactionsAsync(It.IsAny<List<CreateTransactionDto>>()))
            .Throws<ArgumentNullException>();

        // Act
        var result = await _transactionsController.CreateMultipleTransactionAsync([]);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }
}