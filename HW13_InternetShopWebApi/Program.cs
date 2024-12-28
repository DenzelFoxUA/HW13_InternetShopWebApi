using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Services;
using HW13_InternetShop.Repository;
using HW13_InternetShop;
using HW13_InternetShop.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InternetShopDBContext>(options =>
                                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IShopRepository<HW13_InternetShop.Data.Models.Customer>, CustomerRepository>();
builder.Services.AddScoped<IShopRepository<HW13_InternetShop.Data.Models.Brand>, BrandRepository>();
builder.Services.AddScoped<IShopRepository<HW13_InternetShop.Data.Models.Category>, CategoryRepository>();
builder.Services.AddScoped<IShopRepository<HW13_InternetShop.Data.Models.Product>, ProductRepository>();
builder.Services.AddScoped<IShopRepository<HW13_InternetShop.Data.Models.Order>, OrderRepository>();
builder.Services.AddScoped<IShopRepository<HW13_InternetShop.Data.Models.OrderItem>, OrderItemRepository>();

builder.Services.AddScoped<IShopServices<Customer>, CustomerService>();
builder.Services.AddScoped<IShopServices<Brand>, BrandServices>();
builder.Services.AddScoped<IShopServices<Category>, CategoryServices>();
builder.Services.AddScoped<IShopServices<Product>, ProductServices>();
builder.Services.AddScoped<IShopServices<Order>, OrderServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IShopServices<OrderItem>, OrderItemServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");


//apply simulation
DB_Simulator.GenerateCustomers();
DB_Simulator.GenerateBrands();
DB_Simulator.GenerateCategories();

app.Run();
