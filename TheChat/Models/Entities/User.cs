using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheChat.Features;

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
        [MaxLength(30)]
        public String Login { get; set; } = null!;

        [Required]
        [MaxLength(360)] // used to create unique index for entity. Unique index can't be created with nvarchar(max)
        public String Email { get; set; } = null!;

        [Required]
        [Column(TypeName = "char(64)")]
        public String Password { get; set; } = null!;

        [Required]
        [Column(TypeName = "char(64)")]
        public String Salt { get; set; } = String.Empty;

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastLoginDate { get; set; }

        // Roles Implementation
        # region Roles   

        public Guid RoleId { get; set; } 

        [Required]
        public Role Role { get; set; } = null!;

        #endregion



    }
}
