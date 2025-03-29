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
    public class MRes_Account:BaseModel.History
    {
        [Key]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Column("avatar_url")]
        [StringLength(255)]
        public string AvatarUrl { get; set; }

        public string Token { get; set; }
    }
}
