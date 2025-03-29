using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("PostComment")]
public partial class PostComment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("post_id")]
    public int PostId { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [Required]
    [Column("comment_content")]
    public string CommentContent { get; set; }

    [Column("comment_at", TypeName = "datetime")]
    public DateTime CommentAt { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("PostComments")]
    public virtual Post Post { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("PostComments")]
    public virtual Account UsernameNavigation { get; set; }
}
