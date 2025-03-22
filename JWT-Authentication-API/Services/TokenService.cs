using JWT_Authentication_API.Entities;
using JWT_Authentication_API.Interfaces;

namespace JWT_Authentication_API.Services;
/// <summary>
/// Token 資料存取服務
/// </summary>
/// <param name="context"> 資料庫物件 </param>
public class TokenService(AppDbContext context): ITokenService
{
    /// <summary>
    /// 資料庫物件
    /// </summary>
    private readonly AppDbContext _appDb = context;
    
    /// <summary>
    /// 新增 Token 到黑名單
    /// </summary>
    /// <param name="token"> 要新增的 Token </param>
    /// <returns> 新增結果 </returns>
    public async Task<bool> AddTokenToTokenBlackListAsync(string token)
    {
        await _appDb.TokenBlackLists.AddAsync(new TokenBlackList { Token = token });
        var result = await _appDb.SaveChangesAsync();
        
        return result > 0;
    }
}