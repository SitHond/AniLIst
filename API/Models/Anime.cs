using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
    
    [Table("anime")]
    public class Anime
    {
        public int Id { get; set; }
        public string Name { get; set; }      
        public sbyte Score { get; set; }
    }
}
