using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("Contact")]
public partial class Contact
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("full_name")]
    [StringLength(255)]
    public string FullName { get; set; }

    [Required]
    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; }

    [Column("phone")]
    [StringLength(50)]
    public string Phone { get; set; }

    [Required]
    [Column("contact_message")]
    [StringLength(1000)]
    public string ContactMessage { get; set; }

    [Column("contact_status")]
    [StringLength(50)]
    public string ContactStatus { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Contact")]
    public virtual ICollection<ContactLog> ContactLogs { get; set; } = new List<ContactLog>();

    [InverseProperty("Contact")]
    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
}
