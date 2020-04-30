using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class PrescriptionModel
    {
        public int IdPerscription { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }

        public string PatientsFirstName { get; set; }
        public string PatientsLastName { get; set; }

    }
}
