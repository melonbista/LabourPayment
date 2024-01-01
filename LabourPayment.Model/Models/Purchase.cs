using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class Purchase
    {
        public string ProducerId { get; set; }
        public string InvoiceNo { get; set; }
        public string ManualNo { get; set; }
        public DateTime DateTime { get; set; }
        public string ProducerName { get; set; }
        public string ProducerAddress { get; set; }
        public int Job { get; set; }
        public int NonJob { get; set; }
        public string Code { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public int TotalAmount { get; set; }
        public int Discount { get; set; }
        public int Vat { get; set; }
        public int GrandTotal { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Department { get; set; }
        public string ReceivedBy { get; set; }
        public string IssuedBy { get; set; }
        public string Remarks { get; set; }
    }
}
