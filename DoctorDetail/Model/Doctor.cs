using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorDetail.Model
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Qualification { get; set; }
        [DisplayName("Department")]
        public int Dept_Id { get; set; }
        public double MobileNo { get; set; }
        public string? AvailableDays { get; set; }
    }
}
