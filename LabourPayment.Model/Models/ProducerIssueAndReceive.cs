using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class ProducerIssueAndReceive
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string RegNo { get; set; }
        public string FromDepartment { get; set; }
        public string Trans { get; set; }
        public string Department { get; set; }
        public int IssueNo { get; set; }
        public string TransType { get; set; }
        public int ManNo { get; set; }
        public int IssNo { get; set; }
        public int JobN { get; set; }
        public string Producer { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}
