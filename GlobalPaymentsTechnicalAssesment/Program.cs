using GlobalPaymentsTechnicalAssesment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IQueueService, QueueService>();
builder.Services.AddSingleton<IElevatorService, ElevatorService>();
builder.Services.AddHostedService<ElevatorSimulationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ElevatorSimulator}/{action=Index}/{id?}");

app.Run();
