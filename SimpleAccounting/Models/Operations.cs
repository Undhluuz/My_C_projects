using System;


namespace SimpleAccounting.DataAccess.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int OperationType { get; set; } // 1 - приход, 2 - расход
        public int CounterpartyId { get; set; }
        public int CategoryId { get; set; }
        public Counterparty RelatedCounterparty { get; set; } // Navigation property
        public int IncomeExpenseCategoryId { get; set; }
        public IncomeExpenseCategory Category { get; set; }
        public string Description { get; set; }
    }
}