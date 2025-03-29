using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("Post")]
public partial class Post
{
    [Key]
    [Column("post_id")]
    public int PostId { get; set; }

    [Required]
    [Column("title")]
    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    [Column("short_description")]
    [StringLength(255)]
    public string ShortDescription { get; set; }

    [Required]
    [Column("content")]
    public string Content { get; set; }

    [Column("field_id")]
    public int FieldId { get; set; }

    [Column("view_count")]
    public int? ViewCount { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [ForeignKey("FieldId")]
    [InverseProperty("Posts")]
    public virtual FieldOfActivity Field { get; set; }

    [InverseProperty("Post")]
    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    [InverseProperty("Post")]
    public virtual ICollection<PostImage> PostImages { get; set; } = new List<PostImage>();

    [InverseProperty("Post")]
    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    [InverseProperty("Post")]
    public virtual ICollection<PostShare> PostShares { get; set; } = new List<PostShare>();

    [InverseProperty("Post")]
    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

    [InverseProperty("Post")]
    public virtual ICollection<PostView> PostViews { get; set; } = new List<PostView>();

    [ForeignKey("Username")]
    [InverseProperty("Posts")]
    public virtual Account UsernameNavigation { get; set; }
}
