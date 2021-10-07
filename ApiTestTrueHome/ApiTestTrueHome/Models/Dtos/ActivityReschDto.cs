using ApiTestTrueHome.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Models
{
    public class ActivityReschDto
    {
        [Required(ErrorMessage = "El campo Id es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Schedule es obligatorio")]
        public DateTime Schedule { get; set; }
    }
}
