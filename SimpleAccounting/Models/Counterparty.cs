using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccounting.DataAccess.Models
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; } // ИНН
        // ... другие поля
    }
}