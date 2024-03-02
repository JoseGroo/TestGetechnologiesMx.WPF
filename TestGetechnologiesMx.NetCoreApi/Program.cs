using Microsoft.EntityFrameworkCore;
using TestGetechnologiesMx.Logic;
using TestGetechnologiesMx.Repository;
using TestGetechnologiesMx.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProjectModel>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqliteDbContext"));
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonLogic, PersonLogic>();

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceLogic, InvoiceLogic>();

var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
