using Microsoft.EntityFrameworkCore;

namespace JWT_Authentication_API.Entities;

/// <summary>
/// Database Context
/// </summary>
/// <param name="options"></param>
public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    /// <summary>
    /// 員工資料表
    /// </summary>
    public DbSet<Employee> Employees { get; set; }
}