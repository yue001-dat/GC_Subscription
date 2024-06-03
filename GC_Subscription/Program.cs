using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GC_Subscription.Data;
using GC_Subscription.Models;
using Stripe;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


//builder.Services.AddControllers();
builder.Services.AddDbContext<GhostchefContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GhostchefContext") ?? throw new InvalidOperationException("Connection string 'GhostchefContext' not found.")));

// Configure Stripe
var stripeSettings = new StripeSettings();
builder.Configuration.GetSection("Stripe").Bind(stripeSettings);
builder.Services.AddSingleton(stripeSettings);
StripeConfiguration.ApiKey = stripeSettings.SecretKey;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers(); // Added 
app.Run();
