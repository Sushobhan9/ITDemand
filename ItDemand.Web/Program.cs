using ItDemand.Domain.DataContext;
using ItDemand.Web.Configuration;
using ItDemand.Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services
	.AddControllersWithViews()
	.AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider()));

builder.Services.AddMemoryCache(); // cache for claims that are loaded from the db
builder.Services.AddDbContext<ItDemandContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ItDemand")));
builder.Services.AddScoped<ApplicationLog>();
builder.Services.AddAutoMapper(typeof(ItDemand.Web.ViewModels.AutoMapper).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseMiddleware<AuthMiddleware>();
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
	var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

	if (scopedFactory != null)
	{
		using var scope = scopedFactory.CreateScope();
		var service = scope.ServiceProvider.GetService<ItDemandContext>();
		service?.Seed();
	}
}

app.Run();
