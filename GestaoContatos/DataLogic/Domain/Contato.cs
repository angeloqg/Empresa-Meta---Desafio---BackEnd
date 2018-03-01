
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLogic.Domain
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idContato { get; set; }

        [Required]
        [StringLength(255)]
        public string nome { get; set; }

        [Required]
        [StringLength(255)]
        public string canal { get; set; }

        [Required]
        [StringLength(255)]
        public string valor { get; set; }

        [StringLength(255)]
        public string obs { get; set; }
    }
}
