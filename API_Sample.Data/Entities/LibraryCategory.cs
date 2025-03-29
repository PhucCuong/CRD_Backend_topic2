using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

public partial class LibraryCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("category_name")]
    [StringLength(255)]
    public string CategoryName { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Libraryy> Libraryys { get; set; } = new List<Libraryy>();
}
