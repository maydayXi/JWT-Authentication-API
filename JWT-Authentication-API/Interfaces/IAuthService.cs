using JWT_Authentication_API.Models;

namespace JWT_Authentication_API.Interfaces;

/// <summary>
/// 驗證服務介面
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// 驗證員工登入
    /// </summary>
    /// <param name="loginDto"> 員工登入資料 </param>
    /// <returns> 驗證結果 </returns>
    Task<bool> ValidateUserAsync(LoginDto loginDto);
}