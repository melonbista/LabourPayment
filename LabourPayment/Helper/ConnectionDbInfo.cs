using LabourPayment.Model.Interfaces;
using LabourPayment.Model.Models;
using LabourPayment.Options;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;

namespace LabourPayment.Helper
{
    public class ConnectionDbInfo : IConnectionDbInfo
    {
        public IWebHostEnvironment env;
        public string Error;
        public AppState _appState;
        private string _connectionString;
        private IInitialDal _initialDal;
        private DbConnectionInfo _dbConnectionInfo;

        public IWebHostEnvironment getEnv {  get { return env; } }

        public ConnectionDbInfo(
            IWebHostEnvironment _env, 
            AppState appState, 
            IInitialDal initialDal,
            DbConnectionInfo dbConnectionInfo)
        {
            env = _env;
            _appState = appState;
            _initialDal = initialDal;
            _dbConnectionInfo = dbConnectionInfo;
        }

        public string ConnectionString { get { return _connectionString; } set { _connectionString = value; } }


        public string GetConnectionString()
        {
            _connectionString = _initialDal.GetInitialConnection();
            return ConnectionString;
        }

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

        public ConnectingUser GetConnectingUser(HttpRequest request)
        {
            try
            {
                StringValues token;
                request.Headers.TryGetValue("Authorization", out token);
                ConnectingUser conUser = _appState.checkUser(token.ToString());
                if (conUser != null)
                {
                    _connectionString = _initialDal.GetInitialConnection();
                    conUser.UserConnectionString = ConnectionString;
                }
                return conUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Connectiong user error" + ex.Message);
            }
        }
    }
}
