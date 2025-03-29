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
    public class MRes_Contact:BaseModel.History
    {
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
