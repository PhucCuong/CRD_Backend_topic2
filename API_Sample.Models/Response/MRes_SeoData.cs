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
    public class MRes_SeoData:BaseModel.History
    {
        [Required]
        [Column("entity_type")]
        [StringLength(50)]
        public string EntityType { get; set; }

        [Column("entityid")]
        public int Entityid { get; set; }

        [Column("meta_title")]
        [StringLength(255)]
        public string MetaTitle { get; set; }

        [Column("meta_description")]
        [StringLength(500)]
        public string MetaDescription { get; set; }

        [Column("meta_keywords")]
        [StringLength(255)]
        public string MetaKeywords { get; set; }

        [Column("canonical_url")]
        [StringLength(500)]
        public string CanonicalUrl { get; set; }
    }
}
