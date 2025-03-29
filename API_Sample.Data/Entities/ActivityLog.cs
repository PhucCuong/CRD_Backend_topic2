using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("ActivityLog")]
public partial class ActivityLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [Required]
    [Column("description_log")]
    [StringLength(255)]
    public string DescriptionLog { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("ActivityLogs")]
    public virtual Account UsernameNavigation { get; set; }
}
