using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("NewsCategory")]
public partial class NewsCategory
{
    [Key]
    [Column("news_category_id")]
    public int NewsCategoryId { get; set; }

    [Required]
    [Column("news_category_name")]
    [StringLength(255)]
    public string NewsCategoryName { get; set; }

    [Column("status")]
    public bool Status { get; set; }

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

    [InverseProperty("NewsCategory")]
    public virtual ICollection<News> News { get; set; } = new List<News>();
}
