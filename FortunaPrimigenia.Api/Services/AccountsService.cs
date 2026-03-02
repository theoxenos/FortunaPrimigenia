using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Repositories;

namespace FortunaPrimigenia.Api.Services;

public interface IAccountsService
{
    Task<Account> CreateAccountAsync(CreateAccountDto account);
    Task<List<Account>> GetAllAccountsAsync();
    Task<Account?> GetAccountByIdAsync(int accountId);
    Task<Account?> GetAccountByNameAsync(string accountName);
    Task<Account?> UpdateAccountAsync(Account account);
    Task DeleteAccountAsync(int accountId);
}

public class AccountsService(IAccountsRepository accountsRepository) : IAccountsService
{
    public async Task<Account> CreateAccountAsync(CreateAccountDto account)
    {
        var existingAccount = await accountsRepository.GetAccountByNameAsync(account.Name);
        if (existingAccount is not null)
            throw new ArgumentException($"Account with name '{account.Name}' already exists.");

        var newAccount = new Account
        {
            Name = account.Name,
            Balance = account.Balance,
            Currency = account.Currency,
            Type = account.Type,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsOnBudget = account.IsOnBudget
        };
        return await accountsRepository.CreateAccountAsync(newAccount);
    }

    public Task<List<Account>> GetAllAccountsAsync()
    {
        return accountsRepository.GetAllAccountsAsync();
    }

    public Task<Account?> GetAccountByIdAsync(int accountId)
    {
        return accountsRepository.GetAccountByIdAsync(accountId);
    }

    public Task<Account?> GetAccountByNameAsync(string accountName)
    {
        return accountsRepository.GetAccountByNameAsync(accountName);
    }

    public Task<Account?> UpdateAccountAsync(Account account)
    {
        return accountsRepository.UpdateAccountAsync(account);
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var successfulDelete = await accountsRepository.DeleteAccountAsync(accountId);
        if (!successfulDelete) throw new InvalidOperationException($"Account {accountId} not found");
    }
}