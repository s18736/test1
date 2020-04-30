using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models;

namespace Test1.DAL
{
    public interface IDoctorsDAO
    {
        DoctorResponse GetDoctor(int id);
        void AppendPrescriptions(DoctorResponse response, int id);

        void DeleteDoctor(int id);

        void DeletePrescriptions(int id);
    }
}
