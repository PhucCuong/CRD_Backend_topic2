using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("NewsShare")]
public partial class NewsShare
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

    [Column("share_at", TypeName = "datetime")]
    public DateTime ShareAt { get; set; }

    [ForeignKey("NewsId")]
    [InverseProperty("NewsShares")]
    public virtual News News { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("NewsShares")]
    public virtual Account UsernameNavigation { get; set; }
}
