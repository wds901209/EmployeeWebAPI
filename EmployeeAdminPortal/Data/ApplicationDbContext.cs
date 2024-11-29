using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore; //DbContext

namespace EmployeeAdminPortal.Data
{
    // 連接資料庫的工具，可以幫助處理資料的讀取和寫入
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }  // 代表資料庫中的 Employee table。
    }
}
