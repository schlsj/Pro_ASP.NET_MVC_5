using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HelperMethods.Models;

namespace HelperMethods.Controllers
{
    public class PeopleController : Controller
    {
        private Person[] personData =
        {
            new Person {FirstName = "Adam", LastName = "Freeman", Role = Role.Admin},
            new Person {FirstName = "Jacqui", LastName = "Griffyth", Role = Role.User},
            new Person {FirstName = "John", LastName = "Smith", Role = Role.User},
            new Person {FirstName = "Anne", LastName = "Jones", Role = Role.Guest},
        };

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult GetPeople()
        //{
        //    return View(personData);
        //}

        //[HttpPost]
        //public ActionResult GetPeople(string selectedRole)
        //{
        //    if (selectedRole == null || selectedRole == "All")
        //    {
        //        return View(personData);
        //    }
        //    Role selected = (Role) Enum.Parse(typeof(Role), selectedRole);
        //    return View(personData.Where(a => a.Role == selected));
        //}

        //public PartialViewResult GetPeopleData(string selectedRole = "All")
        //{
        //    IEnumerable<Person> data = personData;
        //    if (selectedRole != "All")
        //    {
        //        Role role = (Role)Enum.Parse(typeof(Role), selectedRole);
        //        data = personData.Where(a => a.Role == role);
        //    }
        //    return PartialView(data);
        //}

        public IEnumerable<Person> GetData(string selectedRole = "All")
        {
            IEnumerable<Person> data = personData;
            if (selectedRole != "All")
            {
                Role role = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(a => a.Role == role);
            }
            return data;
        }

        public JsonResult GetPeopleDataJson(string selectedRole = "All")
        {
            //IEnumerable<Person> data = GetData(selectedRole);
            var data = GetData(selectedRole).Select(a => new
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Role = Enum.GetName(typeof(Role), a.Role)
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetPeopleData(string selectedRole = "All")
        {
            IEnumerable<Person> data = GetData(selectedRole);
            return PartialView(data);
        }

        public ActionResult GetPeople(string selectedRole = "All")
        {
            return View((object) selectedRole);
        }
    }
}