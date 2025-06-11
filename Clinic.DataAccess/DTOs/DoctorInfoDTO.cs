using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class DoctorInfoDTO:PersonDTO
    {
        public int DoctorID { get; set; }

        public string Specialization {  get; set; }

        public DoctorInfoDTO(int doctorID,int personID, string name, DateOnly? dateOfBirth, bool gender, 
            string phoneNumber, string email, string address, string specialization)
            : base(personID,name, dateOfBirth, gender,phoneNumber,email,address)
        {
            DoctorID = doctorID;
            Specialization = specialization ;
        }

        public bool IsValidDateOfBirth() =>
            DateOfBirth <= DateOnly.FromDateTime(DateTime.Now.Date.AddYears(-25));

        public bool IsValidSpecialization() =>
            Specialization.All(char.IsLetter);
    }

}
