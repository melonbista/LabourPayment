using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class ConnectingUser
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string UserConnectionString { get; set; }
        public string GlobalConnectionString { get; set; }
        public string MasterDBConnectionString { get; set; }
        public string OrderSyncUrl { get; set; }
    }
}
