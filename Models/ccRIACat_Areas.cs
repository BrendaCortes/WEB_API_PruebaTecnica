using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Models
{
    [Table("ccRIACat_Areas")]
    public class ccRIACat_Areas
    {
        [Key]
        public int IDArea { get; set; }
        public required string AreaName { get; set; }
        public int StatusArea { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
