using API;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MqttClient = MQTTnet.Client.MqttClient;

var builder = WebApplication.CreateBuilder(args);
var mqtt = new MqttService();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding the DB to the context 
builder.Services.AddDbContext<PhotoDbContext>(options => options.UseSqlite(
    "Data source = db.db"
));

builder.Services.AddScoped<PhotoRepository>();

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