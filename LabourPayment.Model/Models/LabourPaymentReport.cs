using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class LabourPaymentReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Month { get; set; }
        public string Department { get; set; }
        public int SNo { get; set; }
        public string ProdcerName { get; set; }
        public int TotalEarning { get; set; }
        public string HousingAllowance { get; set; }
        public string EMR { get; set; }
        public string Incentive { get; set;}
        public int DailyWage { get; set; }
        public string Extra { get; set; }
        public int GrossIncome { get; set; }
        public int TDS { get; set; }
        public int ThreadPayment { get; set; }
        public int ThreadSold { get; set; }
        public int NetGross { get; set; }
    }
}
