using FortunaPrimigenia.Api.Data;
using FortunaPrimigenia.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FortunaPrimigenia.Api.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
    Task<List<Transaction>> CreateMultipleTransactionsAsync(List<Transaction> transactions);
    Task<Transaction?> GetTransactionsByIdAsync(int transactionId);
    Task<List<Transaction>> GetTransactionByAccountIdAsync(int accountId);
    Task<int> GetTransactionCountByAccountIdAsync(int accountId);
    Task<Transaction?> UpdateTransactionAsync(Transaction transaction);
    Task<bool> DeleteTransactionAsync(int transactionId);
}

public class TransactionRepository(FortunaPrimigeniaContext dbContext) : ITransactionRepository
{
    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        await dbContext.Transactions.AddAsync(transaction);
        await dbContext.SaveChangesAsync();
        return transaction;
    }

    public async Task<List<Transaction>> CreateMultipleTransactionsAsync(List<Transaction> transactions)
    {
        await dbContext.Transactions.AddRangeAsync(transactions);
        await dbContext.SaveChangesAsync();
        return transactions;
    }

    public async Task<Transaction?> GetTransactionsByIdAsync(int transactionId)
    {
        return await dbContext.Transactions.FindAsync(transactionId);
    }

    public async Task<List<Transaction>> GetTransactionByAccountIdAsync(int accountId)
    {
        var transactions = dbContext.Transactions.Where(t => t.AccountId == accountId);
        return await transactions.ToListAsync();
    }

    public Task<int> GetTransactionCountByAccountIdAsync(int accountId)
    {
        return dbContext.Transactions.CountAsync(t => t.AccountId == accountId);
    }

    public async Task<Transaction?> UpdateTransactionAsync(Transaction transaction)
    {
        var toUpdateTransaction =
            await dbContext.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.Id == transaction.Id);
        if (toUpdateTransaction is null)
            return null;

        dbContext.Transactions.Update(toUpdateTransaction);
        await dbContext.SaveChangesAsync();
        return toUpdateTransaction;
    }

    public async Task<bool> DeleteTransactionAsync(int transactionId)
    {
        var transactions = await dbContext.Transactions.AsNoTracking().ToListAsync();
        var transactionToDelete = transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transactionToDelete is null)
            return false;

        dbContext.Transactions.Remove(transactionToDelete);
        await dbContext.SaveChangesAsync();
        return true;
    }
}