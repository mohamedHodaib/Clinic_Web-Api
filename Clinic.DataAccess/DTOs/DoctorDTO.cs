using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class DoctorDTO
    {
        public int DoctorID { get; set; }
        public int PersonID { get; set; }

        public string Specialization { get; set; }

        public DoctorDTO(int doctorID, int personID, string specialization)
        {
            DoctorID = doctorID;
            PersonID = personID;
            Specialization = specialization;
        }

    }
}
