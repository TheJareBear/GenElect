using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenElect.Models
{
    public class Period
    {
        [Display(Name = "Period")]
        public int ID { get; set; }
        [Display(Name = "Period")]
        public int PeriodNumber { get; set; }
        public virtual ICollection<Elective> Electives { get; set; }
    }
}