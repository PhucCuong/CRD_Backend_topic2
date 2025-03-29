using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

public partial class LibraryFile
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("library_id")]
    public int LibraryId { get; set; }

    [Required]
    [Column("name_file")]
    [StringLength(255)]
    public string NameFile { get; set; }

    [Column("file_type")]
    [StringLength(50)]
    public string FileType { get; set; }

    [Required]
    [Column("file_url")]
    [StringLength(500)]
    public string FileUrl { get; set; }

    [Column("uploaded_at", TypeName = "datetime")]
    public DateTime? UploadedAt { get; set; }

    [Required]
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [ForeignKey("LibraryId")]
    [InverseProperty("LibraryFiles")]
    public virtual Libraryy Library { get; set; }

    [ForeignKey("Username")]
    [InverseProperty("LibraryFiles")]
    public virtual Account UsernameNavigation { get; set; }
}
