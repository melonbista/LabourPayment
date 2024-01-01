using Dapper;
using LabourPayment.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static LabourPayment.Controllers.ProducerReceivesController;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class ThreadPurchasesController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ThreadPurchasesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/getThreadPurchase")]
        public async Task<IActionResult> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select * from ThreadPurchase";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }

        [HttpGet("api/getThreadPurchase/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select ItemName from ReceiveIngredient where Id = @Id";
                var res = await con.QueryAsync(query, new { Id = id });

                return Ok(res);
            }
        }

        [HttpPost("api/savePurchaseThreadPurchase")]
        public async Task<IActionResult> SaveThreatPurchase(ThreadPurchase input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var productName = "Insert Into ThreadPurchase(Id,PurchaseNo,RegNo,Department,Date,ManualNo," +
                     "Producer,Quantity,Rate,Amount,Remarks) values(@Id,@PurchaseName,@PurchaseNo,@RegNo," +
                     "@Department,@Date,@ManualNo,@Producer,@Quantity,@Rate,@Amount,@Remarks)";

                var res = await con.QueryAsync(productName,
                    new
                    {
                        Id = input.Id,
                        PurchaseNo = input.PurchaseNo,
                        RegNo = input.RegNo,
                        Department = input.Department,
                        Date = DateTime.UtcNow,
                        ManualNo = input.ManualNo,
                        Producer = input.Producer,
                        Quantity = input.Quantity,
                        Rate = input.Rate,
                        Amount = input.Amount,
                        Remarks = input.Remarks
                    });
                return Ok(res);
            }
        }
    }
}
