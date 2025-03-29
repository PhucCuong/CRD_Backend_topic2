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

    [InverseProperty("NewsCategory")]
    public virtual ICollection<News> News { get; set; } = new List<News>();
}
