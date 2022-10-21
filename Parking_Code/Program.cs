using Parking_Code;
using Parking_Code.DataSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ParkingDatabase"));

builder.Services.AddScoped<PriceSettings, PriceSettings>();
builder.Services.AddScoped<ArrivalTicketSettings, ArrivalTicketSettings>();
builder.Services.AddScoped<DepartureTicketSettings, DepartureTicketSettings>();
builder.Services.AddScoped<VehicleSettings, VehicleSettings>();

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
