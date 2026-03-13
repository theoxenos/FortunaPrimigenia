using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Repositories;
using FortunaPrimigenia.Api.Services;
using Moq;

namespace FortunaPrimigenia.Api.Tests.Unit.Services;

public class AccountServiceTests
{
    private readonly AccountService _accountService;
    private readonly Mock<IAccountRepository> _accountsRepositoryMock;

    public AccountServiceTests()
    {
        _accountsRepositoryMock = new Mock<IAccountRepository>();
        _accountService = new AccountService(_accountsRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateAccountAsync_WithValidDto_ShouldReturnCreatedAccountWithMappedProperties()
    {
        // Arrange
        var createAccountDto = new CreateAccountDto
        {
            Name = "Checking Account",
            Balance = 1000.50m,
            Currency = "USD",
            Type = "Checking",
            IsOnBudget = true
        };

        _accountsRepositoryMock.Setup(repo => repo.CreateAccountAsync(It.IsAny<Account>()))
            .ReturnsAsync((Account account) => account);

        // Act
        var result = await _accountService.CreateAccountAsync(createAccountDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createAccountDto.Name, result.Name);
        Assert.Equal(createAccountDto.Balance, result.Balance);
        Assert.Equal(createAccountDto.Currency, result.Currency);
        Assert.Equal(createAccountDto.Type, result.Type);
        Assert.Equal(createAccountDto.IsOnBudget, result.IsOnBudget);
        _accountsRepositoryMock.Verify(repo => repo.CreateAccountAsync(It.Is<Account>(a =>
            a.Name == createAccountDto.Name &&
            a.Balance == createAccountDto.Balance &&
            a.Currency == createAccountDto.Currency &&
            a.Type == createAccountDto.Type &&
            a.IsOnBudget == createAccountDto.IsOnBudget)), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1000000.99)]
    [InlineData(-500.25)]
    public async Task CreateAccountAsync_WithVariousBalances_ShouldCorrectlyMapBalance(decimal balance)
    {
        // Arrange
        var createAccountDto = new CreateAccountDto
        {
            Name = "Test Account",
            Balance = balance,
            Currency = "EUR",
            Type = "Savings",
            IsOnBudget = true
        };

        _accountsRepositoryMock.Setup(repo => repo.CreateAccountAsync(It.IsAny<Account>()))
            .ReturnsAsync((Account account) => account);

        // Act
        var result = await _accountService.CreateAccountAsync(createAccountDto);

        // Assert
        Assert.Equal(balance, result.Balance);
    }

    [Fact]
    public async Task GetAllAccountsAsync_WhenAccountsExist_ShouldReturnListOfAccounts()
    {
        // Arrange
        var accounts = new List<Account>
        {
            new()
            {
                Id = 1,
                Name = "Account 1",
                Balance = 100,
                Currency = "USD",
                Type = "Checking",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            },
            new()
            {
                Id = 2,
                Name = "Account 2",
                Balance = 200,
                Currency = "EUR",
                Type = "Savings",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            }
        };

        _accountsRepositoryMock.Setup(repo => repo.GetAllAccountsAsync())
            .ReturnsAsync(accounts);

        // Act
        var result = await _accountService.GetAllAccountsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(accounts, result);
    }

    [Fact]
    public async Task GetAllAccountsAsync_WhenNoAccountsExist_ShouldReturnEmptyList()
    {
        // Arrange
        _accountsRepositoryMock.Setup(repo => repo.GetAllAccountsAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _accountService.GetAllAccountsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAccountByIdAsync_WhenAccountExists_ShouldReturnAccount()
    {
        // Arrange
        var accountId = 1;
        var expectedAccount = new Account
        {
            Id = accountId,
            Name = "Test Account",
            Balance = 100,
            Currency = "USD",
            Type = "Checking",
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        _accountsRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(accountId))
            .ReturnsAsync(expectedAccount);

        // Act
        var result = await _accountService.GetAccountByIdAsync(accountId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accountId, result.Id);
        Assert.Equal(expectedAccount.Name, result.Name);
    }

    [Fact]
    public async Task GetAccountByIdAsync_WhenAccountDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var accountId = 999;
        _accountsRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(accountId))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _accountService.GetAccountByIdAsync(accountId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAccountByNameAsync_WhenAccountExists_ShouldReturnAccount()
    {
        // Arrange
        var accountName = "Checking";
        var expectedAccount = new Account
        {
            Id = 1,
            Name = accountName,
            Balance = 100,
            Currency = "USD",
            Type = "Checking",
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        _accountsRepositoryMock.Setup(repo => repo.GetAccountByNameAsync(accountName))
            .ReturnsAsync(expectedAccount);

        // Act
        var result = await _accountService.GetAccountByNameAsync(accountName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accountName, result.Name);
    }

    [Fact]
    public async Task GetAccountByNameAsync_WhenAccountDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var accountName = "NonExistent";
        _accountsRepositoryMock.Setup(repo => repo.GetAccountByNameAsync(accountName))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _accountService.GetAccountByNameAsync(accountName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAccountAsync_ShouldCallRepositoryAndUpdateAccount()
    {
        // Arrange
        var accountToUpdate = new Account
        {
            Id = 1,
            Name = "Updated Name",
            Balance = 500,
            Currency = "USD",
            Type = "Checking",
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        _accountsRepositoryMock.Setup(repo => repo.UpdateAccountAsync(accountToUpdate))
            .ReturnsAsync(accountToUpdate);

        // Act
        var result = await _accountService.UpdateAccountAsync(accountToUpdate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accountToUpdate.Name, result.Name);
        _accountsRepositoryMock.Verify(repo => repo.UpdateAccountAsync(accountToUpdate), Times.Once);
    }

    [Fact]
    public async Task DeleteAccountAsync_WhenAccountExists_ShouldCompleteSuccessfully()
    {
        // Arrange
        var accountId = 1;
        _accountsRepositoryMock.Setup(repo => repo.DeleteAccountAsync(accountId))
            .ReturnsAsync(true);

        // Act
        var exception = await Record.ExceptionAsync(() => _accountService.DeleteAccountAsync(accountId));

        // Assert
        Assert.Null(exception);
        _accountsRepositoryMock.Verify(repo => repo.DeleteAccountAsync(accountId), Times.Once);
    }

    [Fact]
    public async Task DeleteAccountAsync_WhenAccountDoesNotExist_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var accountId = 999;
        _accountsRepositoryMock.Setup(repo => repo.DeleteAccountAsync(accountId))
            .ReturnsAsync(false);

        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(() => _accountService.DeleteAccountAsync(accountId));
        Assert.Equal($"Account {accountId} not found", exception.Message);
        _accountsRepositoryMock.Verify(repo => repo.DeleteAccountAsync(accountId), Times.Once);
    }

    [Fact]
    public async Task CreateAccountAsync_WhenAccountNameAlreadyExists_ShouldThrowArgumentException()
    {
        // Arrange
        var dto = new CreateAccountDto { Name = "Existing", Balance = 100, Currency = "USD", Type = "Checking" };
        var existingAccount = new Account { Name = dto.Name, Balance = 0, Currency = "USD", Type = "Checking" };

        _accountsRepositoryMock.Setup(repo => repo.GetAccountByNameAsync(dto.Name))
            .ReturnsAsync(existingAccount);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _accountService.CreateAccountAsync(dto));
        Assert.Equal($"Account with name '{dto.Name}' already exists.", exception.Message);

        _accountsRepositoryMock.Verify(repo => repo.CreateAccountAsync(It.IsAny<Account>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAccountAsync_WhenAccountDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var account = new Account { Id = 1, Name = "NonExistent", Balance = 0, Currency = "USD", Type = "Checking" };
        _accountsRepositoryMock.Setup(repo => repo.UpdateAccountAsync(account))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _accountService.UpdateAccountAsync(account);

        // Assert
        Assert.Null(result);
    }
}