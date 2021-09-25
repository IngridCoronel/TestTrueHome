using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Models
{
    public class ActivityListStatusDto
    {
        [Key]
        [Required(ErrorMessage = "El campo Status es obligatorio")]
        public string Status { get; set; }
        [Required(ErrorMessage = "El campo IniSchedule es obligatorio")]
        public DateTime IniSchedule { get; set; }
        [Required(ErrorMessage = "El campo EndSchedule es obligatorio")]
        public DateTime EndSchedule { get; set; }
    }
}
