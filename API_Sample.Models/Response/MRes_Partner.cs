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
    public class MRes_Partner:BaseModel.History
    {
        [Required]
        [Column("parner_name")]
        [StringLength(200)]
        public string ParnerName { get; set; }

        [Column("abbreviation_name")]
        [StringLength(10)]
        public string AbbreviationName { get; set; }

        [Required]
        [Column("logo_image")]
        [StringLength(255)]
        public string LogoImage { get; set; }

        [Column("website")]
        [StringLength(500)]
        public string Website { get; set; }

        [Column("contact_email")]
        [StringLength(255)]
        public string ContactEmail { get; set; }

        [Column("phone")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Column("note")]
        [StringLength(500)]
        public string Note { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }
    }
}
