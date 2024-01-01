using Dapper;
using LabourPayment.Model.Interfaces;
using LabourPayment.Model.Models;
using System.Data;
using System.Data.SqlClient;
namespace LabourPayment.DataAccess
{
    public class ProducerDal
    {
        private IConnectionDbInfo _dbcon;

        public ProducerDal(IConnectionDbInfo con)
        {
            _dbcon = con;
        }

        public async Task<FunctionResponse> GetProducerList(ConnectingUser conUser)
        {
            string constr = conUser.UserConnectionString;
            using (IDbConnection con = new SqlConnection(constr))
            {
                try
                {
                    string qry = "select * from Producer";
                    var res = await con.QueryAsync(qry);
                    return new FunctionResponse { status = "ok", result = res };
                }
                catch (Exception ex)
                {
                    return new FunctionResponse { status = "error", result = ex.Message };
                }
            }
        }



    }
}