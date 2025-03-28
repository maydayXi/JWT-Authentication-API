using JWT_Authentication_API.Models;

namespace JWT_Authentication_API.Interfaces;

/// <summary>
/// 員工資料存取介面
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// 新增員工資料
    /// </summary>
    /// <param name="registerDto"> 員工註冊資料 </param>
    /// <returns> 註冊結果 </returns>
    Task<bool> AddEmployeeAsync(RegisterDto registerDto);
    
    /// <summary>
    /// 依帳號取得員工資料
    /// </summary>
    /// <param name="email"> 員工帳號 </param>
    /// <returns> 員山資料，如果使用者不存在就 null </returns>
    Task<EmployeeDto?> GetEmployeeByEmailAsync(string email);
    
    /// <summary>
    /// 取得所有員工資料 
    /// </summary>
    /// <returns> 所有員工資料 </returns>
    Task<IEnumerable<EmployeeDto>?> GetEmployeesAsync();
}