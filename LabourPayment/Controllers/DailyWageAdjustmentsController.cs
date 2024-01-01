using Dapper;
using LabourPayment.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class DailyWageAdjustmentsController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DailyWageAdjustmentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/getDailyWageAdjustmentList")]
        public async Task<IActionResult> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query= "Select * from DailyWageAdjustment";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }

        [HttpGet("api/getDailyWageFromId")]
        public async Task<IActionResult> Get(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select Department,Producer,Date,Amount,DayWage,ThreadSold,Remarks" +
                    "from DailyWageAdjustment where Id = @Id";
                var res = await con.QueryAsync(query, new {Id=id});
                return Ok(res);
            }
        }

        [HttpPost("SaveDailyWage")]
        public async Task<IActionResult> Save(DailyWageAdjustment input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Insert Into DailyWageAdjustment(Id,Department,Producer,Date,Amount," +
                    "DailyWage,ThreadSold,Remarks) values(@Id,@Department,@Producer,@Date,@Amount," +
                    "@DailyWage,@ThreadSold,@Remarks)";
                var res = await conn.QueryAsync(query, new
                {
                    Id = input.Id,
                    Department = input.Department,
                    Producer = input.Producer,
                    Date = DateTime.UtcNow,
                    Amount = input.Amount,
                    DailyWage = input.DailyWage,
                    ThreadSold = input.ThreadSold,
                    @Remarks = input.Remarks
                });
                return Ok(res);
            }
        }
    }
}
