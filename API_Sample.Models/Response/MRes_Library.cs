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
    public class MRes_Library:BaseModel.History
    {
        [Required]
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }

        [Column("libraryy_description")]
        [StringLength(500)]
        public string LibraryyDescription { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }
    }
}
