global using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Newtonsoft.Json.Converters;
using storeBL;
using storeDL;
using StoreAppApi;
using storeModel;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddAuthorization();

Log.Logger = new LoggerConfiguration()
        .WriteTo.File("./Logs/user.txt")
        .CreateLogger();

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepo>(repo => new UserRepo(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<ICustomerRepo>(repo => new CustomerRepo(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<ICustomerBL, CustomerBL>();
builder.Services.AddScoped<IInventoryRepo>(repo => new InventoryRepo(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IInventoryBL, InventoryBL>();

var app = builder.Build();
builder.Services.AddEndpointsApiExplorer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();
