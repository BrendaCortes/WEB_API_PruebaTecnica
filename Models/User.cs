using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Models
{
    [Table("ccUsers")]
    public class User
    {
        [Key]
        public int User_id { get; set; }
        public required string Login { get; set; }
        public required string Nombres { get; set; }
        public required string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public required string Password { get; set; }
        public int TipoUser_id { get; set; }

        public int status { get; set; }

        public DateTime fCreate { get; set; }
        public int IDArea { get; set; }
        public DateTime LastLoginAttempt { get; set; }

        public ccRIACat_Areas Area { get; set; }
        public ICollection<Login> Logins { get; set; }
        
    }
}
