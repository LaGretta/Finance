using AutoMapper;
using FinanceAPI.Data;
using FinanceAPI.DTOs;
using FinanceAPI.Exceptions;
using FinanceAPI.Models;
using FinanceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Services;

public class ExpenseService : IExpenseService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;
    
    public ExpenseService(IMapper mapper, AppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<List<ExpenseDtos.ExpenseResponseDto>> GetAllForUserAsync(int userId)
    {
        var expenses = await _dbContext.Expenses
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreateAt)
            .ToListAsync();
        return _mapper.Map<List<ExpenseDtos.ExpenseResponseDto>>(expenses);
    }
    
    public async Task<ExpenseDtos.ExpenseResponseDto> GetByIdAsync(int id, int userId)
    {
        var expenses = await _dbContext.Expenses.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        
        if(expenses == null) 
            throw new CustomExceptions.NotFoundExeption($"Expense with id {id} not found");
        
        return _mapper.Map<ExpenseDtos.ExpenseResponseDto>(expenses);
    }

    public async Task<ExpenseDtos.ExpenseResponseDto> CreateAsync(ExpenseDtos.CreateExpenseDto dto, int userId)
    {
        var expense = _mapper.Map<Expense>(dto);
        expense.UserId = userId;
        
        _dbContext.Expenses.Add(expense);
        await _dbContext.SaveChangesAsync();
        
        return _mapper.Map<ExpenseDtos.ExpenseResponseDto>(expense);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var expense = await _dbContext.Expenses
            .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        
        if(expense == null) 
            throw new CustomExceptions.NotFoundExeption($"Expense with id {id} not found");
        
        _dbContext.Expenses.Remove(expense);
        await _dbContext.SaveChangesAsync();
    }
}