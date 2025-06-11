using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Data;
using tmsminimalapi.DTOs;
using tmsminimalapi.Extensions;
using tmsminimalapi.Models;
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

// Booking endpoints
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

app.MapGet("/api/bookings/status/{status}", async (IBookingService bookingService, BookingStatus status) =>
{
    var bookings = await bookingService.GetBookingsByStatusAsync(status);
    return Results.Ok(bookings);
})
.WithName("GetBookingsByStatus")
.WithOpenApi();

// Booking assignment endpoints
app.MapPost("/api/bookings/assign", async (IBookingAssignmentService assignmentService, BookingAssignmentRequestDTO request) =>
{
    try
    {
        var result = await assignmentService.AssignTrucksToBookingAsync(request);
        return Results.Created($"/api/bookings/{request.BookingId}/assignments", result);
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("AssignTrucksToBooking")
.WithOpenApi();

app.MapGet("/api/bookings/{bookingId}/assignments", async (IBookingAssignmentService assignmentService, Guid bookingId) =>
{
    try
    {
        var assignments = await assignmentService.GetBookingAssignmentsAsync(bookingId);
        return Results.Ok(assignments);
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
})
.WithName("GetBookingAssignments")
.WithOpenApi();

app.Run();
