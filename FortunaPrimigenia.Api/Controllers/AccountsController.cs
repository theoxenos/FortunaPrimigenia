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
        try
        {
            var created = await accountsService.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountByIdAsync),
                new
                {
                    accountId = created.Id
                },
                created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetAllAccountsAsync()
    {
        return Ok(await accountsService.GetAllAccountsAsync());
    }

    [HttpGet("{accountId}")]
    [ActionName(nameof(GetAccountByIdAsync))]
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
        if (updatedAccount is null) return BadRequest("Updated account cannot be null");
        if (accountId != updatedAccount.Id) return BadRequest("Account ID does not match route parameter");

        var account = await accountsService.UpdateAccountAsync(updatedAccount);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpDelete("{accountId}")]
    public async Task<ActionResult> DeleteAccountAsync(int accountId)
    {
        if (accountId <= 0) return BadRequest("Account ID must be positive");
        try
        {
            await accountsService.DeleteAccountAsync(accountId);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return NoContent();
    }
}