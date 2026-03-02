using FortunaPrimigenia.Api.Data;
using FortunaPrimigenia.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FortunaPrimigenia.Api.Repositories;

public interface IAccountsRepository
{
    Task<Account> CreateAccountAsync(Account account);
    Task<List<Account>> GetAllAccountsAsync();
    Task<Account?> GetAccountByIdAsync(int accountId);
    Task<Account?> GetAccountByNameAsync(string accountName);
    Task<Account?> UpdateAccountAsync(Account account);
    Task<bool> DeleteAccountAsync(int accountId);
}

public class AccountsRepository(FortunaPrimigeniaContext dbContext) : IAccountsRepository
{
    public async Task<Account> CreateAccountAsync(Account account)
    {
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();
        return account;
    }

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        return await dbContext.Accounts.ToListAsync();
    }

    public async Task<Account?> GetAccountByIdAsync(int accountId)
    {
        return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
    }

    public async Task<Account?> GetAccountByNameAsync(string accountName)
    {
        return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Name == accountName);
    }

    public async Task<Account?> UpdateAccountAsync(Account account)
    {
        var existingAccount = await dbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == account.Id);
        if (existingAccount is null)
            return null;

        dbContext.Accounts.Update(account);
        await dbContext.SaveChangesAsync();
        return account;
    }

    public async Task<bool> DeleteAccountAsync(int accountId)
    {
        var account = await dbContext.Accounts.FindAsync(accountId);
        if (account is null)
            return false;

        dbContext.Accounts.Remove(account);
        await dbContext.SaveChangesAsync();
        return true;
    }
}