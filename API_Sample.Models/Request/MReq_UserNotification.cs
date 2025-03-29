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
    public class MReq_UserNotification:BaseModel.History
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [Column("title")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [Column("content")]
        [StringLength(255)]
        public string Content { get; set; }

        [Column("notification_id")]
        public int NotificationId { get; set; }

        [Column("is_read")]
        public bool IsRead { get; set; }
    }
}
