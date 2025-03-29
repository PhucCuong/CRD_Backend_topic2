using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("LibraryImage")]
public partial class LibraryImage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("library_id")]
    public int LibraryId { get; set; }

    [Required]
    [Column("video_title")]
    [StringLength(255)]
    public string VideoTitle { get; set; }

    [Required]
    [Column("video_url")]
    [StringLength(500)]
    public string VideoUrl { get; set; }

    [Column("uploaded_at", TypeName = "datetime")]
    public DateTime? UploadedAt { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [ForeignKey("LibraryId")]
    [InverseProperty("LibraryImages")]
    public virtual Libraryy Library { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("LibraryImages")]
    public virtual Account UsernameNavigation { get; set; }
}
