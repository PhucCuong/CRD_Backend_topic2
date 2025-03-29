using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

public partial class SeoDatum
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

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

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }
}
