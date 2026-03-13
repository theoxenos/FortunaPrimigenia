using FortunaPrimigenia.Api.Controllers;
using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FortunaPrimigenia.Api.Tests.Unit.Controllers;

public class AccountsControllerTests
{
    private readonly AccountsController _accountsController;
    private readonly Mock<IAccountsService> _accountsServiceMock;

    public AccountsControllerTests()
    {
        _accountsServiceMock = new Mock<IAccountsService>();
        _accountsController = new AccountsController(_accountsServiceMock.Object);
    }

    [Fact]
    public async Task CreateAccountAsync_ReturnsCreatedAccount()
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

        var createdAccount = new Account
        {
            Id = 1,
            Name = createAccountDto.Name,
            Balance = createAccountDto.Balance,
            Currency = createAccountDto.Currency,
            Type = createAccountDto.Type,
            IsOnBudget = createAccountDto.IsOnBudget
        };

        _accountsServiceMock.Setup(service => service.CreateAccountAsync(It.IsAny<CreateAccountDto>()))
            .ReturnsAsync(createdAccount);

        // Act
        var result = await _accountsController.CreateAccountAsync(createAccountDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, createdResult.StatusCode);

        var returnedAccount = Assert.IsType<Account>(createdResult.Value);
        Assert.Equal(createdAccount.Id, returnedAccount.Id);
        Assert.Equal(createdAccount.Name, returnedAccount.Name);
        Assert.Equal(createdAccount.Balance, returnedAccount.Balance);
        Assert.Equal(createdAccount.Currency, returnedAccount.Currency);
        Assert.Equal(createdAccount.Type, returnedAccount.Type);
        Assert.Equal(createdAccount.IsOnBudget, returnedAccount.IsOnBudget);

        _accountsServiceMock.Verify(service => service.CreateAccountAsync(createAccountDto), Times.Once);
    }

    [Fact]
    public async Task GetAllAccounts_ReturnsAllAccounts()
    {
        // Arrange
        var accounts = new List<Account>
        {
            new()
            {
                Id = 1, Name = "Checking Account", Balance = 1000.50m, Currency = "USD", Type = "Checking",
                IsOnBudget = true
            },
            new()
            {
                Id = 2, Name = "Savings Account", Balance = 5000.00m, Currency = "EUR", Type = "Savings",
                IsOnBudget = false
            }
        };

        _accountsServiceMock.Setup(service => service.GetAllAccountsAsync())
            .ReturnsAsync(accounts);

        // Act
        var result = await _accountsController.GetAllAccountsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);

        var returnedAccounts = Assert.IsType<List<Account>>(okResult.Value);
        Assert.Equal(2, returnedAccounts.Count);
        Assert.Equal(accounts, returnedAccounts);
    }

    [Fact]
    public async Task GetAllAccounts_ReturnsEmptyArray_WhenNoAccountsExist()
    {
        // Arrange
        _accountsServiceMock.Setup(service => service.GetAllAccountsAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _accountsController.GetAllAccountsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAccounts = Assert.IsType<List<Account>>(okResult.Value);
        Assert.Empty(returnedAccounts);
    }

    [Fact]
    public async Task GetAccountByNameAsync_ReturnsCorrectAccount_WithValidAccountName()
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
            IsOnBudget = true
        };

        _accountsServiceMock.Setup(service => service.GetAccountByNameAsync(accountName))
            .ReturnsAsync(expectedAccount);

        // Act
        var result = await _accountsController.GetAccountByNameAsync(accountName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);

        var returnedAccount = Assert.IsType<Account>(okResult.Value);
        Assert.Equal(expectedAccount.Id, returnedAccount.Id);
        Assert.Equal(expectedAccount.Name, returnedAccount.Name);
    }

    [Fact]
    public async Task GetAccountByNameAsync_ReturnsNotFound_WithInvalidAccountName()
    {
        // Arrange
        var accountName = "NonExistent";
        _accountsServiceMock.Setup(service => service.GetAccountByNameAsync(accountName))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _accountsController.GetAccountByNameAsync(accountName);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task UpdateAccountAsync_UpdatesAccount_WithValidAccountDto()
    {
        // Arrange
        const int newBalance = 1500;
        var accountToUpdate = new Account
        {
            Id = 1,
            Name = "Test Account",
            Balance = 500,
            Currency = "EUR",
            Type = "Savings",
            IsOnBudget = false
        };

        _accountsServiceMock.Setup(service => service.UpdateAccountAsync(It.IsAny<Account>()))
            .ReturnsAsync(accountToUpdate with { Balance = newBalance });

        // Act
        var result = await _accountsController.UpdateAccountAsync(accountToUpdate.Id, accountToUpdate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);

        var returnedAccount = Assert.IsType<Account>(okResult.Value);
        Assert.Equal(accountToUpdate.Id, returnedAccount.Id);
        Assert.Equal(accountToUpdate.Name, returnedAccount.Name);
        Assert.Equal(accountToUpdate.Currency, returnedAccount.Currency);
        Assert.Equal(accountToUpdate.Type, returnedAccount.Type);
        Assert.Equal(accountToUpdate.IsOnBudget, returnedAccount.IsOnBudget);
        Assert.Equal(newBalance, returnedAccount.Balance);

        _accountsServiceMock.Verify(service => service.UpdateAccountAsync(accountToUpdate), Times.Once);
    }

    [Fact]
    public async Task UpdateAccountAsync_ReturnsBadRequest_WhenRouteParameterAndAccountIdDoNotMatch()
    {
        // Arrange
        var accountToUpdate = new Account
        {
            Id = 1,
            Name = "Test Account",
            Balance = 500,
            Currency = "EUR",
            Type = "Savings",
            IsOnBudget = false
        };

        _accountsServiceMock.Setup(service => service.UpdateAccountAsync(It.IsAny<Account>()));

        // Act
        var result = await _accountsController.UpdateAccountAsync(2, accountToUpdate);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);

        _accountsServiceMock.Verify(service => service.UpdateAccountAsync(accountToUpdate), Times.Never);
    }

    [Fact]
    public async Task UpdateAccountAsync_ReturnsBadRequest_WhenBodyIsNull()
    {
        // Arrange
        _accountsServiceMock.Setup(service => service.UpdateAccountAsync(It.IsAny<Account>()));

        // Act
        var result = await _accountsController.UpdateAccountAsync(1, null!);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);

        _accountsServiceMock.Verify(service => service.UpdateAccountAsync(It.IsAny<Account>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAccountAsync_DeletesAccount_WithValidAccountId()
    {
        // Arrange
        const int accountId = 1;
        _accountsServiceMock.Setup(service => service.DeleteAccountAsync(It.IsAny<int>()));

        // Act
        var result = await _accountsController.DeleteAccountAsync(accountId);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _accountsServiceMock.Verify(service => service.DeleteAccountAsync(accountId), Times.Once);
    }

    [Fact]
    public async Task DeleteAccountAsync_ReturnsNotFound_WhenAccountIdDoesNotExist()
    {
        // Arrange
        const int accountId = 999;
        _accountsServiceMock.Setup(service => service.DeleteAccountAsync(It.IsAny<int>()))
            .ThrowsAsync(new InvalidOperationException($"Account {accountId} not found"));

        // Act
        var result = await _accountsController.DeleteAccountAsync(accountId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task CreateAccountAsync_ReturnsBadRequest_WhenAccountNameAlreadyExists()
    {
        // Arrange
        var dto = new CreateAccountDto { Name = "Existing", Balance = 100, Currency = "USD", Type = "Checking" };
        _accountsServiceMock.Setup(service => service.CreateAccountAsync(dto))
            .ThrowsAsync(new ArgumentException($"Account with name '{dto.Name}' already exists."));

        // Act
        var result = await _accountsController.CreateAccountAsync(dto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal($"Account with name '{dto.Name}' already exists.", badRequestResult.Value);
    }

    [Fact]
    public async Task UpdateAccountAsync_ReturnsNotFound_WhenAccountDoesNotExist()
    {
        // Arrange
        var account = new Account { Id = 1, Name = "Test", Balance = 100, Currency = "USD", Type = "Checking" };
        _accountsServiceMock.Setup(service => service.UpdateAccountAsync(account))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _accountsController.UpdateAccountAsync(account.Id, account);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}