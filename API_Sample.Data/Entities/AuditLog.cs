using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

public partial class AuditLog
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
    [Column("action")]
    [StringLength(255)]
    public string Action { get; set; }

    [Column("entity_type")]
    [StringLength(50)]
    public string EntityType { get; set; }

    [Column("entityid")]
    public int? Entityid { get; set; }

    [Column("ip_address")]
    [StringLength(50)]
    public string IpAddress { get; set; }

    [Column("user_agent")]
    [StringLength(500)]
    public string UserAgent { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("AuditLogs")]
    public virtual Account UsernameNavigation { get; set; }
}
