using Dapper;
using LabourPayment.Model.Interfaces;
using LabourPayment.Model.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace LabourPayment.Options
{
    public class AppState
    {
        public IDictionary<string, TokenInfo> users;
        public List<TokenUser> TokenUserList;
        private IOptions<JwtIssuerOptions> _options;
        //private IConnectionDbInfo _connectionDbInfo { get; }
        private IInitialDal _initialDal { get; }
        public AppState(IOptions<JwtIssuerOptions> options, IInitialDal initialDal)
        {
            users = new Dictionary<string, TokenInfo>();
            TokenUserList = new List<TokenUser>();
            _options = options;
            //_connectionDbInfo = connectionDbInfo;
            _initialDal = initialDal;
        }

        public ConnectingUser checkUser(string token)
        {
            TokenUser tokenRecord = null;
            TokenInfo userToken; 
            var domainConnectionString = _initialDal.GetMasterDbConnectionString();
            if (tokenRecord != null)
            {
                userToken = tokenRecord.User;
                var conuser = new ConnectingUser()
                {
                    User = userToken.User,
                    Password = userToken.Password,
                    UserConnectionString = userToken.DomainConnectionString,
                };
                return conuser;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(domainConnectionString))
                {
                    var tk = con.ExecuteScalar<string>("SELECT [User] FROM USERLOGINTOKENLIST WHERE Token=@Token", 
                        new {Token = token });
                    userToken = JsonConvert.DeserializeObject<TokenInfo>(tk);
                    var conuser = new ConnectingUser()
                    {
                        User = userToken.User,
                        Password = userToken.Password,
                        UserConnectionString = userToken.DomainConnectionString,
                    };
                    TokenUser TU = new TokenUser
                    {
                        Token = token,
                        User = userToken
                    };
                    var tR = TokenUserList.Find(x => x.Token == token);
                    if (tR == null)
                    {
                        this.TokenUserList.Add(TU);
                    }
                    return conuser;
                }
            }
            return null;
        }


        public ConnectingUser GetConnectionUser(HttpRequest request)
        {
            try
            {
                StringValues token;
                var url = request.Headers["Referer"].ToString();
                request.Headers.TryGetValue("Authorization", out token);
                ConnectingUser conUser = checkUser(token.ToString());
                if (conUser == null) throw new Exception("User login timeout or login user not found");
                conUser.MasterDBConnectionString = _initialDal.GetInitialConnection();
                return conUser;
            }
            catch (Exception ex) {
                throw new Exception("Connecting User error"+ ex.Message);
            }
        }
    }

    public class TokenInfo
    {
        private long _expiry;
        public string User { get; set; }
        public string Password { get; set; }
        public DateTime IssueAt { get; set; }
        public TimeSpan ValidFor {  get; set; }
        public string DomainConnectionString { get; set; }
        public long StartTime
        {
            get { return ToUnixEpochDate(IssueAt); }
        }
        public long Expiry
        {
            get { return ToUnixEpochDate(IssueAt + ValidFor); }

        }

        private static long ToUnixEpochDate(DateTime date)
            =>(long)Math.Round((date.ToUniversalTime()-new DateTimeOffset(1970,1,1,0,0,0,TimeSpan.Zero)).TotalSeconds);
    }

    public class TokenUser
    {
        public TokenInfo User { get; set; }
        public string Domain { get; set; }
        public string Token { get; set; }
    }
}
