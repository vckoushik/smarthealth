using System.ComponentModel.DataAnnotations;

namespace smarthealth.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Precaution { get; set; }
        public string Indication { get; set; }
        public string ContraIndication { get; set; }
        public string Dose { get; set; }
        public string SideEffect { get; set; }
        public string ModeOfAction { get; set; }
        public string Interaction { get; set; }
    }
}
