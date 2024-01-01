using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    internal class ProductIngredient
    {
        public string Code { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public string Batch { get; set; }
        public string Remarks { get; set; }
    }
}
