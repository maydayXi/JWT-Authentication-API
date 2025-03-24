using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT_Authentication_API.Entities;

/// <summary>
/// JWT 的黑名單
/// </summary>
public class TokenBlackList
{
    /// <summary>
    /// 資料識別（PK）
    /// </summary>
    [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    /// <summary>
    /// 使用過的 Token
    /// </summary>
    [Required, MaxLength(2048)]
    public string Token { get; set; } = string.Empty;
    /// <summary>
    /// 資料新增的時間，也是 Token 過期的時間
    /// </summary>
    public DateTimeOffset CreateTime { get; init; } = DateTimeOffset.Now;
}