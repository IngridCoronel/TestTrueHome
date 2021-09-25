using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Models
{
    public class SurveyDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Answers es obligatorio")]
        public string Answers { get; set; }
        public int Activity_Id { get; set; }
        public Activity Activity { get; set; }
    }
}
