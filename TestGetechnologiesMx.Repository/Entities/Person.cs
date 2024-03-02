using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologiesMx.Repository.Entities
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150)]
        public string PaternalSurname { get; set; }

        [StringLength(150)]
        public string? MaternalSurname { get; set; }

        [Required]
        [StringLength(150)]
        public string Identification { get; set; }
    }
}
