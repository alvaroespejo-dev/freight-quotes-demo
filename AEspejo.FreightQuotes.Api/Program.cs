using AEspejo.FreightQuotes.Api.Extensions;
using AEspejo.FreightQuotes.Api.Hubs;
using AEspejo.FreightQuotes.Application.Extensions;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.CarrierApiClient.Extensions;
using AEspejo.FreightQuotes.Infrastructure.Extensions;
using AEspejo.FreightQuotes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFreightQuotesDbContext(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddRepositories();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApplicationServices();
builder.Services.AddClients();

//builder.Services.AddCors(options=> {
//    options.AddPolicy("AllowAngularApp",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:49672", "https://tu-dominio.com")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod()
//                  .AllowCredentials(); 
//        });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<FreightQuotesDbContext>();
        db.Database.Migrate();
    }
    //builder.Services.AddHttpsRedirection(options =>
    //{
    //    options.HttpsPort = 7129;
    //});
    //app.UseDeveloperExceptionPage();
    //app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

//app.UseCors("AllowAngularApp");
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<RateQuoteHub>("/hubs/rate-quote");

app.Run();
