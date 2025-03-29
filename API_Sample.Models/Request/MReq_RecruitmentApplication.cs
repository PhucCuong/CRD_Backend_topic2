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
    public class MReq_RecruitmentApplication:BaseModel.History
    {
        [Key]
        [Column("application_id")]
        public int ApplicationId { get; set; }

        [Column("recruitment_id")]
        public int RecruitmentId { get; set; }

        [Required]
        [Column("full_name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [Column("education_level")]
        [StringLength(255)]
        public string EducationLevel { get; set; }

        [Required]
        [Column("professional_qualifications")]
        [StringLength(255)]
        public string ProfessionalQualifications { get; set; }

        [Column("cv_url")]
        [StringLength(255)]
        public string CvUrl { get; set; }
    }
}
