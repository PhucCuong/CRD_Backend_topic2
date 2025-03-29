using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("UserNotification")]
public partial class UserNotification
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [Required]
    [Column("title")]
    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    [Column("content")]
    [StringLength(255)]
    public string Content { get; set; }

    [Column("notification_id")]
    public int NotificationId { get; set; }

    [Column("is_read")]
    public bool IsRead { get; set; }

    [Column("create_at", TypeName = "datetime")]
    public DateTime CreateAt { get; set; }

    [ForeignKey("NotificationId")]
    [InverseProperty("UserNotifications")]
    public virtual NotificationType Notification { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("UserNotifications")]
    public virtual Account UsernameNavigation { get; set; }
}
