using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using PruebasORM001.Models;

namespace PruebasORM001.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public JsonResult GetPersons()
        {
            string connetionString = @"Data Source=ARES\ARES_SQL;Initial Catalog=Pruebas001;Integrated Security=SSPI;";
            SqlConnection cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            string sql, output = "";

            sql = "SELECT * FROM Persons";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                output += $"{dataReader.GetValue(0)} - {dataReader.GetValue(1)}\n";
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return Json(new { output });
        }


        public JsonResult InsertPerson()
        {
            string connetionString = @"Data Source=ARES\ARES_SQL;Initial Catalog=Pruebas001;Integrated Security=SSPI;";
            SqlConnection cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "";

            sql = "INSERT INTO Persons (firstName, lastName, age) VALUES ('Macarena', 'Brook', 25)";
            command = new SqlCommand(sql, cnn);
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();

            return Json(new { ok = true });
        }

        public JsonResult GetPersonAttributes()
        {
            var person = new Person();
            person.FirstName = "Facundo";
            person.LastName = "Garcia";
            person.Age = 29;
            person.Id = 88;
            person.Grades = new List<int>();
            person.Grades.Add(19);
            person.Grades.Add(99);

            var properties = typeof(Person).GetProperties();
            var values = new Dictionary<string, object>();

            foreach(var property in properties.Where(p => Attribute.IsDefined(p, typeof(SpecialAttribute))))
            {
                values.Add(property.Name, property.GetValue(person));
            }

            return Json(new { ok = values });
        }

        public JsonResult Index()
        {
            var assembly = typeof(HomeController).Assembly;
            var values = new List<string>();

            foreach(var type in assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(SpecialAttribute))))
            {
                values.Add(type.Name);
            }

            return Json(new { ok = values });
        }
    }
}