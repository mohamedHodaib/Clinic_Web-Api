using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class PatientDTO
    {
        public int PatientID { get; }
        public int PersonID { get; }


        public PatientDTO(int patientID, int personID)
        {
            PatientID = patientID;
            PersonID = personID;
        }

    }


    

}
