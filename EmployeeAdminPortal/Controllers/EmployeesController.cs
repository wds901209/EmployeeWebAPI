using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // 網址會像這樣: localhost:xxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; //Dependency Injection(DI)  建構函式注入（Constructor Injection）
        }

        [HttpGet] // 用來查詢資料(R)
        public IActionResult GetAllEmployee()
        {
            //var allEmployees =  dbContext.Employees.ToList();
            return Ok(dbContext.Employees.ToList());    // HTTP 200
        }


        [HttpGet] //R  只想抓取id
        [Route("{id:Guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost] // 用來新增資料(C)
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            // 驗證模型
            if (!ModelState.IsValid)
            {
                // 返回 400 BadRequest，並包含模型驗證錯誤訊息
                return BadRequest(ModelState);
            }

            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            await dbContext.Employees.AddAsync(employeeEntity);
            await dbContext.SaveChangesAsync();    

            return Ok(employeeEntity);
        }

        [HttpPut] // 用來更新資料(U)
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // 若模型無效，返回錯誤訊息
            }

            var employee = await dbContext.Employees.FindAsync(id);
        
            if(employee == null)
            {
                return NotFound(id);
            }
            // 是因為 .Find(id) 會回傳一個Employee table中的實例 才可以用 employee.Name
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            await dbContext.SaveChangesAsync();    // 在swagger上輸入完後幫我轉換成SQL語言
            return Ok(employee);
        }

        [HttpDelete] // 用來刪除資料(D)
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
/*
為什麼用 IActionResult？
它的彈性很高，因為它可以回傳：

成功回應，例如：
Ok()（HTTP 200，表示成功）。
Created()（HTTP 201，表示已成功建立資源）。

錯誤回應，例如：
BadRequest()（HTTP 400，表示請求有誤）。
NotFound()（HTTP 404，表示資源不存在）。
自訂回應，你也可以用 Content() 或 Json() 自訂回應的內容格式。
*/

/*
Dependency Injection(DI)?
https://www.freecodecamp.org/news/a-quick-intro-to-dependency-injection-what-it-is-and-when-to-use-it-7578c84fa88f/
*/