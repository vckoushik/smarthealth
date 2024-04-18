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
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
    
}
