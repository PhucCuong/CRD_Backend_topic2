using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("PostImage")]
public partial class PostImage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("post_id")]
    public int PostId { get; set; }

    [Column("image_url")]
    [StringLength(255)]
    public string ImageUrl { get; set; }

    [Column("is_avatar")]
    public bool? IsAvatar { get; set; }

    [Column("create_at", TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("PostImages")]
    public virtual Post Post { get; set; }
}
