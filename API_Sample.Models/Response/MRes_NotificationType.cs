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
    public class MRes_NotificationType:BaseModel.History
    {
        [Required]
        [Column("notification_type")]
        [StringLength(50)]
        public string NotificationType1 { get; set; }
    }
}
