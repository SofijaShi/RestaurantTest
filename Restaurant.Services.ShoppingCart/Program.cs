using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ShoppingCart;
using Restaurant.Services.ShoppingCart.DbContexts;
using Restaurant.Services.ShoppingCart.RabbitMqSender;
using Restaurant.Services.ShoppingCart.Repository;

IConfiguration Configuration;

var builder = WebApplication.CreateBuilder(args);

Configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddSingleton<IRabbitMQCartMessageSender, RabbitMQCartMessageSender>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
