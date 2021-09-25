using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Models
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Answers is required")]
        public string Answers { get; set; }
        public DateTime Created_at { get; set; }
        public int Activity_Id { get; set; }
        [ForeignKey("Activity_Id")]
        public Activity Activity { get; set; }
    }
}
