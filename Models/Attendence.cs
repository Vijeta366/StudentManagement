using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Attendence
    {
        [Key]
        public int AtendenceId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public Boolean IsPresent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }


    }
}