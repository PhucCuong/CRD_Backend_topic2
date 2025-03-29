using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("EmployeeProfile")]
[Index("EmployeeCode", Name = "UQ__Employee__B0AA73455F4389BC", IsUnique = true)]
public partial class EmployeeProfile
{
    [Key]
    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Required]
    [Column("full_name")]
    [StringLength(100)]
    public string FullName { get; set; }

    [Column("date_of_birth", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("home_address")]
    [StringLength(255)]
    public string HomeAddress { get; set; }

    [Required]
    [Column("education_level")]
    [StringLength(255)]
    public string EducationLevel { get; set; }

    [Column("employee_code")]
    [StringLength(255)]
    public string EmployeeCode { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
