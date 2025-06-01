using MovieBlazor.Clients;
using MovieBlazor.Components;

var builder = WebApplication.CreateBuilder(args); // Create a new instance of class WebApplication

// Add services to the container.
// Services are objects that are used to add functionality to the application.
// Such as NavigationManager, HttpClient, and so on.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var movieApiUrl = builder.Configuration["MovieApiUrl"] ?? throw new Exception("The MovieApiUrl is not set!"); // Get the MovieApiUrl from the configuration file
builder.Services.AddHttpClient<MoviesClient>(client => client.BaseAddress = new Uri(movieApiUrl)); // Add a new HttpClient service to the container with a base address of the MovieApiUrl
builder.Services.AddHttpClient<GenresClient>(client => client.BaseAddress = new Uri(movieApiUrl));

// RazorComponents are new feature in ASP.NET Core that allows to build web applications using C# and HTML.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files from wwwroot folder

app.UseAntiforgery(); // Protects the application from cross-site request forgery (CSRF) attacks

app.MapStaticAssets(); // MapStaticAssets is a method that maps static assets to the application
app.MapRazorComponents<App>().AddInteractiveServerRenderMode(); // MapRazorComponents is a method that maps Razor components to the application
// Enable server-side rendering of the components.
app.Run(); // Run the application
