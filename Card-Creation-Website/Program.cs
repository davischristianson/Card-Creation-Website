using Azure.Storage.Blobs;
using Card_Creation_Website.Data;
using Card_Creation_Website.Models;
using Card_Creation_Website.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CardCreationContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailProvider, EmailProviderSendGrid>();

// Allow section access in Views
// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();\
builder.Services.AddHttpContextAccessor();

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-7.0#configure-session-state
// Add Session - Part 1 of 2
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddAzureClients(clientBuilder =>
{
    // clientBuilder.AddBlobServiceClient(builder.Configuration["AzureBlobStorage:blob"]!, preferMsi: true);
    clientBuilder.AddBlobServiceClient(builder.Configuration["ConnectionStrings:AzureBlobStorage:blob"]!, preferMsi: true);
    // clientBuilder.AddQueueServiceClient(builder.Configuration["AzureBlobStorage:queue"]!, preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["ConnectionStrings:AzureBlobStorage:queue"]!, preferMsi: true);
    clientBuilder.AddFileServiceClient(builder.Configuration["ConnectionStrings:AzureBlobStorage"]);
});
// builder.Services.AddScoped<AzureBlobService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add Session - Part 2 of 2
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
