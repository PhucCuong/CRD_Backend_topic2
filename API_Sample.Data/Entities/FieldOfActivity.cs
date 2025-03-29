using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("FieldOfActivity")]
public partial class FieldOfActivity
{
    [Key]
    [Column("field_id")]
    public int FieldId { get; set; }

    [Required]
    [Column("field_name")]
    [StringLength(50)]
    public string FieldName { get; set; }

    [Column("status_")]
    public int Status { get; set; }

    [InverseProperty("Field")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
