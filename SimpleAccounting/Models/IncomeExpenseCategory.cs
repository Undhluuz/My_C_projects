using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccounting.DataAccess.Models
{
    public class IncomeExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; } // true - доход, false - расход
        // ... другие поля
    }
}
