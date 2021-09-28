using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Models
{
    public class ActivityDtoRequest
    {
        public int idActivity { get; set; }
        public DateTime newScheduleDay { get; set; }
        public string Status { get; set; }
        public DateTime IniSchedule { get; set; }
        public DateTime EndSchedule { get; set; }

    }
}
