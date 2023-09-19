using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace site.Models
{

    [Table("anime")]
    public class Anime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Russian { get; set; }

        [Required]
        [MaxLength(255)]
        public string ImageOriginal { get; set; }

        [Required]
        [MaxLength(255)]
        public string ImagePreview { get; set; }

        [Required]
        [MaxLength(255)]
        public string ImageX96 { get; set; }

        [Required]
        [MaxLength(255)]
        public string ImageX48 { get; set; }

        [Required]
        [MaxLength(255)]
        public string Url { get; set; }

        [Required]
        [MaxLength(255)]
        public string Kind { get; set; }

        [Required]
        public double Score { get; set; }

        [Required]
        [MaxLength(255)]
        public string Status { get; set; }

        public int Episodes { get; set; }

        [JsonProperty("episodes_aired")]
        public int EpisodesAired { get; set; }

        [JsonProperty("aired_on")]
        public DateTime AiredOn { get; set; }

        [JsonProperty("released_on")]
        public DateTime ReleasedOn { get; set; }
    }

}
