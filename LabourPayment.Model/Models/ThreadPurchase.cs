using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class ThreadPurchase
    {
        public int Id { get; set; }
        public string ProductName {  get; set; }
        public int PurchaseNo { get; set; }
        public string RegNo { get; set; }
        public string Department { get; set; }
        public DateTime Date { get; set; }
        public string ManualNo { get; set; }
        public string Producer { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public string Remarks { get; set; }
    }
}
