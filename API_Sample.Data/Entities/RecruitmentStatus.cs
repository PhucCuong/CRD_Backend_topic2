using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("RecruitmentStatus")]
public partial class RecruitmentStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("application_id")]
    public int ApplicationId { get; set; }

    [Required]
    [Column("application_status")]
    [StringLength(20)]
    [Unicode(false)]
    public string ApplicationStatus { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("note")]
    [StringLength(255)]
    public string Note { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("RecruitmentStatuses")]
    public virtual RecruitmentApplication Application { get; set; }
}
