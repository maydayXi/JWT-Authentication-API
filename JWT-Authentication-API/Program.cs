using JWT_Authentication_API.Entities;
using JWT_Authentication_API.Helper;
using JWT_Authentication_API.Interfaces;
using JWT_Authentication_API.Options;
using JWT_Authentication_API.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Database service
// Add SQL Server Database Service
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(
        builder.Configuration.GetConnectionString("JwtAuthDB"),
        options =>
        {
            options.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );
            options.CommandTimeout(commandTimeout: 300);
        }
    );
});
#endregion

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

#region Swagger Service
// Add Swagger service
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
#endregion

#region CustomService
builder.Services
    .AddScoped<IEmployeeService, EmployeeService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<ITokenService, TokenService>()
    // 註冊這個 JwtOptions 的物件，並封裝成 IOptions 型別，讓其他類別可以注入使用
    .Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
// 註冊 JwtHelper
builder.Services.AddSingleton<JwtHelper>();
#endregion

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // See https://github.com/scalar/scalar/blob/main/integrations/aspnetcore/README.md#usage
    app.UseSwagger(options => 
        options.RouteTemplate = "swagger/{documentName}/swagger.json"
    );
    // 因為要使用 scalar 的 UI，所以這邊將 Swagger UI 關閉 
    // app.UseSwaggerUI();
    
    // See https://github.com/scalar/scalar/blob/main/documentation/integrations/dotnet.md#openapi-document-route
    app.MapScalarApiReference(options => 
        options.WithOpenApiRoutePattern("swagger/{documentName}/swagger.json"));
}

app.UseHttpsRedirection();
// 啟用控制器的 Route
app.MapControllers();
app.Run();
