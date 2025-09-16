using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    [Alias("Operations")]
    public class InfoOperation
    {
        public int Id { get; set; }
        public string IdName { get; set; }
        public OperationType Type { get; set; }
        public int Ammount { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"Operation {Id} {IdName} {Type} {Ammount} {Time}";
        }
    }

    public enum OperationType
    {
        Income,
        Outcome,
        Transfer,
        Paycheck
    }
}