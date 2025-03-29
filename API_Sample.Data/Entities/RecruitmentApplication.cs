using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("RecruitmentApplication")]
public partial class RecruitmentApplication
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

    [ForeignKey("RecruitmentId")]
    [InverseProperty("RecruitmentApplications")]
    public virtual Recruitment Recruitment { get; set; }

    [InverseProperty("Application")]
    public virtual ICollection<RecruitmentStatus> RecruitmentStatuses { get; set; } = new List<RecruitmentStatus>();
}
