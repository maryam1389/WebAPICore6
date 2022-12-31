using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole(); /// مربوط به لاگر میشود می توان تنظیمات رو درست کرد
//و براساس آن چیزی که می خواهید لاگ بزنید

// Add services to the container.

//builder.Services.AddMvc();
//builder.Services.AddControllersWithViews();

builder.Services.AddControllers(options => // باعث میشه دو خروجی فایل با جوجه به در خواست کلایت ، جی سان و ایکس ام ال رو داشته باشه 
{
    options.ReturnHttpNotAcceptable = true;
}) 
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddTransient<LocalMailService>(); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("HelloWord");
//});

app.UseHttpsRedirection();
app.UseRouting(); // موتور مسیربابی رو روشن می کند
app.UseAuthorization(); //کنترل دسترسی ها رو فعال می کند


// controller/Action/{id?}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); //کنترل ها رابه مسیربابی اضافه می کند

});
//app.MapControllers();


app.Run();
