using System.ComponentModel.DataAnnotations;
using JWT_Authentication_API.Entities;
using JWT_Authentication_API.Interfaces;
using JWT_Authentication_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWT_Authentication_API.Services;

/// <summary>
/// 員工資料存取服務
/// </summary>
public class EmployeeService(AppDbContext dbContext) : IEmployeeService
{
    private readonly AppDbContext _appDb = dbContext;

    /// <summary>
    /// 新增員工資料
    /// </summary>
    /// <param name="registerDto"> 員工註冊資料 </param>
    /// <returns> 註冊結果 </returns>
    public async Task<bool> AddEmployeeAsync(RegisterDto registerDto)
    {
        if (string.IsNullOrEmpty(registerDto.Email)
            || string.IsNullOrEmpty(registerDto.Password))
            throw new ValidationException("Email or password is required");
        
        // Create new employee data.
        Employee employee = new() { Email = registerDto.Email };
        // Hash password
        employee.PasswordHash = new PasswordHasher<Employee>()
            .HashPassword(employee, registerDto.Password);
        // Insert into database
        await _appDb.Employees.AddAsync(employee);
        // Save changes
        var result = await _appDb.SaveChangesAsync();
        
        return result > 0;
    }

    /// <summary>
    /// 依帳號取得員工資料
    /// </summary>
    /// <param name="email"> 登入信箱/註冊信箱 </param>
    /// <returns> 員工資料 或 null </returns>
    public async Task<RegisterDto?> GetEmployeeByEmailAsync(string email)
    {
        var employee = await _appDb.Employees
            .FirstOrDefaultAsync(e => e.Email == email);
        
        return employee == null 
            ? null 
            : new RegisterDto
            {
                Email = employee.Email, 
                Password = employee.PasswordHash
            };
    }
}