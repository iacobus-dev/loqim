using Loqim.Api.Services;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Loqim.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IBusinessProfileRepository, BusinessProfileRepository>();
builder.Services.AddScoped<IAiRuleRepository, AiRuleRepository>();
builder.Services.AddScoped<ICatalogProductRepository, CatalogProductRepository>();
builder.Services.AddScoped<ICatalogServiceItemRepository, CatalogServiceItemRepository>();

builder.Services.AddScoped<PromptBuilderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
