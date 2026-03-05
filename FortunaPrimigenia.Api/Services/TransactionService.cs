using FortunaPrimigenia.Api.Helpers;
using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Repositories;

namespace FortunaPrimigenia.Api.Services;

public interface ITransactionService
{
    Task<List<Transaction>> CreateMultipleTransactionsAsync(List<CreateTransactionDto> transactions);
    public Task<Transaction?> GetTransactionByIdAsync(int transactionId);
    public Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    public Task<int> GetTransactionCountByAccountIdAsync(int accountId);
    public Task<Transaction?> UpdateTransactionAsync(UpdateTransactionDto transaction);
    public Task<bool> DeleteTransactionAsync(int transactionId);
}

public class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
{
    public async Task<List<Transaction>> CreateMultipleTransactionsAsync(List<CreateTransactionDto> transactions)
    {
        var mappedTransactions = transactions
            .Select(t => t.MapCreateTransactionDtoToTransactionModel()).ToList();
        return await transactionRepository.CreateMultipleTransactionsAsync(mappedTransactions);
    }

    public async Task<Transaction?> GetTransactionByIdAsync(int transactionId)
    {
        return await transactionRepository.GetTransactionByIdAsync(transactionId);
    }

    public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return await transactionRepository.GetTransactionsByAccountIdAsync(accountId);
    }

    public Task<int> GetTransactionCountByAccountIdAsync(int accountId)
    {
        return transactionRepository.GetTransactionCountByAccountIdAsync(accountId);
    }

    public async Task<Transaction?> UpdateTransactionAsync(UpdateTransactionDto updatedTransaction)
    {
        if (updatedTransaction.InflowAmount != 0 && updatedTransaction.OutflowAmount != 0)
            throw new ArgumentException("Cannot update both inflow and outflow amounts");

        var transactionFromDb = await transactionRepository.GetTransactionByIdAsync(updatedTransaction.Id);
        if (transactionFromDb is null) return null;

        transactionFromDb.AccountId = updatedTransaction.AccountId;
        transactionFromDb.CategoryId = updatedTransaction.CategoryId;
        transactionFromDb.Payee = updatedTransaction.Payee;
        transactionFromDb.Memo = updatedTransaction.Memo;
        transactionFromDb.TransactionDate = updatedTransaction.TransactionDate;
        transactionFromDb.InflowAmount = updatedTransaction.InflowAmount;
        transactionFromDb.OutflowAmount = updatedTransaction.OutflowAmount;
        transactionFromDb.IsCleared = updatedTransaction.IsCleared;
        transactionFromDb.UpdatedAt = DateTime.UtcNow;

        return await transactionRepository.UpdateTransactionAsync(transactionFromDb);
    }

    public async Task<bool> DeleteTransactionAsync(int transactionId)
    {
        return await transactionRepository.DeleteTransactionAsync(transactionId);
    }
}