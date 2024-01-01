using Dapper;
using LabourPayment.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Transactions;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class ProducerIRReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProducerIRReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/getProducerIssueAndReceive")]
        public async Task<IActionResult> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select RefNo,Date,FromDepartment,Trans,ToDepartment,IssueNo,TransType from " +
                    "ProducerIssueAndReceiveReports";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }

        [HttpGet("api/getIssueAndReportFormDate")]
        public async Task<IActionResult> Get(DateTime fromDate, DateTime toDate)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from ProducerIssueAndReceiveReports where Date between @FromDate and @ToDate";
                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        FromDate = fromDate.ToString("yyyy-MM-dd"),
                        ToDate = toDate.ToString("yyyy-MM-dd")
                    });
                return Ok(res);
            }
        }

        [HttpPost("api/saveIssueAndReport")]
        public async Task<IActionResult> Save(ProducerIssueAndReceive input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection con =new SqlConnection(connectionString))
            {
                string query = "Insert Into ProducerIssueAndReceiveReports(Date,RegNo,FromDepartment,Trans,ToDepartment" +
                    ",IssueNo,TransType) Values(@Date,@RegNo,@FromDepartment,@Trans,@ToDepartment,@IssueNo,@TransType)";
                var res = await con.QueryAsync(
                    query,
                new
                {
                    Date = DateTime.UtcNow,
                    RegNo = input.RegNo,
                    FromDepartment = input.FromDepartment,
                    Trans = input.Trans,
                    ToDepartment = input.Department,
                    IssueNo = input.IssueNo,
                    TransType = input.TransType,
                });

                return Ok(res);
            }
        }

        [HttpGet("api/getProducerIssueReport")]
        public async Task<IActionResult> GetProducerIssueReport(DateTime fromDate, DateTime toDate)
        {
            string connectionString = _configuration.GetConnectionString("DeafaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select ManNo,IssueNo,JobN,Date,Producer,Department,Code,ProductName,Color," +
                    "Quantity from ProducerIssueAndReceiveReports where Date between @fromDate and @toDate";

                var res = await con.QueryAsync(query, new { fromDate, toDate });
                return Ok(res);
            }
        }
    }
}
