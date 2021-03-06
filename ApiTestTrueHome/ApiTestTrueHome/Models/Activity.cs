using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Models
{
    public class Activity
    {
       [Key]
        public int Id { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Status { get; set; }
        public int Property_Id { get; set; }
        [ForeignKey("Property_Id")]
        public Property Property { get; set; }
    }
}
