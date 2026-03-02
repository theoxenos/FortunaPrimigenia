using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FortunaPrimigenia.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController(IAccountsService accountsService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccountAsync([FromBody] CreateAccountDto account)
    {
        return Ok(await accountsService.CreateAccountAsync(account));
    }

    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetAllAccountsAsync()
    {
        return Ok(await accountsService.GetAllAccountsAsync());
    }

    [HttpGet("{accountId}")]
    public async Task<ActionResult<Account?>> GetAccountByIdAsync(int accountId)
    {
        var account = await accountsService.GetAccountByIdAsync(accountId);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpGet("name/{accountName}")]
    public async Task<ActionResult<Account?>> GetAccountByNameAsync(string accountName)
    {
        var account = await accountsService.GetAccountByNameAsync(accountName);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpPut("{accountId}")]
    public async Task<ActionResult<Account>> UpdateAccountAsync(int accountId, [FromBody] Account updatedAccount)
    {
        return Ok(await accountsService.UpdateAccountAsync(updatedAccount));
    }

    [HttpDelete("{accountId}")]
    public async Task<ActionResult> DeleteAccountAsync(int accountId)
    {
        await accountsService.DeleteAccountAsync(accountId);
        return NoContent();
    }
}