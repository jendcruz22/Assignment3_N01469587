using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Teacher
    {

        //What describes an author?
        public int TeacherId { get; set;  }

        public string TeacherFName { get; set; }

        public string TeacherLName { get; set; }

        public string EmployeeNumber { get; set; }

        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }

    }
}