using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Repositories;
using FortunaPrimigenia.Api.Services;
using Moq;

namespace FortunaPrimigenia.Api.Tests.Unit.Services;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly TransactionService _transactionService;

    public TransactionServiceTests()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _transactionService = new TransactionService(_transactionRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateMultipleTransactionsAsync_ReturnsCorrectNumberOfTransactions()
    {
        // Arrange
        var createTransactionDtos = new List<CreateTransactionDto>
        {
            new() { Payee = "Payee 1" },
            new() { Payee = "Payee 2" }
        };

        var transactions = new List<Transaction>
        {
            new() { Id = 1, Payee = "Payee 1" },
            new() { Id = 2, Payee = "Payee 2" }
        };

        _transactionRepositoryMock.Setup(repo => repo.CreateMultipleTransactionsAsync(It.IsAny<List<Transaction>>()))
            .ReturnsAsync(transactions);

        // Act
        var result = await _transactionService.CreateMultipleTransactionsAsync(createTransactionDtos);

        // Assert
        Assert.Equal(transactions.Count, result.Count);
        _transactionRepositoryMock.Verify(
            repo => repo.CreateMultipleTransactionsAsync(It.Is<List<Transaction>>(list => list.Count == 2)),
            Times.Once);
    }

    [Fact]
    public async Task CreateMultipleTransactionsAsync_ReturnsCorrectTransactions()
    {
        // Arrange
        var createTransactionDtos = new List<CreateTransactionDto>
        {
            new() { Payee = "Test Payee" },
            new() { Payee = "Test Payee 2" }
        };

        var transactions = new List<Transaction>
        {
            new() { Id = 1, Payee = "Test Payee" },
            new() { Id = 2, Payee = "Test Payee 2" }
        };

        _transactionRepositoryMock.Setup(repo => repo.CreateMultipleTransactionsAsync(It.IsAny<List<Transaction>>()))
            .ReturnsAsync(transactions);

        // Act
        var result = await _transactionService.CreateMultipleTransactionsAsync(createTransactionDtos);

        // Assert
        Assert.Equal(transactions[0].Payee, result[0].Payee);
        Assert.Equal(transactions[1].Payee, result[1].Payee);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ReturnsTransaction_WhenTransactionExists()
    {
        // Arrange
        var transactionId = 1;
        var transaction = new Transaction { Id = transactionId, Payee = "Test Payee" };

        _transactionRepositoryMock.Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ReturnsAsync(transaction);

        // Act
        var result = await _transactionService.GetTransactionByIdAsync(transactionId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transactionId, result.Id);
        _transactionRepositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ReturnsNull_WhenTransactionDoesNotExist()
    {
        // Arrange
        var transactionId = 1;

        _transactionRepositoryMock.Setup(repo => repo.GetTransactionByIdAsync(transactionId))
            .ReturnsAsync((Transaction?)null);

        // Act
        var result = await _transactionService.GetTransactionByIdAsync(transactionId);

        // Assert
        Assert.Null(result);
        _transactionRepositoryMock.Verify(repo => repo.GetTransactionByIdAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionsByAccountIdAsync_ReturnsListOfTransactions()
    {
        // Arrange
        var accountId = 1;
        var transactions = new List<Transaction>
        {
            new() { Id = 1, AccountId = accountId },
            new() { Id = 2, AccountId = accountId }
        };

        _transactionRepositoryMock.Setup(repo => repo.GetTransactionsByAccountIdAsync(accountId))
            .ReturnsAsync(transactions);

        // Act
        var result = await _transactionService.GetTransactionsByAccountIdAsync(accountId);

        // Assert
        Assert.Equal(transactions.Count, result.Count);
        Assert.All(result, t => Assert.Equal(accountId, t.AccountId));
        _transactionRepositoryMock.Verify(repo => repo.GetTransactionsByAccountIdAsync(accountId), Times.Once);
    }

    [Fact]
    public async Task GetTransactionCountByAccountIdAsync_ReturnsCorrectCount()
    {
        // Arrange
        var accountId = 1;
        var count = 5;

        _transactionRepositoryMock.Setup(repo => repo.GetTransactionCountByAccountIdAsync(accountId))
            .ReturnsAsync(count);

        // Act
        var result = await _transactionService.GetTransactionCountByAccountIdAsync(accountId);

        // Assert
        Assert.Equal(count, result);
        _transactionRepositoryMock.Verify(repo => repo.GetTransactionCountByAccountIdAsync(accountId), Times.Once);
    }

    [Fact]
    public async Task UpdateTransactionAsync_ReturnsUpdatedTransaction()
    {
        // Arrange
        var transaction = new Transaction { Id = 1, Payee = "Updated Payee" };
        var transactionDto = new UpdateTransactionDto { Id = 1, Payee = "Updated Payee" };

        _transactionRepositoryMock.Setup(repo => repo.GetTransactionByIdAsync(transactionDto.Id))
            .ReturnsAsync(transaction);
        _transactionRepositoryMock.Setup(repo => repo.UpdateTransactionAsync(transaction))
            .ReturnsAsync(transaction);

        // Act
        var result = await _transactionService.UpdateTransactionAsync(transactionDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
        Assert.Equal(transaction.Payee, result.Payee);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(transaction), Times.Once);
    }

    [Fact]
    public async Task UpdateTransactionAsync_ReturnsNull_WhenUpdateFails()
    {
        // Arrange
        var transaction = new Transaction { Id = 1, Payee = "Updated Payee" };
        var transactionDto = new UpdateTransactionDto { Id = 1, Payee = "Updated Payee" };

        _transactionRepositoryMock.Setup(repo => repo.UpdateTransactionAsync(transaction))
            .ReturnsAsync((Transaction?)null);

        // Act
        var result = await _transactionService.UpdateTransactionAsync(transactionDto);

        // Assert
        Assert.Null(result);
        _transactionRepositoryMock.Verify(repo => repo.UpdateTransactionAsync(transaction), Times.Never);
    }

    [Fact]
    public async Task DeleteTransactionAsync_ReturnsTrue_WhenDeletionSucceeds()
    {
        // Arrange
        var transactionId = 1;

        _transactionRepositoryMock.Setup(repo => repo.DeleteTransactionAsync(transactionId))
            .ReturnsAsync(true);

        // Act
        var result = await _transactionService.DeleteTransactionAsync(transactionId);

        // Assert
        Assert.True(result);
        _transactionRepositoryMock.Verify(repo => repo.DeleteTransactionAsync(transactionId), Times.Once);
    }

    [Fact]
    public async Task DeleteTransactionAsync_ReturnsFalse_WhenDeletionFails()
    {
        // Arrange
        var transactionId = 1;

        _transactionRepositoryMock.Setup(repo => repo.DeleteTransactionAsync(transactionId))
            .ReturnsAsync(false);

        // Act
        var result = await _transactionService.DeleteTransactionAsync(transactionId);

        // Assert
        Assert.False(result);
        _transactionRepositoryMock.Verify(repo => repo.DeleteTransactionAsync(transactionId), Times.Once);
    }
}