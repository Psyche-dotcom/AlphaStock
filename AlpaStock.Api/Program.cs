using AlpaStock.Api.Extension;
using AlpaStock.Core.Seeder;
using AlpaStock.Infrastructure.SignalRHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLibrary(builder.Configuration);
builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
Seeder.SeedData(app).Wait();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); 
    endpoints.MapHub<ChatHub>("/chatHub");
});


app.Run();
