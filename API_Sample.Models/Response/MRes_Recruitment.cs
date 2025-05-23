﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Response
{
    public class MRes_Recruitment:BaseModel.History
    {
        [Required]
        [Column("recruitment_name")]
        [StringLength(255)]
        public string RecruitmentName { get; set; }

        [Column("business_id")]
        public int BusinessId { get; set; }

        [Required]
        [Column("recruitment_position")]
        [StringLength(100)]
        public string RecruitmentPosition { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }
    }
}
