using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("LoginHistory")]
public partial class LoginHistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("login_at", TypeName = "datetime")]
    public DateTime LoginAt { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("LoginHistories")]
    public virtual Account UsernameNavigation { get; set; }
}
