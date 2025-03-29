using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("Libraryy")]
public partial class Libraryy
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; }

    [Column("libraryy_description")]
    [StringLength(500)]
    public string LibraryyDescription { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Libraryys")]
    public virtual LibraryCategory Category { get; set; }

    [InverseProperty("Library")]
    public virtual ICollection<LibraryFile> LibraryFiles { get; set; } = new List<LibraryFile>();

    [InverseProperty("Library")]
    public virtual ICollection<LibraryImage> LibraryImages { get; set; } = new List<LibraryImage>();
}
