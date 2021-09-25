using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Models
{
    public class ActivityDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Schedule es obligatorio")]
        public DateTime Schedule { get; set; }
        [Required(ErrorMessage = "El campo Title es obligatorio")]
        public string Title { get; set; }
        public int Property_Id { get; set; }
        public Property Property { get; set; }
    }
}
