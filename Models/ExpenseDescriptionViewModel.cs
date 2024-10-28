using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpendSmart.Models
{
	public class ExpenseDescriptionViewModel
	{
		public List<Expense>? Expenses { get; set; }
		public SelectList? Descriptions { get; set; }
		public string? ExpenseDescription { get; set; }
		public string? SearchString { get; set; }
	}
}
