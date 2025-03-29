using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.Entities;

[Table("Business")]
public partial class Business
{
    [Key]
    [Column("business_id")]
    public int BusinessId { get; set; }

    [Required]
    [Column("business_name")]
    [StringLength(200)]
    public string BusinessName { get; set; }

    [Column("hotline")]
    [StringLength(10)]
    public string Hotline { get; set; }

    [Column("email")]
    [StringLength(25)]
    public string Email { get; set; }

    [Column("tax_code")]
    [StringLength(50)]
    public string TaxCode { get; set; }

    [Column("logo")]
    [StringLength(255)]
    public string Logo { get; set; }

    [Column("business_address")]
    [StringLength(255)]
    public string BusinessAddress { get; set; }

    [Column("website_color")]
    [StringLength(20)]
    public string WebsiteColor { get; set; }

    [Column("tutorial_file")]
    [StringLength(255)]
    public string TutorialFile { get; set; }

    [Column("link_facebook")]
    [StringLength(255)]
    public string LinkFacebook { get; set; }

    [Column("link_zalo")]
    [StringLength(255)]
    public string LinkZalo { get; set; }

    [Column("link_youtube")]
    [StringLength(255)]
    public string LinkYoutube { get; set; }

    [InverseProperty("Business")]
    public virtual ICollection<Recruitment> Recruitments { get; set; } = new List<Recruitment>();
}
