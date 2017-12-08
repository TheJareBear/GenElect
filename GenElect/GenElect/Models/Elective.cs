using System.Collections.Generic;

namespace GenElect.Models
{
    public class Elective
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int CurrentStudentCount { get; set; }
        public int? PeriodID { get; set; }
        public virtual Period Period { get; set; }
        //public List<ApplicationUser> Roster { get; set; }
    }
}