using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("Recruitment")]
public partial class Recruitment
{
    [Key]
    [Column("recruitment_id")]
    public int RecruitmentId { get; set; }

    [Required]
    [Column("recruitment_name")]
    [StringLength(255)]
    public string RecruitmentName { get; set; }

    [Column("business_id")]
    public int BusinessId { get; set; }

    [Required]
    [Column("recruitment_position")]
    [StringLength(100)]
    public string RecruitmentPosition { get; set; }

    [Required]
    [Column("content")]
    public string Content { get; set; }

    [Column("create_at", TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [Column("create_by")]
    [StringLength(30)]
    [Unicode(false)]
    public string CreateBy { get; set; }

    [Column("update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [Column("update_by")]
    [StringLength(30)]
    [Unicode(false)]
    public string UpdateBy { get; set; }

    [ForeignKey("BusinessId")]
    [InverseProperty("Recruitments")]
    public virtual Business Business { get; set; }

    [ForeignKey("CreateBy")]
    [InverseProperty("RecruitmentCreateByNavigations")]
    public virtual Account CreateByNavigation { get; set; }

    [InverseProperty("Recruitment")]
    public virtual ICollection<RecruitmentApplication> RecruitmentApplications { get; set; } = new List<RecruitmentApplication>();

    [ForeignKey("UpdateBy")]
    [InverseProperty("RecruitmentUpdateByNavigations")]
    public virtual Account UpdateByNavigation { get; set; }
}
