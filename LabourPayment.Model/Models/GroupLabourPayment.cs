using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class GroupLabourPayment
    {
        public int Id { get; set; }
        public string ProducerName { get; set; }
        public int Income { get; set; }
        public int Commission { get; set; }
        public int NetIncome { get; set; }
        public int H_Allow { get; set;}
        public int Emg_Fund { get; set;}
        public int M_Allow { get; set; }
        public int W_Incentive { get; set; }
        public int DailyWage { get; set;}
        public int Extra { get; set;}
        public int Total { get; set;}
        public int TDS { get; set;}
    }
}
