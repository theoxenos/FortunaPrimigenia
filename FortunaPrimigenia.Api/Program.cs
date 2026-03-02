using FortunaPrimigenia.Api.Data;
using FortunaPrimigenia.Api.Repositories;
using FortunaPrimigenia.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<FortunaPrimigeniaContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                           ?? "Data Source=./db.sqlite";
    options.UseSqlite(connectionString,
        sqliteOptions => sqliteOptions.MigrationsAssembly(typeof(FortunaPrimigeniaContext).Assembly.FullName));
});

builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();

builder.Services.AddScoped<IAccountsService, AccountsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();