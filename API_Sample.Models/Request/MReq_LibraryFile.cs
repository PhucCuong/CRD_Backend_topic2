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
    public class MReq_LibraryFile:BaseModel.History
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("library_id")]
        public int LibraryId { get; set; }

        [Required]
        [Column("name_file")]
        [StringLength(255)]
        public string NameFile { get; set; }

        [Column("file_type")]
        [StringLength(50)]
        public string FileType { get; set; }

        [Required]
        [Column("file_url")]
        [StringLength(500)]
        public string FileUrl { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
    }
}
