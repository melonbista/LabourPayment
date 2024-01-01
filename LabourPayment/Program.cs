using LabourPayment;
using LabourPayment.DataAccess;
using LabourPayment.Helper;
using LabourPayment.Helpers;
using LabourPayment.Model.Interfaces;
using LabourPayment.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IInitialDal, InitailDal>();
builder.Services.AddSingleton<AppState, AppState>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<DbConnectionInfo>();
builder.Services.AddTransient<IConnectionDbInfo, ConnectionDbInfo>();
builder.Services.AddSingleton<CommonMethods, CommonMethods>();
builder.Services.AddTransient<ProducerDal, ProducerDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
