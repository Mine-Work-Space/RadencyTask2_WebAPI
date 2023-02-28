using Task2.BLL.Implementations;
using Task2.DAL;
using Task2.PL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy",
    builder =>
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Use build-in logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Seed data to in memory db
ContextSeed.Seed(new ApiContext());
// Add services
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<RateRepository>();
builder.Services.AddScoped<ReviewRepository>();


var app = builder.Build();
app.UseCors("CorsPolicy");
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
