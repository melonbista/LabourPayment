using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class LabourRateSetup
    {
        public string MaterialType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Design { get; set; }
        public string Color { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Units { get; set; }
        public string Size { get; set; }
        public int PurRate { get; set; }
        public int Variance { get; set; }
        public int FOB { get; set; }
        public int LabourRate { get; set; }
        public int SalesRate { get; set; }
        public int CommissionRate { get; set; }
        public int CreditRate { get; set; }
        public int ThreadRate { get; set; }
        public string Remarks { get; set; }
        public string BarcodeSize { get; set; }
        public int UniqueCode { get; set; }
        public int IsActive { get; set; }
    }
}
