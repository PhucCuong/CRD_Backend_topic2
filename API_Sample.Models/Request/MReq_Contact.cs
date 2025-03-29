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
    public class MReq_Contactt : BaseModel.History
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("full_name")]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("phone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [Column("contact_message")]
        [StringLength(1000)]
        public string ContactMessage { get; set; }

        [Column("contact_status")]
        [StringLength(50)]
        public string ContactStatus { get; set; }
    }
}
