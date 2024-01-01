using Dapper;
using LabourPayment.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Drawing;

namespace LabourPayment.Controllers
{
    [ApiController]
    public class LabourRateSetupController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public LabourRateSetupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("api/saveLabourRate")]
        public async Task<IActionResult> Save(LabourRateSetup input)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Insert Into LabourRateSetup(MaterialType,Category,SubCategory,Design,Color,ItemName," +
                    "ItemCode,Units,Size,PurRate,Variance,FOB,LabourRate,SaleRate,CommissionRate,CreditRate,ThreadRate," +
                    "Remarks,BarcodeSize,UniqueCode,IsActive) values(@MaterialType,@Category,@SubCategory,@Design,@Color," +
                    "@ItemName,@ItemCode,@Units,@Size,@PurRate,@Variance,@FOB,@LabourRate,@SaleRate,@CommissionRate,@CreditRate," +
                    "@ThreadRate,@Remarks,@BarcodeSize,@UniqueCode,@IsActive)";

                var res = await con.QueryAsync(
                    query,
                    new
                    {
                        MaterialType = input.MaterialType,
                        Category = input.Category,
                        SubCategory = input.SubCategory,
                        Design = input.Design,
                        Color = input.Color,
                        ItemName = input.ItemName,
                        ItemCode = input.ItemCode,
                        Units = input.Units,
                        Size = input.Size,
                        PurRate = input.PurRate,
                        Variance = input.Variance,
                        FOB = input.FOB,
                        LabourRate = input.LabourRate,
                        SaleRate = input.SalesRate,
                        CommissionRate = input.CommissionRate,
                        CreditRate = input.CreditRate,
                        ThreadRate = input.ThreadRate,
                        Remarks=input.Remarks,
                        BarcodeSize=input.BarcodeSize,
                        UniqueCode = input.UniqueCode,
                        IsActive=input.IsActive,
                    });
                return Ok(res);
            }
        }

    }
}
