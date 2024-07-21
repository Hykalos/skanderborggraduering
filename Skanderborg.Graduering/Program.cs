var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAutoMapper(a => a.AllowNullCollections = true, Assembly.GetAssembly(typeof(Program)));

builder.Services.AddSingleton<MentoClubConfiguration>((serviceProvider) => new MentoClubConfiguration
{
    BaseUrl = builder.Configuration.GetValue<string>("MentoClubUrl") ?? throw new KeyNotFoundException("MentoClubUrl was not set in the app settings")
});
builder.Services.AddSingleton<ICsvReader, CsvReader>();
builder.Services.AddSingleton<IPdfGenerator, PdfGenerator>();
builder.Services.AddTransient<IMemberSystemService, MentoClubService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
