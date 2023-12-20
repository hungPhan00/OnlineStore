using Microsoft.EntityFrameworkCore;
using OnlineStore.cms.Mapper;
using OnlineStore.cms.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OnlineStore.DataAccess.Data.OnlineStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineStoreContextString") ?? throw new InvalidOperationException("Connection string 'OnlineStoreContext' not found.")));
builder.Services.RegisterService();
builder.Services.AddAutoMapper(typeof(MapperDTOViewModel).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();