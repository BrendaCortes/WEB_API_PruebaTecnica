using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Models
{
    [Table("ccloglogin")]
    public class Login
    {

        [Key]
        public int id { get; set; }

        public int User_id { get; set; }

        [Required]
        public int Extension { get; set; }

        [Required, Range(0, 1)]
        public int TipoMov { get; set; } // 1: login, 0: logout

        [Required]
        public DateTime fecha { get; set; }

        
        public User? User { get; set; }

    }
}
