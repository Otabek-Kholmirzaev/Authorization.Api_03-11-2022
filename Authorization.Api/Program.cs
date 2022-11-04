using Authorization.Api.Models;
using Authorization.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FilePathsOptions>(builder.Configuration.GetSection("FilePaths"));

builder.Services.AddSingleton<IConfigurationsService, ConfigurationsService>();

builder.Services.AddScoped<UsersService>();

builder.Services.AddScoped<SolutionService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
