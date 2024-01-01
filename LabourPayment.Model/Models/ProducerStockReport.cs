using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class ProducerStockReport
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int CostRate { get; set; }
        public int SaleRate { get; set; }
        public int Stock { get; set; }
        public int CostAmt { get; set; }
        public int SaleAmt { get; set;}
    }
}
