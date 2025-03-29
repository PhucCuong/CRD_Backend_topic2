using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("NewsView")]
public partial class NewsView
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("news_id")]
    public int NewsId { get; set; }

    [Column("view_count")]
    public int? ViewCount { get; set; }

    [ForeignKey("NewsId")]
    [InverseProperty("NewsViews")]
    public virtual News News { get; set; }
}
