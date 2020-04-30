using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models;

namespace Test1.DAL
{
    public class DoctorsDao : IDoctorsDAO
    {
        private static string connectionString = "Data Source=db-mssql;Initial Catalog = s18736; Integrated Security = True";

        public void AppendPrescriptions(DoctorResponse response, int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "SELECT IdPrescription, Date, DueDate, FirstName, LastName " +
                                    "FROM Prescription " +
                                    "JOIN Patient ON Prescription.IdPatient = Patient.IdPatient " +
                                    "WHERE IdDoctor = @Id " +
                                    "ORDER BY Date DESC;";
                command.Parameters.AddWithValue("Id", id);
                var reader2 = command.ExecuteReader();
                response.Prescriptions = new List<PrescriptionModel>();
                while (reader2.Read())
                {
                    PrescriptionModel prescription = new PrescriptionModel();
                    prescription.IdPerscription = int.Parse(reader2["IdPrescription"].ToString());
                    prescription.Date = reader2["Date"].ToString();
                    prescription.DueDate = reader2["DueDate"].ToString();
                    prescription.PatientsFirstName = reader2["FirstName"].ToString();
                    prescription.PatientsLastName = reader2["LastName"].ToString();
                    response.Prescriptions.Add(prescription);
                }
                reader2.Close();
                
            }
            
        }

        public void DeleteDoctor(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;


                command.CommandText = "DELETE Doctor WHERE IdDoctor = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
                transaction.Commit();

            }

        }

        public void DeletePrescriptions(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;


                command.CommandText = "DELETE Prescription WHERE IdDoctor = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
                transaction.Commit();

            }
        }

        public DoctorResponse GetDoctor(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                DoctorResponse response = new DoctorResponse();

                command.CommandText = "SELECT FirstName, LastName, Email FROM Doctor WHERE IdDoctor = @Id;";
                command.Parameters.AddWithValue("Id", id);
                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    throw new Exception();
                }

                response.FirstName = reader["FirstName"].ToString();
                response.LastName = reader["LastName"].ToString();
                response.Email = reader["Email"].ToString();
                reader.Close();
                return response;

                
            }

        }
    }
}
