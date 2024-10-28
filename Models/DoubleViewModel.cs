using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace SpendSmart.Models
{
    public class DoubleViewModel
    {
            public int Id { get; set; }
            public decimal Value { get; set; }

            [Required]
            public string? Description { get; set; }
            
            public List<Expense>? Expenses { get; set; }
            public SelectList? Descriptions { get; set; }
            public string? ExpenseDescription { get; set; }
            public string? SearchString { get; set; }
    }
}
