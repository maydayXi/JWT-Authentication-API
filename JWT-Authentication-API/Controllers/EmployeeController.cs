using JWT_Authentication_API.ActionFilter;
using JWT_Authentication_API.Enums;
using JWT_Authentication_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Authentication_API.Controllers;
/// <summary>
/// 員工資料存取
/// </summary>
/// <param name="employeeService"> 員工資料服務 </param>
[ApiController, Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : Controller
{
    /// <summary>
    /// 員工資料服務
    /// </summary>
    private readonly IEmployeeService _employeeService = employeeService;

    /// <summary>
    /// 取得單一員工資料
    /// </summary>
    /// <returns> 指定的員工資料 </returns>
    [RolePermission(UserRole.Intern, UserRole.HrAssistant, UserRole.HrHead)]
    [HttpPost("get/employee")]
    public async Task<ActionResult> GetEmployee()
    {
        var userRole = (int?)HttpContext.Items[nameof(UserRole)];
        var email = (string?)HttpContext.Items["Email"];

        if (userRole == null || string.IsNullOrEmpty(email))
            return BadRequest($"{nameof(userRole)} or {nameof(email)} are required.");
        // 取得員工資料
        var employee = await _employeeService.GetEmployeeByEmailAsync(email);
        if(employee == null)
            return NotFound($"{email} not found.");
        
        // 密碼不應該回傳
        employee.PasswordHash = string.Empty;
        return Ok(employee);
    }
    
    /// <summary>
    /// 取得所有員工資料
    /// </summary>
    /// <returns></returns>
    [RolePermission(UserRole.HrHead)]
    [HttpPost("get/employees")]
    public async Task<IActionResult> Employees()
    {
        var employees = await _employeeService.GetEmployeesAsync();
        if(employees == null) return NotFound();
        
        return Ok(employees.ToList());
    }
}