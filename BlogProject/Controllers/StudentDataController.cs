using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProject.Models;
using MySql.Data.MySqlClient;

namespace BlogProject.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Students table of our school database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Students (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> ListStudents(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student Names
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFName = StudentFName;
                NewStudent.StudentLName = StudentLName;

                //Add the Student Name to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Student names
            return Students;
        }

        /// <summary>
        /// Finds an Student based on the Student ID
        /// </summary>
        /// <example> GET api/Studentdata/findStudent/{id}</example>
        /// <param name="id">The ID of the Student</param>
        /// <returns>the name of the Student</returns>
        [HttpGet]
        [Route("api/Studentdata/findStudent/{id}")]
        public Student FindStudent(int id)
        {
            //when we want to contact the database, use a query

            Student NewStudent = new Student();

            //accessing the database through connection string
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection to the db
            Conn.Open();

            //creating a new mysql command query
            MySqlCommand Cmd = Conn.CreateCommand();

            //setting the command query to the string we generated in query variable
            Cmd.CommandText = "Select * from students where studentid = @id";
            Cmd.Parameters.AddWithValue("@id", id);
            Cmd.Prepare();

            //read through the results for our query
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //iterating through our results -- even if there is one one
            while (ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFName = StudentFName;
                NewStudent.StudentLName = StudentLName;
            }

            return NewStudent;
        }

    }
}
