using Serilog;
using TajneedApi.Api.Extensions;
using TajneedApi.Application.Extensions;
using TajneedApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddDatabase(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureMvc();
builder.Services.AddHealthChecks();
builder.Services.AddMapster();
builder.Services.AddValidators();
builder.Services.AddMediatR();
builder.Services.AddConfigurations(builder.Configuration);


var app = builder.Build();
app.ApplyMigration();
// Configure the HTTP request pipeline.
app.ConfigureExceptionHandler();
app.ConfigureCors();
app.ConfigureSwagger(configuration);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/healthz");


app.Run();
