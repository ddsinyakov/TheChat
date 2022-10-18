using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheChat.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public String FirstName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public String SecondName { get; set; } = null!;

        [Required]
        public String Login { get; set; } = null!;

        [Required]
        public String Email { get; set; } = null!;

        [Column(TypeName = "char(64)")]
        public String Password { get; set; } = null!;

        [Column(TypeName = "char(64)")]
        public String Salt { get; set; } = String.Empty;

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastLoginDate { get; set; }
    }
}
