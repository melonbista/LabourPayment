using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class PurchasesController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PurchasesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/all")]
        public async Task<IActionResult> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Purchase";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }

        [HttpGet("api/getTrnMain")]
        public async Task<IActionResult> GetTrnMain(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select ProducerId, InvoiceNo,ProducerName,ProducerAddress,Date,Job,NonJob" +
                    "from Purchase where ProducerId=@producerId";
                var res = await con.QueryAsync(query, new { ProdcerId = id });
                return Ok(res);
            }
        }

        [HttpGet("api/getPurchaseItemList")]
        public async Task<IActionResult> GetSales()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select Code,ItemName, Unit,Quantity,Rate,Amount,Remarks from Purchase";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }

        [HttpPost("api/savePurchaseItem")]
        public async Task<IActionResult> SaveSalesItem(ItemList input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Insert Into Purchase(Code,ItemName,Unit,Quantity,Rate,Amount,Remarks)" +
                    "values(@Code,ItemName,@Unit,@Quantity,@Rate,@Amount,@Remarks)";
                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        Code = input.Code,
                        ItemName = input.ItemName,
                        Unit = input.Unit,
                        Quantity = input.Quantity,
                        Amount = input.Quantity * input.Rate,
                        Remarks = input.Remarks,
                    }
                    );
                return Ok(res);
            }
        }

        [HttpPut("api/Edit")]
        public async Task<IActionResult> Edit(int id, ItemList input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Update Sale Set Code=@Code,ItemName=@ItemName,Unit=@Unit,Quantity=@Quantity," +
                    "Rate=@Rate,Amount=@Amount,Remarks=@Remarks" +
                    "Where ProducerId =@producerId";
                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        ProducerId = id,
                        Code = input.Code,
                        ItemName = input.ItemName,
                        Unit = input.Unit,
                        Quantity = input.Quantity,
                        Rate = input.Rate,
                        Amount = input.Amount,
                        Remarks = input.Remarks,
                    });
                return Ok(res);
            }
        }

        [HttpDelete("api/deletePurchaseItem")]
        public async Task<IActionResult> DeleteSalesItem(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Delete from Purchase where ProducerId = @ProducerId";
                var res = await con.QueryAsync(query, new { ProducerId = id });
                return Ok(res);
            }
        }


        public class TrnMain
        {
            public int ProducerId { get; set; }
            public string InvoiceNo { get; set; }
            public string ProducerName { get; set; }
            public string ProducerAddress { get; set; }
            public DateTime Date { get; set; }
            public int Job { get; set; }
            public int NonJob { get; set; }
        }
        public class ItemList
        {
            public string Code { get; set; }
            public string ItemName { get; set; }
            public string Unit { get; set; }
            public int Quantity { get; set; }
            public int Rate { get; set; }
            public int Amount { get; set; }
            public string Remarks { get; set; }
        }
    }
}
