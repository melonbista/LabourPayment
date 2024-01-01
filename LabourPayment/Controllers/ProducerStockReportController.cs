using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class ProducerStockReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProducerStockReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/GetProducerStockReport")]
        public async Task<IActionResult> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select * from ProductStockReport";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }
        
        [HttpGet("api/GetProducerStockReportFromDepartment")]
        public async Task<IActionResult> Get(string department)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select * from ProductStockReport where Department = @Department";
                var res = await con.QueryAsync(query, new {Department = department});
                return Ok(res);
            }
        }


        [HttpPost("api/ProductStockReport")]
        public async Task<IActionResult> Save(AddModel input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Insert Into ProductStockReport(Code,Name,Department,Unit,CostRate,SaleRate,Stock,CostAmt,SaleAmt)" +
                    "values(@Code,@Name,@Department,@Unit,@CostRate,@SaleRate,@Stock,@@CostAmt,@SaleAmt)";
                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        Code = input.Code,
                        Name = input.Name,
                        Unit = input.Unit,
                        Department = input.Department,  
                        CostRate = input.CostRate,
                        SaleRate = input.SaleRate,
                        Stock = input.Stock,
                        CostAmt = input.CostAmt,
                        SaleAmt = input.SaleAmt,
                    });
                return Ok(res);
            }
        }

        [HttpPut("api/Update")]
        public async Task<IActionResult> Update(UpdateInputModel input,int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Update ProductStockReport Set Code=@Code,Name=@Name,Unit=@Unit,CostRate=@CostRate," +
                    "SaleRate=@SaleRate,Stock=@Stock,CostAmt=@CostAmt,SaleAmt=@SaleAmt where Id =@Id";
                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        id = id,
                        Code = input.Code,
                        Name = input.Name,
                        Department = input.Department,
                        Unit = input.Unit,
                        CostRate = input.CostRate,
                        SaleRate = input.SaleRate,
                        Stock = input.Stock,
                        CostAmt = input.CostAmt,
                        SaleAmt = input.SaleAmt,
                    });
                return Ok(res);
            }
        }

        [HttpDelete("api/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection con = new SqlConnection( connectionString))
            {
                string query = "Delete from ProductStockReport where Id = @Id";
                var res = await con.QueryAsync(query, new { Id = id });
                return Ok(res);
            }
        }

        public class BaseInputModel
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public string Unit { get; set; }
            public int CostRate { get; set; }
            public int SaleRate { get; set; }
            public double Stock { get; set; }
            public int CostAmt { get; set; }
            public double SaleAmt { get; set; }
        }

        public class AddModel : BaseInputModel {
        }
        public class UpdateInputModel : BaseInputModel { }

        public class  AddModelValidator : AbstractValidator<AddModel>
        {
            private readonly IConfiguration _configuration;

            public AddModelValidator(IConfiguration configuration)
            {
                _configuration = configuration;

                RuleFor(x => x.Id)
                    .NotEmpty();

                RuleFor(x => x.Code).
                    NotEmpty();

                RuleFor(x=>x.Name).NotEmpty();
                RuleFor(x=> x.Department).NotEmpty();
                RuleFor(x=> x.Unit).NotEmpty();
                RuleFor(x=> x.CostRate).NotEmpty();
                RuleFor(x=> x.SaleRate).NotEmpty();
                RuleFor(x=> x.Stock).NotEmpty();
                RuleFor(x=> x.CostAmt).NotEmpty();
                RuleFor(x=> x.SaleRate).NotEmpty();
            }
        }
    }
}
