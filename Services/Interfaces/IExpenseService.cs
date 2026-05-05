using FinanceAPI.DTOs;

namespace FinanceAPI.Services.Interfaces;

public interface IExpenseService
{
    Task<List<ExpenseDtos.ExpenseResponseDto>> GetAllForUserAsync(int userId);
    Task<ExpenseDtos.ExpenseResponseDto> GetByIdAsync(int id ,  int userId);
    Task<ExpenseDtos.ExpenseResponseDto> CreateAsync(ExpenseDtos.CreateExpenseDto dto,  int userId);
    Task DeleteAsync(int id, int userId);
}