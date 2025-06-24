using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Mappings;
using WebApplication1.Repositories;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ICheeseRepository, CheeseRepository>();
builder.Services.AddScoped<ICheeseService, CheeseService>();
builder.Services.AddDbContext<CheeseDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CheeseConnectionString")));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();