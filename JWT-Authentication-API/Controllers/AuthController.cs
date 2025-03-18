using JWT_Authentication_API.Entities;
using JWT_Authentication_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Authentication_API.Controllers;

/// <summary>
/// 將 AuthController 宣告成為 ApiController
/// 並定義路由規則（網址）=> domain/api/auth 
/// </summary>
[ApiController, Route("api/[controller]")]
public class AuthController : Controller
{
    /// <summary>
    /// 註冊 API
    /// </summary>
    /// <param name="registerDto"> 使用者傳送的員工註冊資料 </param>
    /// <returns> 註冊完成的員工資料 </returns>
    [HttpPost("register")]
    public ActionResult Register(RegisterDto registerDto)
    {
        // 驗證註冊資料
        if (string.IsNullOrEmpty(registerDto.Email) 
            || string.IsNullOrEmpty(registerDto.Password))
            return BadRequest("Please provide 'Email' and 'Password'");
        
        // 建立員工資料
        Employee employee = new() { Email = registerDto.Email };
        // 將密碼加密
        employee.PasswordHash = new PasswordHasher<Employee>()
            .HashPassword(employee, registerDto.Password);
        
        // 回傳建立完成的員工資料
        return Ok(employee);
    }
}