using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class ProducerIssueReport
    {
        public int Id { get; set; } 
        public int ManNo { get; set; }
        public int IssNo { get; set; }
        public int JobN { get; set; }
        public DateTime Date { get; set; }
        public string Producer { get; set; }
        public string Department { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }

    }
}
