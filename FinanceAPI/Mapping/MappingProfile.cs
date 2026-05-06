using AutoMapper;
using FinanceAPI.DTOs;
using FinanceAPI.Models;

namespace FinanceAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Expense, ExpenseDtos.ExpenseResponseDto>();
        CreateMap<ExpenseDtos.CreateExpenseDto , Expense>();
    }
}