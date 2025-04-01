using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Index("NameSlug", Name = "UQ_News", IsUnique = true)]
public partial class News
{
    [Key]
    [Column("news_id")]
    public int NewsId { get; set; }

    [Column("news_category_id")]
    public int NewsCategoryId { get; set; }

    [Required]
    [Column("news_name")]
    [StringLength(255)]
    public string NewsName { get; set; }

    [Required]
    [Column("short_description")]
    [StringLength(255)]
    public string ShortDescription { get; set; }

    [Required]
    [Column("content")]
    public string Content { get; set; }

    [Column("create_at", TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [Column("create_by")]
    [StringLength(50)]
    public string CreateBy { get; set; }

    [Column("update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [Column("update_by")]
    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column("status")]
    public bool Status { get; set; }

    [Column("name_slug")]
    [StringLength(255)]
    public string NameSlug { get; set; }

    [ForeignKey("NewsCategoryId")]
    [InverseProperty("News")]
    public virtual NewsCategory NewsCategory { get; set; }

    [InverseProperty("News")]
    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();

    [InverseProperty("News")]
    public virtual ICollection<NewsImage> NewsImages { get; set; } = new List<NewsImage>();

    [InverseProperty("News")]
    public virtual ICollection<NewsLike> NewsLikes { get; set; } = new List<NewsLike>();

    [InverseProperty("News")]
    public virtual ICollection<NewsShare> NewsShares { get; set; } = new List<NewsShare>();

    [InverseProperty("News")]
    public virtual ICollection<NewsView> NewsViews { get; set; } = new List<NewsView>();
}
