using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("PostTag")]
public partial class PostTag
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("post_id")]
    public int PostId { get; set; }

    [Required]
    [Column("tag_name")]
    [StringLength(50)]
    public string TagName { get; set; }

    [Column("create_at", TypeName = "datetime")]
    public DateTime CreateAt { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("PostTags")]
    public virtual Post Post { get; set; }
}
