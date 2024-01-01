using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class DailyWageAdjustment
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Producer { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; } 
        public int DailyWage { get; set; }
        public int ThreadSold { get; set; }
        public string Remarks { get; set; }
    }
}
