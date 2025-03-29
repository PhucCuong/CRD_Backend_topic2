using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("PostLike")]
public partial class PostLike
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

    [Column("like_at", TypeName = "datetime")]
    public DateTime LikeAt { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("PostLikes")]
    public virtual Post Post { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("PostLikes")]
    public virtual Account UsernameNavigation { get; set; }
}
