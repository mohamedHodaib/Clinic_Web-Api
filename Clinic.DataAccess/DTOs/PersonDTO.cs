using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class PersonDTO
    {
        public int PersonID { get; set; }

        public string Name { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public PersonDTO(int personID, string name, DateOnly? dateOfBirth, bool gender, 
            string phoneNumber, string email, string address)
        {
            PersonID = personID;
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }


        public bool IsValid() =>
            _Validate();
            
        private bool _Validate() =>
            !string.IsNullOrEmpty(Name)
           && Name.All(char.IsLetter)
           && IsValidEmail(Email)
           && IsValidPhone(PhoneNumber)
           && !string.IsNullOrEmpty(Address)
           && DateOfBirth != default;


        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            return !string.IsNullOrEmpty(phone)
            && phone.All(char.IsDigit)
            && (phone.StartsWith("011") ||
             phone.StartsWith("010") || phone.StartsWith("012"))
            && phone.Length == 11;
        }

    }
}
