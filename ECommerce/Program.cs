using Core;
using ECommerce.Helpers;
using ECommerce.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<ECommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// FluentValidation services
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CustomerDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrderCreateUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrderItemCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrderStatusUpdateDtoValidator>();

builder.Services.AddAutoMapper(typeof(ECommerceProfile));


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
