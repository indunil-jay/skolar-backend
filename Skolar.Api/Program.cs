using Microsoft.AspNetCore.Mvc;
using Skolar.Application;
using Skolar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // Disable automatic 400 responses from ASP.NET
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
