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
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        
        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<string> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<String> TeacherNames = new List<string>{};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                //Add the Teacher Name to the List
                TeacherNames.Add(TeacherName);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return TeacherNames;
        }

        /// <summary>
        /// Finds an teacher based on the teacher ID
        /// </summary>
        /// <example> GET api/teacherdata/findteacher/{id}</example>
        /// <param name="id">The ID of the teacher</param>
        /// <returns>the name of the teacher</returns>
        [HttpGet]
        [Route("api/teacherdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            //when we want to contact the database, use a query
            string query = "select * from teachers where teacherid=" + id;


            //accessing the database through connection string
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection to the db
            Conn.Open();

            //creating a new mysql command query
            MySqlCommand Cmd = Conn.CreateCommand();

            //setting the command query to the string we generated in query variable
            Cmd.CommandText = query;

            //read through the results for our query
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            Teacher SelectedTeacher = new Teacher();

            //iterating through our results -- even if there is one one
            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.HireDate = (DateTime)ResultSet["hiredate"];
                SelectedTeacher.Salary = (decimal)ResultSet["salary"];
 
            }

            Conn.Close();

            return SelectedTeacher;
        }

    }
}
