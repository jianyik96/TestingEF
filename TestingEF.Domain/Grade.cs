using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingEF.Domain
{
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
       
        public int GradeID { get; set; }
        public string GradeName { get; set; }
        public int StudentId { get; set; }
    }
}
