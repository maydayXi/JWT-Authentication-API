namespace JWT_Authentication_API.Models;

/// <summary>
/// 註冊使用的資料模型
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// 使用者帳號
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// 使用者密碼（未加密）
    /// </summary>
    public string Password { get; set; } = string.Empty;
}