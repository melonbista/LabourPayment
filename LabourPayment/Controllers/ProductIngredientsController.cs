using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class ProductIngredientsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductIngredientsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Route("api/getIngredient")]
        [HttpGet]
        public async Task<IActionResult> GetIngredient()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string qry = "select * from Ingredient";
                var res = await con.QueryAsync(qry);
                return Ok(res);
            }
        }

        [HttpPost("api/saveIngredient")]
        public async Task<IActionResult> SaveIngredient(InputModel input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string qry = "Insert Into Ingredient(Code,ItemName,Unit,Quantity,Rate,Amount,Batch,Remarks)" +
                    "Values(@Code,@ItemName,@Unit,@Quantity,@Rate,@Amount,@Batch,@Remarks)";
                var res = await con.ExecuteScalarAsync(qry,
                    new
                    {
                        Code = input.Code,
                        ItemName = input.ItemName,
                        Unit = input.Unit,
                        Quantity = input.Quantity,
                        Rate = input.Rate,
                        Amount = input.Quantity * input.Rate,
                        Batch = input.Batch,
                        Remarks = input.Remarks,
                    });
                return Ok(res);
            }
        }


        public class BaseModel
        {
            public string Code { get; set; }
            public string ItemName { get; set; }
            public string Unit { get; set; }
            public int Quantity { get; set; }
            public int Rate { get; set; }
            public int Amount { get; set; }
            public string Batch { get; set; }
            public string Remarks { get; set; }
        }

        public class InputModel : BaseModel { }
    }
}