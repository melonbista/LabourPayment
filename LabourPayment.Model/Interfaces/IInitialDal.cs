using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Interfaces
{
    public interface IInitialDal
    {
        string Database { get; }
        string Division { get; }
        string Server {  get; }
        string Terminal { get; }
        string User { get; }
        string AppPath {  get; }
        string MainCon {  get; }
        string Encrypt(string txtValue, string key = "AmitLalJoshi");
        string GetInitialConnection();
        string GetMasterDbConnectionString();
    }
}
