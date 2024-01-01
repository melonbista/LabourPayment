using Dapper;
using FluentValidation;
using LabourPayment.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class ProducerReceivesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProducerReceivesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/getProducerReceiveIngredient")]
        [HttpGet]
        public async Task<IActionResult> GetProducerReceiveIngedient()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from ReceiveIngredient";
                var res = await con.QueryAsync(query);

                return Ok(res);
            }
        }

        [Route("api/saveReceivedIngredient")]
        [HttpPost]
        public async Task<IActionResult> SaveReceivedIngredient(AddInput input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert into ReceiveIngredient(Code,ItemName,Unit,CtrlNo,Quantity,Rate,Amount,Remarks)" +
                    "values(@Code,@ItemName,@Unit,@CtrlNo,@Quantity,@Rate,@Amount,@Remarks)";
                var res = await con.QueryAsync(query, new
                {
                    Code = input.Code,
                    ItemName = input.ItemName,
                    Unit = input.Unit,
                    CtrlNo = input.CtrlNo,
                    Quantity = input.Quantity,
                    Rate = input.Rate,
                    Amount = input.Amount,
                    Remaeks = input.Remarks
                });

                return Ok(res);
            }
        }


        //[Route("api/delete")]
        //[HttpDelete]
        //public async Task<IActionResult>
        //[Route("api/saveThreadPurchase")]


        [HttpPut("api/editReceiveProducer")]
        public async Task<IActionResult> Edit(AddInput input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection())
            {
                string updateQuery = "Update ReceiveIngredient set ItemName=@ItemName,Code=@Code,Unit=@Unit,CtrlNo=@CtrlNo," +
                    "Quantity=@Quantity,Rate=@Rate,Amount=@Amount,Remarks=@Remarks where Id = @Id";
                var res = await con.QueryAsync(
                    updateQuery,
                    new
                    {
                        Id=input.Id,
                        ItemName= input.ItemName,
                        Code = input.Code,
                        Unit = input.Unit,
                        CtrlNo = input.CtrlNo,
                        Quantity= input.Quantity,
                        Rate = input.Rate,
                        Amount = input.Amount,
                        Remarks = input.Remarks
                    });
                return Ok(res);
            }

        }

        [HttpDelete("api/deleteReceiveProducer")]
        public async Task<IActionResult> Delete(AddInput input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Drop from ReceiveReoducer where Id=@Id";
                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        Id = input.Id
                    });
                return Ok(res);
            }
        }

        public class BaseInputModel
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string ItemName { get; set; }
            public string Unit { get; set; }
            public int CtrlNo { get; set; }
            public int Quantity { get; set; }
            public int Rate { get; set; }
            public int Amount { get; set; }
            public string Remarks { get; set; }
        }

        public class AddInput : BaseInputModel { }

        public class AddInputValidator : AbstractValidator<AddInput>
        {
            private readonly IConfiguration _configuration;
            public AddInputValidator(IConfiguration configuration)
            {
                _configuration = configuration;

                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Code).NotEmpty();
                RuleFor(x => x.ItemName).NotEmpty();
                RuleFor(x => x.Unit).NotEmpty();
                RuleFor(x => x.CtrlNo).NotEmpty();
                RuleFor(x => x.Quantity).NotEmpty();
                RuleFor(x => x.Rate).NotEmpty();
                RuleFor(x => x.Amount).NotEmpty();
                RuleFor(x => x.Remarks).NotEmpty();                
            }
        }


    }
}
