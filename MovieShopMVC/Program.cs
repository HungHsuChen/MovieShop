using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieShopMVC.Helpers;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    // Add services to the container.
    // Configure Services
    // Registering your Services/dependcies
    builder.Services.AddControllersWithViews();

    // Services Injection
    builder.Services.AddScoped<IMovieService, MovieService>();
    builder.Services.AddScoped<IGenreService, GenreService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ICastService, CastService>();

    // Repositories Injection
    builder.Services.AddScoped<IMovieRepository, MovieRepository>();
    builder.Services.AddScoped<ICastRepository, CastRepository>();
    builder.Services.AddScoped<IGenreRepository, GenreRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
    builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
    builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

    // inject the connection string into the MovieShopDbContext constructor using DbContextOptions
    builder.Services.AddDbContext<MovieShopDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"))
        );

    // DI for caching
    //builder.Services.AddMemoryCache();

    // tell our asp.net what kind of authentication we are using
    // cookie based authentication
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "MovieShopAuthCookie";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.LoginPath = "/account/login";
        });

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        //app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseMovieShopExceptionMiddleware();

        app.UseHsts();
    }
    else
    {
        app.UseMovieShopExceptionMiddleware();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

