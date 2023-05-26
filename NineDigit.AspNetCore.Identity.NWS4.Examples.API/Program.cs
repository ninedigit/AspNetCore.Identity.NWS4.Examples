using NineDigit.AspNetCore.Identity.NWS4;
using NineDigit.AspNetCore.Identity.NWS4.Examples.API.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(builder =>
    {
        builder.DefaultScheme = NWS4AuthenticationDefaults.AuthenticationScheme;
    })
    .AddNWS4Authentication<NWS4AuthenticationHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();