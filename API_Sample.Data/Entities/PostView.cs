using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("PostView")]
public partial class PostView
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("post_id")]
    public int PostId { get; set; }

    [Column("view_count")]
    public int? ViewCount { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("PostViews")]
    public virtual Post Post { get; set; }
}
