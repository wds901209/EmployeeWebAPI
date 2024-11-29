using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models
{
    public class AddEmployeeDto
    {
        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")] 
        public required string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]  
        public required string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number format")] 
        public string? Phone { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }
    }
}
/*
為什麼要特別創一個 AddEmployeeDto class?

1.資料驗證/格式化： 
AddEmployeeDto 可以幫助我們確保從前端傳來的資料是正確的。
例如，當前端傳來員工的 Name 或 Email 時，我們可以設定這些欄位必須有值，並且格式必須正確等。

2.讓前後端解耦： 
前端和後端有時候需要處理不同的資料。
舉例來說，前端可能會有很多資料（像是員工的詳細資料），
但後端其實只需要最基本的資料（像是姓名、Email 和職位）。

3.簡化資料： 
假設我們有一個很複雜的 Employee 類別，裡面有員工的 ID、入職日期、薪水等欄位，
但其實新增員工時，前端只需要傳送姓名、Email 等少數幾個欄位。
AddEmployeeDto 就是幫助我們簡化這部分，讓後端專注於處理我們真正需要的資料。

4.保護資料： 
在資料庫中，Employee 類別裡包含了像密碼、員工 ID 等敏感資料，而我們不希望這些資料被前端看到或修改，
那麼 AddEmployeeDto 可以讓我們只暴露出需要的欄位（像是姓名和職位），從而保護這些隱私資料。
*/