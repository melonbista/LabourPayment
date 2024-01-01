using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Interfaces
{
    public interface IConnectionDbInfo
    {
        string ConnectionString { get; }
        string GetConnectionString();
        string SetConnectionString(string user, string password);
    }
}
