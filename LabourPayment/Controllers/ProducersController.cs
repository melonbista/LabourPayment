using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class ProducersController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProducersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/getProducerList")]
        [HttpGet]
        public async Task<ActionResult> GetProducerList()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Producer";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }

        [Route("api/saveProducer")]
        [HttpPost]
        public async Task<IActionResult> Save(AddInputModel input)
        {
            string conenctionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conenctionString))
            {
                string query = "Insert Into Producer(Name,Type,Code,Address,Gender,Age,MaritalStatus,NoOfChildren," +
                    "Pan,Date,RegNo,Post,Education,RegisteredUnit,OrganizationName,GrHead,IsActive)" +
                    "values(@Name,@Type,@Code,@Address,@Gender,@Age,@MaritalStatus,@NoOfChildren," +
                    "@Pan,@Date,@RegNo,@Post,@Education,@RegisteredUnit,@OrganizationName,@GrHead,@IsActive)";
                var res = await con.ExecuteScalarAsync(query,
                    new
                    {
                        Name = input.Name,
                        Type = input.Type,
                        Code = input.Code,
                        Address = input.Address,
                        Gender = input.Gender,
                        Age = input.Age,
                        MaritalStatus = input.MaritalStatus,
                        NoOfChildren = input.NoOfChildren,
                        Pan = input.Pan,
                        Date = DateTime.UtcNow,
                        RegNo = input.RegNo,
                        Post = input.Post,
                        Education = input.Education,
                        RegisteredUnit = input.RegisteredUnit,
                        OrganizationName = input.OrganizationName,
                        GrHead = input.GrHead,
                        IsActive = input.IsActive,
                    });
                return Ok();
            }
        }

        public class BaseInputModel
        {
            public string Name { get; set; }
            public Type Type { get; set; }  
            public string Code { get; set; }
            public string Address { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
            public string MaritalStatus { get; set; }
            public int NoOfChildren { get; set; }
            public string Pan { get; set; }
            public DateTime Date { get; set; }
            public string RegNo { get; set; }
            public string Post { get; set; }
            public string Education { get; set; }
            public string RegisteredUnit { get; set; }
            public string OrganizationName { get; set; }
            public string GrHead { get; set; }
            public int IsActive { get; set; }
        }
        public enum Type
        {
            Individual,
            Group,
            Staff
        }
        public class AddInputModel : BaseInputModel { }
    }
}