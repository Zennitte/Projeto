using System.Reflection;
using API.Contexts;
using API.Interfaces;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options => {
	options.AddPolicy("CorsPolicy", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyHeader()
			   .AllowAnyMethod();
	});
});

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto", Version = "v1" });
});

builder.Services.AddDbContext<ProjectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddTransient<IUserRepository, UsersRepository>();
builder.Services.AddTransient<IAccountRepository, AccountsRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionsRepository>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto v1");
	c.RoutePrefix = string.Empty;
});
	
	
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
