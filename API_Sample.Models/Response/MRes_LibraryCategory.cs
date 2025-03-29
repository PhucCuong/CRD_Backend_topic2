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
    public class MRes_LibraryCategory:BaseModel.History
    {
        [Required]
        [Column("category_name")]
        [StringLength(255)]
        public string CategoryName { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
