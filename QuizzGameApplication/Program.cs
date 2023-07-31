using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Service;
using QuizzGameApplication.Services;
using System;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/quizGameLogs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddHealthChecks();

builder.Services.AddControllers(configure =>
{
    configure.ReturnHttpNotAcceptable = true;

    configure.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status400BadRequest));
    configure.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status406NotAcceptable));
    configure.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status500InternalServerError));
    configure.Filters.Add(
        new ProducesResponseTypeAttribute(
            StatusCodes.Status422UnprocessableEntity));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlComentsFile = $"QuizGameWeb.xml";
    var xmlComentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlComentsFile);
    setupAction.IncludeXmlComments(xmlComentsFullPath);
}
);
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAuthenticationRequestService, AuthenticationRequestService>();

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});


//app.MapControllers();

app.Run();
