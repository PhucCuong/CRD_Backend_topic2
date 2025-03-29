using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

public partial class FeedBack
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("contact_id")]
    public int? ContactId { get; set; }

    [Column("rating")]
    public int? Rating { get; set; }

    [Required]
    [Column("comments")]
    [StringLength(1000)]
    public string Comments { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("ContactId")]
    [InverseProperty("FeedBacks")]
    public virtual Contact Contact { get; set; }
}
