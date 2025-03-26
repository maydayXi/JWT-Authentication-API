using System.Text;
using JWT_Authentication_API.Entities;
using JWT_Authentication_API.Helper;
using JWT_Authentication_API.Interfaces;
using JWT_Authentication_API.Options;
using JWT_Authentication_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

#region Authentication Service
builder.Services
    // 啟用 JWT 驗證 
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Jwt 驗證選項
    .AddJwtBearer(options =>
    {
        // 不使用 Dependency Injection 方式讀取設定檔
        var jwtOptions = builder.Configuration.GetSection(
            nameof(JwtOptions)).Get<JwtOptions>()!;
        // 要加入這個設定，不然 Sub 會變成 ClaimTypes.NameIdentifier
        options.MapInboundClaims = false;
        // 回應 JWT 詳細錯誤訊息，方便 Debug，所以只有在非正式環境開啟
        options.IncludeErrorDetails = true;
        // JWT 驗證規則
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // 驗證發行單位
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            
            // 不驗證接收單位，不需要瞼證要改成 false, 原始預設是 true
            ValidateAudience = false,
            
            // 驗證有效期限
            ValidateLifetime = true,
            
            // 驗證金鑰
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });
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
// 啟用驗證機制
app.UseAuthentication().UseAuthorization();
app.Run();
