using storeBL;
using storeDL;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepo>(repo => new CustomerRepo(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<ICustomerBL, CustomerBL>();
builder.Services.AddScoped<IInventoryRepo>(repo => new InventoryRepo(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IInventoryBL, InventoryBL>();


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
