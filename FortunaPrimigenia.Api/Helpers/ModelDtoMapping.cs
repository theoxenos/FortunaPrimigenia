using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;

namespace FortunaPrimigenia.Api.Helpers;

public static class ModelDtoMapping
{
    public static Account MapCreateAccountDtoToAccountModel(this CreateAccountDto createAccountDto)
    {
        return new Account
        {
            Name = createAccountDto.Name,
            Balance = createAccountDto.Balance,
            Currency = createAccountDto.Currency,
            Type = createAccountDto.Type,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            IsOnBudget = createAccountDto.IsOnBudget
        };
    }

    public static Transaction MapCreateTransactionDtoToTransactionModel(this CreateTransactionDto createTransactionDto)
    {
        return new Transaction
        {
            AccountId = createTransactionDto.AccountId,
            CategoryId = createTransactionDto.CategoryId,
            Payee = createTransactionDto.Payee,
            Memo = createTransactionDto.Memo,
            InflowAmount = createTransactionDto.InflowAmount,
            OutflowAmount = createTransactionDto.OutflowAmount,
            IsCleared = createTransactionDto.IsCleared,
            TransactionDate = createTransactionDto.TransactionDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}