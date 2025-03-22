using JWT_Authentication_API.Helper;
using JWT_Authentication_API.Interfaces;
using JWT_Authentication_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Authentication_API.Controllers;

/// <summary>
/// 將 AuthController 宣告成為 ApiController
/// 並定義路由規則（網址）=> domain/api/auth 
/// </summary>
/// <param name="employeeService"> 員工資料存取服務 </param>
/// <param name="authService"> 登入驗證服務 </param>
/// <param name="jwtHelper"> JWT 輔助工具 </param>
[ApiController, Route("api/[controller]")]
public class AuthController(
    IEmployeeService employeeService, 
    IAuthService authService,
    JwtHelper jwtHelper) : Controller 
{
    /// <summary>
    /// 員工資料存取的服務
    /// </summary>
    private readonly IEmployeeService _employeeService = employeeService;
    /// <summary>
    /// 登入驗證的服務
    /// </summary>
    private readonly IAuthService _authService = authService;
    /// <summary>
    /// JWT 輔助工具，負責生成 JWT
    /// </summary>
    private readonly JwtHelper _jwtHelper = jwtHelper;
    
    #region 註冊
    /// <summary>
    /// 註冊 API
    /// </summary>
    /// <param name="registerDto"> 使用者傳送的員工註冊資料 </param>
    /// <returns> 註冊完成的員工資料 </returns>
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto registerDto)
    {
        // 驗證註冊資料
        if (string.IsNullOrEmpty(registerDto.Email) 
            || string.IsNullOrEmpty(registerDto.Password))
            return BadRequest("Please provide 'Email' and 'Password'");
        
        // 先查詢有沒有重複的員工資料
        var employee = await _employeeService.GetEmployeeByEmailAsync(registerDto.Email);
        
        // 如果沒有就註冊新員工
        var registerResult = 
            employee == null &&
            await _employeeService.AddEmployeeAsync(registerDto);
        
        // 回傳註冊結果
        return registerResult 
            ? Ok("Register successfully!") 
            : employee != null 
                ? BadRequest("User already exists!")
                : BadRequest("Failed to register user");
    }
    #endregion

    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="loginDto"> 使用者的輸入資料 </param>
    /// <returns> 登入結果 </returns>
    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginAsync(LoginDto loginDto)
    { 
        // 登入資料驗證
        if (string.IsNullOrEmpty(loginDto.Email) || 
            string.IsNullOrEmpty(loginDto.Password))
            return BadRequest("Please provide 'Email' and 'Password'");
        
        // 檢查員工帳號
        if (await _employeeService.GetEmployeeByEmailAsync(loginDto.Email) == null) 
            return BadRequest("User does not exist!");
        
        // 檢查員工密碼並回傳登入結果
        if (!await _authService.ValidateUserAsync(loginDto))
            return BadRequest("Login failed!");
        
        // 產生 Jwt 
        var jwt = _jwtHelper.CreateJwt(loginDto);
        
        return Ok(jwt);
    }
}