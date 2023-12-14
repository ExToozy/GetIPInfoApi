using GetIPInfoApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

//private readonly IPInformationDbContext _dbContext;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IPInformationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "Get IP info");
    });
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<IPInformationDbContext>();
    db.Database.EnsureCreated();
}


app.MapGet("/history", async (IPInformationDbContext db) => 
    await db.QueryHistory.ToListAsync());

app.MapGet("ip/{ipAdress}", async (string ipAdress, IPInformationDbContext db) =>
{
    var response = await IPExternalApi.GetIpInfoFromExternalApi(ipAdress);
    IPInformation? ipInfo = JsonSerializer.Deserialize<IPInformation>(response);
    if (ipInfo != null)
    {
        var ip = await db.QueryHistory.FirstOrDefaultAsync(i => i.Ip == ipAdress);
        if (ip == null)
        {
            if (db.QueryHistory.Count() > 0)
            {
                ipInfo.Id = db.QueryHistory.OrderBy(x => x.Id).Last().Id + 1;
            }
            else
            {
                ipInfo.Id = 1;
            }
            ipInfo.Status = 200;
            db.QueryHistory.Add(ipInfo);
            await db.SaveChangesAsync();
        }
    }
    return response;
});

app.UseHttpsRedirection();
app.Run();
