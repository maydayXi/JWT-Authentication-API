using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JWT_Authentication_API.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT_Authentication_API.ActionFilter;
/// <summary>
/// 角色驗證的邏輯
/// </summary>
/// <param name="userRoles"> 允許的角色名單 </param>
public class RolePermission(params UserRole[] userRoles): Attribute, IAuthorizationFilter
{
    /// <summary>
    /// 角色清單
    /// </summary>
    private readonly UserRole[] _userRoles = userRoles;
    
    /// <summary>
    /// 實際角色驗證的邏輯
    /// </summary>
    /// <param name="context"> 驗證場景，包含 request 的相關資訊 </param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // 從 JWT 中取得使用者角色
        var userRole = context.HttpContext.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .FirstOrDefault();
        
        // 取得失敗
        if (string.IsNullOrEmpty(userRole))
            // Http 403
            context.Result = new ForbidResult();
        // 取出來的角色是 string，為避免轉換失敗，用 TryParse 轉換成 int
        // TryParse 會回傳轉換的結果，如果無法轉換會 回傳 fasle
        // 如果可以轉換，會將轉換的值寫到 out 指定的變數
        if (int.TryParse(userRole, out var role))
        {
            // 如果不在允許的角色清單中
            if (_userRoles.All(u => (int)u != role))
            {
                // Http 403
                context.Result = new ForbidResult();
            }
            else
            {
                // 在行 JWT 中取得角色帳號
                var email = context.HttpContext.User.Claims
                    .Where(c => c.Type == JwtRegisteredClaimNames.Sub)
                    .Select(c => c.Value)
                    .FirstOrDefault();
                
                // 如果沒有取得使用帳號，回傳 Http 403
                if(string.IsNullOrEmpty(email))
                    context.Result = new ForbidResult();
                
                // 記住現在登入者的角色及帳號
                context.HttpContext.Items.Add(nameof(UserRole), role);
                context.HttpContext.Items.Add("Email", email);
            }
        }
        else
        {
            // 轉換失敗，Http 403
            context.Result = new ForbidResult();
        }
    }
}