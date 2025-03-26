using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT_Authentication_API.Interfaces;
using JWT_Authentication_API.Models;
using JWT_Authentication_API.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWT_Authentication_API.Helper;

/// <summary>
/// JSON Web Token 輔助工具
/// </summary>
public class JwtHelper(IOptions<JwtOptions> jwtOptions)
{
    /// <summary>
    /// Jwt 的相關設定
    /// </summary>
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    /// <summary>
    /// 產生 JWT
    /// </summary>
    /// <param name="employee"> 員工的登入資訊 </param>
    /// <returns> JSON Web Token </returns>
    public string CreateJwt(EmployeeDto employee)
    {
        var now = DateTimeOffset.UtcNow;
        
        // 設定 Payload
        List<Claim> claims = [
            // 發行單位
            new(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
            // 使用者帳號作為識別
            new(JwtRegisteredClaimNames.Sub, employee.Email),
            // Token 的有效期限，從現在開始到 5 分鐘後
            new(JwtRegisteredClaimNames.Exp, $"{now.AddMinutes(_jwtOptions.Expiry).ToUnixTimeSeconds()}"),
            // 這個 JWT 的識別
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // 這個 JWT 的發行時間
            new(JwtRegisteredClaimNames.Iat, $"{now.ToUnixTimeSeconds()}"),
            // 員工角色
            new(ClaimTypes.Role, $"{employee.EmployeeRole}", ClaimValueTypes.Integer)
        ];
        // 產生使用者身分證明
        ClaimsIdentity userClaimsIdentity = new(claims);
        // 產生私鑰供後續加密使用
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        // 產生數位簽章憑證，使用 SHA256 加密演算
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        
        // 產生 JWT
        JwtSecurityToken securityToken = new(
            issuer: _jwtOptions.Issuer,             // issuer
            claims: userClaimsIdentity.Claims,      // payload 
            signingCredentials: credentials,        // signature
            expires: now.AddMinutes(_jwtOptions.Expiry).UtcDateTime);  // expiry time
        
        // 輸出 JWT 並轉換成字串
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}