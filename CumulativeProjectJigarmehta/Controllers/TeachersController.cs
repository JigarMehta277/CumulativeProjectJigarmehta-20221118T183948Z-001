using CumulativeProjectJigarmehta.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CumulativeProjectJigarmehta.Controllers
{
    public class TeachersController : ApiController
    {
        //The databse context class which allows us to access our MySql Database.
        private CumulativeProjectDb Teachers = new CumulativeProjectDb();
        //This Controller will access the teachers table of our project database.

        //This Controller will access the teachers table of our project database/
        /// <summary>
        /// Returns a list of teachers in the system.
        /// </summary>
        /// <example>
        /// GET api/Teachers/List
        /// </example>
        /// <param name="SearchKey"></param>
        /// <returns>A list of teachers (first names and last names)</returns>
        [HttpGet]
        [Route("api/Teachers/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {   
            //Create an instance of a connection.
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open the connection between the web server and database.
            Conn.Open();

            //Establish a new command(query) for our database.
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat (teacherfname, ' ', teacherlname)) like lower(@key)";

            //A extra step is done to protect the database against SQL injection, SearchKey is replaced by @key, so malacious inputs cannot execute.
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Results of the query would be stored here.
            MySqlDataReader Results = cmd.ExecuteReader();

            //It is responsible for displaying the list of teachers.
            List<Teacher> TeachersInfo = new List<Teacher> { };


            //Loop through Each Row the Result Set
            while (Results.Read())
            {
                //Access Column information by the DB column name as an index.
                DateTime HireDate = (DateTime)Results["hiredate"];
                decimal Salary = (decimal)Results["salary"];
                string EmployeeNumber = (string)Results["employeenumber"];
                int TeacherId = (int)Results["teacherid"];
                string TeacherFname = (string)Results["teacherfname"];
                string TeacherLname = (string)Results["teacherlname"];

                //Object is created to pass data to teacher class.
                Teacher NewTeacher = new Teacher();

                //data is passed to teacher class, and can be accessed through variables.
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                TeachersInfo.Add(NewTeacher);
            }

            //Close the connection between the web server and database.
            Conn.Close();


            //returns final list of teachers names.
            return TeachersInfo;


        }


        /// <summary>
        /// Returns teachers details in the system.
        /// </summary>
        /// <example>GET api/Teachers/Show </example>
        /// <param name="id"></param>
        /// <returns>returns details of teachers(hiredate, salary, employeenumber, teacherid, teacherfname, teacherlname)</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection.
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open the connection between the web server and database.
            Conn.Open();

            //Establish a new command(query) for our database.
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = "+id;

            //Results of the query would be stored here.
            MySqlDataReader Results = cmd.ExecuteReader();

            //Loop through Each Row the Result Set
            while (Results.Read())
            {
                //Access Column information by the DB column name as an index.
                DateTime HireDate = (DateTime)Results["hiredate"];
                decimal Salary = (decimal)Results["salary"];
                string EmployeeNumber = (string)Results["employeenumber"];
                int TeacherId = (int)Results["teacherid"];
                string TeacherFname = (string)Results["teacherfname"];
                string TeacherLname = (string)Results["teacherlname"];

                //data is passed to teacher class, and can be accessed through variables.
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            //returns final details of teachers.
            return NewTeacher;
        }
    }
}
