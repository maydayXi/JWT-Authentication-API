namespace JWT_Authentication_API.Options;

/// <summary>
/// Jwt 的設定物件
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// 有效期限（分），預設 10 分鐘
    /// </summary>
    public int Expiry { get; set; } = 10;

    /// <summary>
    /// 發行單位
    /// </summary>
    public string Issuer { get; set; } = "JWT_Authentication_API";
    
    /// <summary>
    /// 加密金鑰
    /// </summary>
    public string SecretKey { get; set; } = Guid.NewGuid().ToString();
}