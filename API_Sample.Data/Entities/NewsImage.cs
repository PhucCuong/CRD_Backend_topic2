using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("NewsImage")]
public partial class NewsImage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("news_id")]
    public int NewsId { get; set; }

    [Column("image_url")]
    [StringLength(255)]
    public string ImageUrl { get; set; }

    [Column("is_avatar")]
    public bool? IsAvatar { get; set; }

    [Column("create_at", TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [ForeignKey("NewsId")]
    [InverseProperty("NewsImages")]
    public virtual News News { get; set; }
}
