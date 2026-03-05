using FortunaPrimigenia.Api.Models.Domain;
using FortunaPrimigenia.Api.Models.DTO;
using FortunaPrimigenia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FortunaPrimigenia.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<Transaction>>> CreateMultipleTransactionAsync(
        [FromBody] List<CreateTransactionDto> toCreateTransactionDtos)
    {
        var newTransactions = await transactionService.CreateMultipleTransactionsAsync(toCreateTransactionDtos);
        return Ok(newTransactions);
    }

    [HttpGet("{transactionId}")]
    public async Task<ActionResult<Transaction>> GetTransactionByIdAsync(int transactionId)
    {
        var transaction = await transactionService.GetTransactionByIdAsync(transactionId);
        if (transaction == null) return NotFound();

        return Ok(transaction);
    }

    [HttpGet("account/{accountId}")]
    public async Task<ActionResult<List<Transaction>>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return Ok(await transactionService.GetTransactionsByAccountIdAsync(accountId));
    }

    [HttpGet("account/{accountId}/count")]
    public async Task<ActionResult<int>> GetTransactionCountByAccountIdAsync(int accountId)
    {
        return Ok(await transactionService.GetTransactionCountByAccountIdAsync(accountId));
    }

    [HttpPut("{transactionId}")]
    public async Task<ActionResult<Transaction>> UpdateTransactionAsync(int transactionId,
        [FromBody] UpdateTransactionDto transaction)
    {
        if (transactionId != transaction.Id) return BadRequest("Transaction ID does not match route parameter");

        var updated = await transactionService.UpdateTransactionAsync(transaction);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{transactionId}")]
    public async Task<ActionResult> DeleteTransactionAsync(int transactionId)
    {
        if (transactionId <= 0) return BadRequest("Transaction ID must be positive");

        var deleted = await transactionService.DeleteTransactionAsync(transactionId);
        if (!deleted) return NotFound();

        return NoContent();
    }
}