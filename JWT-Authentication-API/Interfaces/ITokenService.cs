namespace JWT_Authentication_API.Interfaces;

/// <summary>
/// Token 相關服務的介面
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// 新增 token 到黑名單
    /// </summary>
    /// <param name="token"> 要新增的 token </param>
    /// <returns> 新增結果 </returns>
    Task<bool> AddTokenToTokenBlackListAsync(string token);
}