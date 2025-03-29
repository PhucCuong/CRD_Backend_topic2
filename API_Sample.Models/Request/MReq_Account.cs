using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Request
{
    public class MReq_Account : BaseModel.History
    {
        [Key]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [Column("pass_word")]
        [StringLength(30)]
        public string PassWord { get; set; }

        [Column("avatar_url")]
        [StringLength(255)]
        public string AvatarUrl { get; set; }

        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
