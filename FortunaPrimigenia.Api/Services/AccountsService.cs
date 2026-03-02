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
    Task<Account> UpdateAccountAsync(Account account);
    Task DeleteAccountAsync(int accountId);
}

public class AccountsService(IAccountsRepository accountsRepository) : IAccountsService
{
    public Task<Account> CreateAccountAsync(CreateAccountDto account)
    {
        var newAccount = new Account
        {
            Name = account.Name,
            Balance = account.Balance,
            Currency = account.Currency,
            Type = account.Type,
            CreatedAt = default,
            UpdatedAt = default,
            IsOnBudget = account.IsOnBudget
        };
        return accountsRepository.CreateAccountAsync(newAccount);
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

    public Task<Account> UpdateAccountAsync(Account account)
    {
        return accountsRepository.UpdateAccountAsync(account);
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var successfulDelete = await accountsRepository.DeleteAccountAsync(accountId);
        if (!successfulDelete) throw new InvalidOperationException($"Account {accountId} not found");
    }
}