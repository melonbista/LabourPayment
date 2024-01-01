using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public int NoOfChildren { get; set; }
        public string Pan { get; set; }
        public DateTime Date { get; set; }
        public string RegNo { get; set; }
        public string Post { get; set; }
        public string Education { get; set; }
        public string RegisteredUnit { get; set; }
        public string OrganizationName { get; set; }
        public string GrHead { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
    }


    public enum Type
    {
        Individual,
        Group,
        Staff
    }
}
