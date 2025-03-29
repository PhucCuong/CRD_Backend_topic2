using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("NewsComment")]
public partial class NewsComment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("news_id")]
    public int NewsId { get; set; }

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

    [ForeignKey("NewsId")]
    [InverseProperty("NewsComments")]
    public virtual News News { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("NewsComments")]
    public virtual Account UsernameNavigation { get; set; }
}
