namespace smarthealth.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }  

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
