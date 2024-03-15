using System.ComponentModel.DataAnnotations;

namespace smarthealth.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string ImgUrl { get; set; }  
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
