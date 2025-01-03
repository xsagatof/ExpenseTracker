﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpendSmart.Data;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly SpendSmartContext _context;

        public ExpensesController(SpendSmartContext context)
        {
            _context = context;
        }

		// GET: Expenses
		public async Task<IActionResult> Index(string expenseDescription,string searchString)
		{
			if (_context.Expense == null)
			{
				return Problem("Entity set 'SpendSmartDbContext.Expenses'  is null.");
			}

            IQueryable<string> descriptionQuery = from m in _context.Expense
                                            orderby m.Description
                                            select m.Description;

            var expenses = from m in _context.Expense
				select m;

			if (!String.IsNullOrEmpty(searchString))
			{
				expenses = expenses.Where(s => s.Description!.ToUpper().Contains(searchString.ToUpper()));
			}

			if (!string.IsNullOrEmpty(expenseDescription))
			{
				expenses = expenses.Where(x => x.Description == expenseDescription);
			}

			var doubleVM = new DoubleViewModel()
			{
				Descriptions = new SelectList(await descriptionQuery.Distinct().ToListAsync()),
				Expenses = await expenses.ToListAsync()
			};

			return View(doubleVM);


		}

		// GET: Expenses/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doubleVM = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doubleVM == null)
            {
                return NotFound();
            }

            return View(doubleVM);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Description")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,Description")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expense.FindAsync(id);
            if (expense != null)
            {
                _context.Expense.Remove(expense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.Id == id);
        }
    }
}
