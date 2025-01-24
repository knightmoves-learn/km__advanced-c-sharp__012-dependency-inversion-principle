using Microsoft.AspNetCore.Mvc.Controllers;
using HomeEnergyApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IControllerFactory, ApplicationFactory>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

//Do NOT remove anything below this comment, this is required to autograde the lesson
public partial class Program { }