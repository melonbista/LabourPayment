using Dapper;
using LabourPayment.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class LabourPaymentsSheetController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LabourPaymentsSheetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/getLabourPaymentSheet")]
        public async Task<IActionResult> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from LabourGroupPayment";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }


        [HttpGet("api/getLabourPaymentfromDate")]
        public async Task<IActionResult> Get(DateTime month,DateTime year, string department)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var date = month.Month;
                var date2 = year.Year;
                string query = "select * from LabourGroupPayment where Month=@Month " +
                    "and Year=@Year and Department=@Department";

                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        month = date,
                        year = date2,
                        Department = department
                    });
                return Ok(res);
            }
        }

        [HttpPost("api/savelabourPaymentSheet")]
        public async Task<IActionResult> Save(LabourPaymentReport input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();

                string query = "insert Into LabourPaymentReport(Id,Date,Department,SNo,ProducerName,TotalEarning," +
                    "HousingAllowance,EMR,Incentive,DailyWage,GrossIncome,TDS,ThreadPayment,ThreadSold,NetGross)" +
                    "values(@Id,@Date,@Department,@SNo,@ProducerName,@TotalEarning,@HousingAllowance,@EMR,@Incentive," +
                    "@DailyWage,@GrossIncome,@TDS,@ThreadPayment,@ThreadSold,@NetGross)";

                var totalEarning = con.QueryAsync<int>($"Select Amount from DailyWage where Department=@Department" +
                    $"Producer=@Producer", new
                    {
                        Dapartment = input.Department,
                        Producer = input.ProdcerName
                    });

                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        Id = input.Id,
                        Date = input.Date,
                        Department = input.Department,
                        SNo = input.SNo,
                        ProducerName = input.ProdcerName,
                        totalEarning = input.TotalEarning,
                        HousingAllowance = input.HousingAllowance,
                        EMR = input.EMR,
                        Incentive = input.Incentive,
                        DailyWage = input.DailyWage,
                        GrossIncome = input.GrossIncome,
                        TDS = input.TDS,
                        ThreadPayment = input.ThreadPayment,
                        ThreadSold = input.ThreadSold,
                        NetGross = input.GrossIncome - input.TDS + input.ThreadPayment - input.ThreadSold
                    });
                return Ok(res);
            }
        }

    }
}
