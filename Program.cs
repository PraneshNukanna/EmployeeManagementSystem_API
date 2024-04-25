using API_ManagementSystem_ClassActivity.Data;
using API_ManagementSystem_ClassActivity.ServiceLayer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ManSys_DB");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<TitleData>();
builder.Services.AddScoped<EmployeeData>();

builder.Services.AddScoped<TitleService>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
context.Database.EnsureCreated();

//exception handling
app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception != null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = exception.GetBaseException().Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    });
});

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
