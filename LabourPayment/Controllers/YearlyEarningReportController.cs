using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class YearlyEarningReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public YearlyEarningReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("api/getYearlyReport")]
        public async Task<IActionResult> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from YearlyEarningReport";
                var res = await con.QueryAsync(query);
                return Ok(res);
            }
        }

        [HttpGet("api/getYearlyReportFromDate")]
        public async Task<IActionResult> Get(DateTime fiscalYear)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select * from YearlyEarningReport where RegDate=@RegDate";
                var res = await con.QueryAsync(query, new {RegDate=fiscalYear.Year});
                return Ok(res);
            }
        }

        [HttpPost("api/saveYearlyReport")]
        public async Task<IActionResult> Save(AddInputYearly input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Insert Into YealyEarningReport(Id,ProducerName,Unit,RegNo,RegDate," +
                    "Shrawan,Bhadra,Aswin,Kartik,Mangshir,Poush,Magh,Falgun,Chaitra,Baishak,Jheth,Ashar)" +
                    "Values(@Id,@ProducerName,@Unit,@RegNo,@RegDate,@Shrawan,@Bhadra,@Aswin,@Kartik,@Mangshir," +
                    "@Poush,@Magh,@Falgun,@Chaitra,@Baishak,@Jheth,@Ashar)";

                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        Id = input.Id,
                        ProducerName = input.ProducerName,
                        Unit = input.Unit,
                        RegNo = input.RegNo,
                        RegDate = input.RegDate,
                        Shrawan = input.Shrawan,
                        Bhadra = input.Bhadra,
                        Aswin = input.Aswin,
                        Kartik = input.Kartik,
                        Mangshir = input.Mangshir,
                        Poush = input.Poush,
                        Magh = input.Magh,
                        Falgun = input.Falgun,
                        Chaitra = input.Chaitra,
                        Baishak = input.Baishak,
                        Jheth = input.Jheth,
                        Ashar = input.Ashar
                    });
                return Ok(res);
            }
        }



        public class BaseInpputModel
        {
            public int Id { get; set; }
            public string ProducerName { get; set; }
            public string Unit { get; set; }
            public int RegNo { get; set; }
            public DateTime RegDate { get; set; }
            public int Shrawan { get; set; }
            public int Bhadra { get; set; }
            public int Aswin { get; set; }
            public int Kartik { get; set; }
            public int Mangshir { get; set; }
            public int Poush { get; set; }
            public int Magh { get; set; }
            public int Falgun { get; set; }
            public int Chaitra { get; set; }
            public int Baishak { get; set; }
            public int Jheth { get; set; }
            public int Ashar { get; set; }
        }

        public class AddInputYearly : BaseInpputModel { }

        public class AddInputYearlyValidator : AbstractValidator<AddInputYearly>
        {
            private readonly IConfiguration _configuration;
            public AddInputYearlyValidator(IConfiguration configuration)
            {
                _configuration = configuration;

                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.ProducerName).NotEmpty();
                RuleFor(x => x.Unit).NotEmpty();
                RuleFor(x => x.RegNo).NotEmpty();
                RuleFor(x => x.RegDate).NotEmpty();
                RuleFor(x => x.Shrawan).NotEmpty();
                RuleFor(x => x.Bhadra).NotEmpty();
                RuleFor(x => x.Aswin).NotEmpty();
                RuleFor(x => x.Kartik).NotEmpty();
                RuleFor(x => x.Mangshir).NotEmpty();
                RuleFor(x => x.Poush).NotEmpty();
                RuleFor(x => x.Magh).NotEmpty();
                RuleFor(x => x.Falgun).NotEmpty();
                RuleFor(x => x.Chaitra).NotEmpty();
                RuleFor(x => x.Baishak).NotEmpty();
                RuleFor(x => x.Jheth).NotEmpty();
                RuleFor(x => x.Ashar).NotEmpty();
            }
        }
    }
}
