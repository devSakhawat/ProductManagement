using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL;
using ProductManagement.DAL.Constracts;
using ProductManagement.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the dbcontext.
builder.Services.AddDbContext<TonerContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DbLocation")));

// Add services to the container.
builder.Services.AddControllers();

// add service UnitOfWork
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
   options.AddDefaultPolicy(
      builder =>
      {
         builder.WithOrigins("*", "https://localhost:7058")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
