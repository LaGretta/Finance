using FinanceAPI.DTOs;
using FinanceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpensesController : ControllerBase
{
    private readonly IValidator<ExpenseDtos.CreateExpenseDto> _validator;
    private readonly IExpenseService _expenseService;

    public ExpensesController(IValidator<ExpenseDtos.CreateExpenseDto> validator, IExpenseService expenseService)
    {
        _validator = validator;
        _expenseService = expenseService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetCurrentUserId();
        var expense = await _expenseService.GetByIdAsync(id, userId);
        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExpenseDtos.CreateExpenseDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

        var userId = GetCurrentUserId();
        var expense = await _expenseService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = expense.Id }, expense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetCurrentUserId();
        await _expenseService.DeleteAsync(id, userId);
        return NoContent();
    }
    private int GetCurrentUserId()
    {
        var idClaim = User.FindFirst("id")?.Value;
        return int.Parse(idClaim!);
    }
}


