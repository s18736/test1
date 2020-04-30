
using System;
using Microsoft.AspNetCore.Mvc;
using Test1.DAL;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test1.Controllers
{
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private IDoctorsDAO dao { get; set; }
        public DoctorsController(IDoctorsDAO dao)
        {
            this.dao = dao;
        }
     
        [HttpGet("{id}")]
        public IActionResult getDoctor(int id)
        {
            try
            {
                var response = dao.GetDoctor(id);
                dao.AppendPrescriptions(response, id);
                return Ok(response);
            }
            catch (Exception exception)
            {
                Console.Write(exception.StackTrace);
                return NotFound("Doctor does not exist!");
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult deleteDoctor(int id)
        {
            try
            {
                dao.DeleteDoctor(id);
                dao.DeletePrescriptions(id);
                return Ok("Deleted!");
            }
            catch (Exception exception)
            {
                Console.Write(exception.StackTrace);
                return NotFound("Something went wrong!");
            }
        }
    }
}
