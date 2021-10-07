using ApiTestTrueHome.Models.Dtos;
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
        public DateTime Created_at { get; set; }
        public string Status { get; set; }

        private string condition;
        public string Condition
        {
            get
            {
                if (Status == "Active")
                {
                    if (Schedule >= DateTime.Now)
                        condition = "Pendiente a realizar";
                    else
                        condition = "Atrasada";
                }
                else if (Status == "Done")
                {
                    condition = "Finalizada";
                }
                else if (Status == "Canceled")
                {
                    condition = "Cancelada";
                }
                return condition;
            }
            set { condition = value; }
        }
        public int Property_Id { get; set; }
        public PropertyDto Property { get; set; }
        public string Answers { get; set; }
    }
}
