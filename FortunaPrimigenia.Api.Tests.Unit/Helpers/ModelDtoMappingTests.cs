using FortunaPrimigenia.Api.Helpers;
using FortunaPrimigenia.Api.Models.DTO;

namespace FortunaPrimigenia.Api.Tests.Unit.Helpers;

public class ModelDtoMappingTests
{
    [Fact]
    public void MapCreateAccountDtoToAccountModel_ReturnsCorrectAccountModel()
    {
        // Arrange
        var createAccountDto = new CreateAccountDto
        {
            Name = "Test Account",
            Balance = 1000,
            Currency = "USD",
            Type = "Checking"
        };

        // Act
        var accountModel = createAccountDto.MapCreateAccountDtoToAccountModel();

        // Assert
        Assert.Equal(createAccountDto.Name, accountModel.Name);
        Assert.Equal(createAccountDto.Balance, accountModel.Balance);
        Assert.Equal(createAccountDto.Currency, accountModel.Currency);
        Assert.Equal(createAccountDto.Type, accountModel.Type);
    }

    [Fact]
    public void MapUpdateAccountDtoToAccountModel_ReturnsCorrectAccountModel()
    {
        // Arrange
        var createTransActionDto = new CreateTransactionDto
        {
            AccountId = 1,
            CategoryId = 1,
            InflowAmount = 20m,
            OutflowAmount = 0m,
            Payee = "Test Payee",
            TransactionDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var accountModel = createTransActionDto.MapCreateTransactionDtoToTransactionModel();

        // Assert
        Assert.Equal(createTransActionDto.AccountId, accountModel.AccountId);
        Assert.Equal(createTransActionDto.CategoryId, accountModel.CategoryId);
        Assert.Equal(createTransActionDto.InflowAmount, accountModel.InflowAmount);
        Assert.Equal(createTransActionDto.OutflowAmount, accountModel.OutflowAmount);
        Assert.Equal(createTransActionDto.Payee, accountModel.Payee);
        Assert.Equal(createTransActionDto.TransactionDate, accountModel.TransactionDate);
    }
}