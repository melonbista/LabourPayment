using LabourPayment.Model;
using LabourPayment.Model.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json.Serialization;

namespace LabourPayment.Helper
{
    public class InitailDal : IInitialDal
    {
        private readonly ILogger _logger;
        private string _user;
        private string _password;
        private string _database;
        private string _server;
        private string _terminal;
        private string _division;
        private string _mainCon;
        private string _orderSyncUrl;

        public string User { get { return _user; } }
        public string Database { get { return _database; } }
        public string Server { get { return _server; } }
        public string Terminal { get { return _terminal; } }
        public string Division { get { return _division; } }
        public string MainCon { get { return _mainCon; } }
        public string OrderSyncUrl { get { return _orderSyncUrl; } }

        public string AppPath { get; }

        public IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string Error;

        public InitailDal(
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<InitailDal>();
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            AppPath = env.ContentRootPath;
        }

        public string GetInitialConnection()
        {
            return GetMasterDbConnectionString();
        }

        public string GetMasterDbConnectionString()
        {
            try
            {
                if (File.Exists(Path.Combine(AppPath, "DbConnection.txt")))
                {
                    using(StreamReader st = File.OpenText(Path.Combine(AppPath, "DbConnection.txt")))
                    {
                        var jsonStr = st.ReadToEnd();
                        var res = JsonConvert.DeserializeObject<JObject>(jsonStr);
                        SqlConnectionStringBuilder sbr = new SqlConnectionStringBuilder();
                        _user = sbr.UserID = res["USER"].ToString();
                        _password = sbr.Password = Encrypt(HexadecimalEncoding.ToHexString(res["PASSWORD"].ToString()));
                        _database = sbr.InitialCatalog = res["DATABASE"].ToString();
                        _server = sbr.DataSource = res["SERVER"].ToString();
                        _orderSyncUrl = res["ORDERSYNCURL"].ToString();
                        return sbr.ConnectionString;
                    }
                }
                else { return "DbConnection file not found."; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Encrypt(string txtValue, string key = "AmitLalJoshi")
        {
            try
            {
                int i;
                string textChar;
                string keyChar;
                string retMsg = "";
                int ind = 1;

                for(i= 1; i <= Convert.ToInt32(txtValue.Length); i++)
                {
                    textChar = txtValue.Substring(i - 1, 1);
                    ind = i % key.Length;
                    keyChar = key.Substring(ind);
                    byte str1 = Encoding.ASCII.GetBytes(textChar)[0];
                    byte str2 = Encoding.ASCII.GetBytes(keyChar)[0];
                    var encData = str1 ^ str2;
                    retMsg = retMsg + Convert.ToChar(encData).ToString();
                }
                return retMsg;
            }
            catch(Exception ex)
            {
                return "error";
            }
        }
    }
}
