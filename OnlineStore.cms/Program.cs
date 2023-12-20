using Microsoft.EntityFrameworkCore;
using OnlineStore.cms.Extentions;
using OnlineStore.cms.Mapper;
using OnlineStore.DataAccess.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200",
                                              "http://localhost:4200/landingpage")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod();
                      });
});

builder.Services.AddDbContext<OnlineStore.DataAccess.Data.OnlineStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineStoreContextString") ?? throw new InvalidOperationException("Connection string 'OnlineStoreContext' not found.")));
builder.Services.RegisterService();
builder.Services.AddAutoMapper(typeof(MapperDTOViewModel).Assembly);
var app = builder.Build();
//seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
