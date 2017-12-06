using System.ComponentModel.DataAnnotations;

namespace GenElect.ViewModels
{
    public class RoleViewModel
    {
        public string ID { get; set;}
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}