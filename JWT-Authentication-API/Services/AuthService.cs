using JWT_Authentication_API.Interfaces;
using JWT_Authentication_API.Models;
using Microsoft.AspNetCore.Identity;

namespace JWT_Authentication_API.Services;

/// <summary>
/// 驗證服務
/// </summary>
public class AuthService(IEmployeeService employeeService): IAuthService
{
    /// <summary>
    /// 員工資料存取服務
    /// </summary>
    private readonly IEmployeeService _employeeService = employeeService;
    
    /// <summary>
    /// 驗證員工登入密碼
    /// </summary>
    /// <param name="loginDto"> 員工登入資料 </param>
    /// <returns> 驗證結果 </returns>
    public async Task<bool> ValidateUserAsync(LoginDto loginDto)
    {
        if(string.IsNullOrEmpty(loginDto.Email) 
           || string.IsNullOrEmpty(loginDto.Password))
            throw new ArgumentException($"Invalid {nameof(loginDto.Email)} or {nameof(loginDto.Password)}!");
        
        // 取得員工資料進行驗證
        var employee = await _employeeService.GetEmployeeByEmailAsync(loginDto.Email);
        if (employee == null) throw new NullReferenceException("Employee not found!");
        
        // 回傳驗證結果
        return new PasswordHasher<RegisterDto>().VerifyHashedPassword(employee, 
                   employee.Password, loginDto.Password) 
               == PasswordVerificationResult.Success;
    }
}