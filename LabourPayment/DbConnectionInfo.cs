using LabourPayment.Model.Interfaces;
using LabourPayment.Model.Models;
using System.Data.SqlClient;

namespace LabourPayment
{
    public class DbConnectionInfo
    {
        public string error;
        private string _connectionString;
        private IInitialDal _initialDal;

        public DbConnectionInfo(IInitialDal initialDal)
        {
            _initialDal = initialDal;
        }

        public string ConnectionString { get { return _connectionString; } }

        public string SetConnectionString(string user, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.UserID = user;
            builder.Password = password;
            builder.InitialCatalog = _initialDal.Database;
            builder.DataSource = _initialDal.Server;
            _connectionString = builder.ConnectionString;
            return _connectionString;
        }

        public string GetConnectionString()
        {
            _connectionString = _initialDal.GetInitialConnection();
            return _connectionString;
        }

        public string GetConnectionString(ConnectingUser conUser)
        {
            _connectionString = _initialDal.GetInitialConnection();
            return _connectionString;
        }

        public ConnectingUser GetConnectingUser(ConnectingUser conUser)
        {
            try
            {
                if (conUser != null)
                {
                    _connectionString = _initialDal.GetInitialConnection();
                    conUser.UserConnectionString = ConnectionString;
                }
                return conUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Connection User error" + ex.Message, ex);
            }
        }
        public ConnectingUser GetUserConnection(ConnectingUser conUser)
        {
            try
            {
                if(conUser != null)
                {
                    _connectionString = _initialDal.GetInitialConnection();
                    conUser.UserConnectionString = ConnectionString;
                }
                return conUser;
            }catch (Exception ex)
            {
                throw new Exception("Connecting user error"+ex.Message);
            }
        }
    }
}
