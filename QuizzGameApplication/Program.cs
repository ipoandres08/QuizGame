using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Service;
using QuizzGameApplication.Services;
using System;
using Serilog;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using QuizGamePerssistence.Models;

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
    setupAction.AddSecurityDefinition("QuizApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "QuizApiBearerAuth" }
                        }, new List<string>() }
    });

    var xmlComentsFile = $"QuizGameWeb.xml";
    var xmlComentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlComentsFile);
    setupAction.IncludeXmlComments(xmlComentsFullPath);
}
);

builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                    };
                }
);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustHaveAuthorization", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddSingleton<IQuestionService, QuestionService>();
builder.Services.AddSingleton<IQuizService, QuizService>();
builder.Services.AddSingleton<IAuthenticationRequestService, AuthenticationRequestService>();

//builder.Services.AddSingleton<QuizGameContext>();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});


//app.MapControllers();

app.Run();
