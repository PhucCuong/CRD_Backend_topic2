using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("Account")]
public partial class Account
{
    [Key]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [Required]
    [Column("pass_word")]
    [StringLength(30)]
    public string PassWord { get; set; }

    [Column("avatar_url")]
    [StringLength(255)]
    public string AvatarUrl { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<ContactLog> ContactLogs { get; set; } = new List<ContactLog>();

    [ForeignKey("EmployeeId")]
    [InverseProperty("Accounts")]
    public virtual EmployeeProfile Employee { get; set; }

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<LibraryFile> LibraryFiles { get; set; } = new List<LibraryFile>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<LibraryImage> LibraryImages { get; set; } = new List<LibraryImage>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<NewsLike> NewsLikes { get; set; } = new List<NewsLike>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<NewsShare> NewsShares { get; set; } = new List<NewsShare>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<PostShare> PostShares { get; set; } = new List<PostShare>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    [InverseProperty("CreateByNavigation")]
    public virtual ICollection<Recruitment> RecruitmentCreateByNavigations { get; set; } = new List<Recruitment>();

    [InverseProperty("UpdateByNavigation")]
    public virtual ICollection<Recruitment> RecruitmentUpdateByNavigations { get; set; } = new List<Recruitment>();

    [ForeignKey("RoleId")]
    [InverseProperty("Accounts")]
    public virtual UserRole Role { get; set; }

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
