using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("Permission")]
public partial class Permission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Required]
    [Column("action_name")]
    [StringLength(20)]
    public string ActionName { get; set; }

    [Column("allow_view")]
    public bool AllowView { get; set; }

    [Column("allow_create")]
    public bool AllowCreate { get; set; }

    [Column("allow_edit")]
    public bool AllowEdit { get; set; }

    [Column("alow_delete")]
    public bool AlowDelete { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Permissions")]
    public virtual UserRole Role { get; set; }
}
