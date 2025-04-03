using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API_Sample.Models.Request
{
    public class EscapeJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            string escaped = value.Replace("\n", "\\n").Replace("\r", "\\r");
            writer.WriteStringValue(escaped);
        }
    }
    public class MReq_Post
    {
        [Key]
        [Column("post_id")]
        public int PostId { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Required]
        [Column("content")]
        [JsonConverter(typeof(EscapeJsonConverter))]
        public string Content { get; set; }

        [Column("field_id")]
        public int FieldId { get; set; }

        [Column("view_count")]
        public int? ViewCount { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Column("status")]
        public bool IsActive { get; set; }

        [Column("name_slug")]
        [StringLength(255)]
        public string NameSlug { get; set; }

        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
    }

    public class MReq_PostPaging : PagingRequestBase
    {
        public string SequenceStatus { get; set; }

        public string SearchText { get; set; }
    }
}
