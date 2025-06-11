using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Data;
using tmsminimalapi.DTOs;
using tmsminimalapi.Extensions;
using tmsminimalapi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<TmsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Add application services
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Party endpoints
app.MapPost("/api/parties", async (IPartyService partyService, PartyCreateDTO partyDto) =>
{
    var result = await partyService.CreatePartyAsync(partyDto);
    return Results.Created($"/api/parties/{result.Id}", result);
})
.WithName("CreateParty")
.WithOpenApi();

app.MapGet("/api/parties/search", async (IPartyService partyService, string query) =>
{
    var parties = await partyService.SearchPartiesAsync(query);
    return Results.Ok(parties);
})
.WithName("SearchParties")
.WithOpenApi();

// Booking endpoint
app.MapPost("/api/bookings", async (IBookingService bookingService, BookingCreateDTO bookingDto) =>
{
    try
    {
        var result = await bookingService.CreateBookingAsync(bookingDto);
        return Results.Created($"/api/bookings/{result.Id}", result);
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
})
.WithName("CreateBooking")
.WithOpenApi();

app.Run();
