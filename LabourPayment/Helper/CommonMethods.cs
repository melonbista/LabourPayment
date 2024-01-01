using LabourPayment.Model.Models;
using LabourPayment.Options;

namespace LabourPayment.Helpers
{
    public class CommonMethods
    {
        private AppState _appState { get; }
        private DbConnectionInfo _con { get; }
        public CommonMethods(AppState appState, DbConnectionInfo connectionDbInfo)
        {
            _appState = appState;
            _con = connectionDbInfo;
        }
        public ConnectingUser GetConnetingUser(HttpRequest request)
        {
            //getting transation user from request
            Microsoft.Extensions.Primitives.StringValues token;
            request.Headers.TryGetValue("Authorization", out token);
            ConnectingUser conUser = _appState.checkUser(token.ToString());
            var connectingUser = _con.GetUserConnection(conUser);

            return connectingUser;
        }

        public string GetConnectionString(HttpRequest request)
        {
            Microsoft.Extensions.Primitives.StringValues token;
            request.Headers.TryGetValue("Authorization", out token);
            ConnectingUser conUser = _appState.checkUser(token.ToString());
            var constr = _con.GetConnectionString(conUser);
            return constr;
        }
    }
}
