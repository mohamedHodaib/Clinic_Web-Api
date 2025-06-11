using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class PatientInfoDTO : PersonDTO
    {
        public int PatientID { get; set; }

        public PatientInfoDTO(int patientID, int personID, string name, DateOnly? dateOfBirth, bool gender,
            string phoneNumber, string email, string address)
            :base (personID,name, dateOfBirth, gender, phoneNumber, email,address)
        {
            PatientID = patientID;
            PersonID = personID;
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }
    }
}
