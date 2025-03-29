using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

public partial class Partner
{
    [Key]
    [Column("partner_id")]
    public int PartnerId { get; set; }

    [Required]
    [Column("parner_name")]
    [StringLength(200)]
    public string ParnerName { get; set; }

    [Column("abbreviation_name")]
    [StringLength(10)]
    public string AbbreviationName { get; set; }

    [Required]
    [Column("logo_image")]
    [StringLength(255)]
    public string LogoImage { get; set; }

    [Column("website")]
    [StringLength(500)]
    public string Website { get; set; }

    [Column("contact_email")]
    [StringLength(255)]
    public string ContactEmail { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; }

    [Column("note")]
    [StringLength(500)]
    public string Note { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Partners")]
    public virtual PartnerCategory Category { get; set; }
}
