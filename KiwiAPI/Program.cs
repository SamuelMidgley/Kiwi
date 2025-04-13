using FluentValidation;
using KiwiAPI.Helpers;
using KiwiAPI.Repositories;
using KiwiAPI.Services;
using KiwiAPI.Services.ContentCreation;
using KiwiAPI.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddSingleton<DataContext>();

builder.Services.AddTransient<IContentRepository, ContentRepository>();
builder.Services.AddTransient<IContentService, ContentService>();

builder.Services.AddTransient<ToDoContentCreationService>();
builder.Services.AddTransient<IContentCreationFactory, ContentCreationFactory>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();