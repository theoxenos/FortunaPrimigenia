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
    public Task<Transaction?> UpdateTransactionAsync(Transaction transaction);
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
        return await transactionRepository.GetTransactionsByIdAsync(transactionId);
    }

    public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return await transactionRepository.GetTransactionByAccountIdAsync(accountId);
    }

    public Task<int> GetTransactionCountByAccountIdAsync(int accountId)
    {
        return transactionRepository.GetTransactionCountByAccountIdAsync(accountId);
    }

    public async Task<Transaction?> UpdateTransactionAsync(Transaction transaction)
    {
        return await transactionRepository.UpdateTransactionAsync(transaction);
    }

    public async Task<bool> DeleteTransactionAsync(int transactionId)
    {
        return await transactionRepository.DeleteTransactionAsync(transactionId);
    }
}