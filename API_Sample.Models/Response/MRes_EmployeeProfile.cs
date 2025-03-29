using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Response
{
    public class MRes_EmployeeProfile:BaseModel.History
    {
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
    }
}
