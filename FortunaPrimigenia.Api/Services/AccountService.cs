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

public class AccountService(IAccountRepository accountRepository) : IAccountsService
{
    public async Task<Account> CreateAccountAsync(CreateAccountDto account)
    {
        var existingAccount = await accountRepository.GetAccountByNameAsync(account.Name);
        if (existingAccount is not null)
            throw new ArgumentException($"Account with name '{account.Name}' already exists.");

        var newAccount = new Account
        {
            Name = account.Name,
            Balance = account.Balance,
            Currency = account.Currency,
            Type = account.Type,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            IsOnBudget = account.IsOnBudget
        };
        return await accountRepository.CreateAccountAsync(newAccount);
    }

    public Task<List<Account>> GetAllAccountsAsync()
    {
        return accountRepository.GetAllAccountsAsync();
    }

    public Task<Account?> GetAccountByIdAsync(int accountId)
    {
        return accountRepository.GetAccountByIdAsync(accountId);
    }

    public Task<Account?> GetAccountByNameAsync(string accountName)
    {
        return accountRepository.GetAccountByNameAsync(accountName);
    }

    public Task<Account?> UpdateAccountAsync(Account account)
    {
        return accountRepository.UpdateAccountAsync(account);
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var successfulDelete = await accountRepository.DeleteAccountAsync(accountId);
        if (!successfulDelete) throw new InvalidOperationException($"Account {accountId} not found");
    }
}