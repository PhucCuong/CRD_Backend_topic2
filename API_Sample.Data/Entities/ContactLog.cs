using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("ContactLog")]
public partial class ContactLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("contact_id")]
    public int ContactId { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [Column("contactLog_action")]
    [StringLength(255)]
    public string ContactLogAction { get; set; }

    [Column("notes")]
    [StringLength(1000)]
    public string Notes { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("ContactId")]
    [InverseProperty("ContactLogs")]
    public virtual Contact Contact { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("ContactLogs")]
    public virtual Account UsernameNavigation { get; set; }
}
