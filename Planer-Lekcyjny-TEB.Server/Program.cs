using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Planer_Lekcyjny_TEB.Server.Dataa;
using Planer_Lekcyjny_TEB.Server.Models;
using Planer_Lekcyjny_TEB.Server.Services;
using System.Text;
using Planer_Lekcyjny_TEB.Server.CustomIdentityErrorDescriber;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<LessonService>();

// Register the CodePagesEncodingProvider to enable the use of Windows-1250 encoding for Polish characters
// IN XML File
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Add services to the container.
builder.Services.AddControllers().AddXmlSerializerFormatters();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Dostosowanie walidacji has³a
builder.Services.AddIdentityCore<User>(options =>
    {
        // Password settings.
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 0;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddErrorDescriber<
        CustomIdentityErrorDescriber>(); // Dodaj w³asny opis b³êdów

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder => builder.WithOrigins("https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
            { Title = "Planer-Lekcyjny-TEB.Server", Version = "v1" });

    // Filtr ukrywaj¹cy endpointy niezwi¹zane z SecureWebsite i Card
    options.DocInclusionPredicate((_, apiDesc) =>
    {
        // Zwróæ true tylko dla endpointów z kontrolera SecureWebsite i Card
        return apiDesc.ActionDescriptor.RouteValues.ContainsKey("controller") &&
            (apiDesc.ActionDescriptor.RouteValues["controller"] ==
                "SecureWebsite" ||
                apiDesc.ActionDescriptor.RouteValues["controller"] == "Card");
    });
});

WebApplication? app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // lub isProduction
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseCors("AllowMyOrigin"); // Use CORS policy

app.UseAuthorization();
app.MapIdentityApi<User>();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
