using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Index("CategoryName", Name = "UQ__PartnerC__5189E255BB319C98", IsUnique = true)]
public partial class PartnerCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("category_name")]
    [StringLength(255)]
    public string CategoryName { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
