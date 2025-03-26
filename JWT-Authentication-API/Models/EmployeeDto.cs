namespace JWT_Authentication_API.Models;

/// <summary>
/// 員工角色資料
/// </summary>
public class EmployeeDto
{
    /// <summary>
    /// 員工帳號
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// 員工密碼（加密）
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// 員工角色
    /// </summary>
    public int EmployeeRole { get; set; } = -1;
}