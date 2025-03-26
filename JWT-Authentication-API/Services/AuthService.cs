using JWT_Authentication_API.Entities;
using JWT_Authentication_API.Interfaces;
using JWT_Authentication_API.Models;
using Microsoft.AspNetCore.Identity;

namespace JWT_Authentication_API.Services;

/// <summary>
/// 驗證服務
/// </summary>
public class AuthService: IAuthService
{
    /// <summary>
    /// 驗證員工登入密碼
    /// </summary>
    /// <param name="loginDto"> 員工登入資料 </param>
    /// <param name="employeeDto"> 員工資料 </param>
    /// <returns> 驗證結果 </returns>
    public bool ValidateUserAsync(LoginDto loginDto, EmployeeDto employeeDto)
    {
        if(string.IsNullOrEmpty(loginDto.Email) 
           || string.IsNullOrEmpty(loginDto.Password))
            throw new ArgumentException($"Invalid {nameof(loginDto.Email)} or {nameof(loginDto.Password)}!");
        
        // 驗證員工資料
        if (employeeDto == null) throw new NullReferenceException("Employee not found!");
        
        // 回傳驗證結果
        return new PasswordHasher<EmployeeDto>().VerifyHashedPassword(
           employeeDto, employeeDto.PasswordHash, loginDto.Password
        ) == PasswordVerificationResult.Success;
    }
}