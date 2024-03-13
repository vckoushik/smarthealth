using System.ComponentModel.DataAnnotations;

namespace smarthealth.Models
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string URL { get; set;}

        public int UserId { get; set; } 
        public ApplicationUser User { get; set; }
    }
    
}
