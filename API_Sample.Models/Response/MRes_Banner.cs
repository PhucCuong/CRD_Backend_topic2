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
    public class MRes_Banner:BaseModel.History
    {
        [Required]
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [Column("image_url")]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Column("link")]
        [StringLength(500)]
        public string Link { get; set; }

        [Required]
        [Column("position")]
        [StringLength(100)]
        public string Position { get; set; }
    }
}
