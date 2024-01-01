using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model.Models
{
    public class FunctionResponse<T>
    {
        public string status { get; set; }
        public T result { get; set; }
        public string Message { get; set; }
        public string RefNo { get; set; }
        public string Location { get; set; }
        public object result2 { get; set; }
        public Exception Ex { get; set; }
        public int TotalRecord { get; set; }

        public FunctionResponse()
        {
            status = "error";
            Message = "Response not set";
        }            

        public FunctionResponse(
            string status,
            string message,
            T Result=default,
            int total = 0,
            dynamic result2=null)
        {
            this.status = status;
            this.Message = message;           
            this.result = Result;
            this.result2 = result2;
        }
    }

    public class FunctionResponse
    {
        public string status { get; set; }
        public string errorLogLink { get; set; }
        public object result { get; set; }
        public string RefNo { get; set; }
        public string Locaation { get; set; }
        public Exception Ex { get; set; }
        public object result2 { get; set; }
        public string message { get; set; }
        public string savedvchrno { get; set; }
        public object resutl3 { get; set; }
        public object resutl4 { get; set; }
        public static FunctionResponse<T>Ok<k,T>(string message,T data,int total,k result2)=>
            new FunctionResponse<T>("ok",message,data,total,result2);
        public static FunctionResponse<T> Ok<T>(string message,T data,int total)=>
            new FunctionResponse<T>("ok",message,data,total);
        public static FunctionResponse<T> Error<T>(string message)=>
            new FunctionResponse<T>("error",message);
    }

    public class FunctionResponseEventArgs : EventArgs
    {
        public FunctionResponse Response;
    }

    public class SFAResponseModel
    {
        public string status { get; set; }
        public object data { get; set; }
        public string message { get; set; }
    }
}
