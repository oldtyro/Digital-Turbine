using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using People.Models;

namespace People.Controllers
{
    public class PersonController : Controller
    {
        public static List<Person> people;

        // GET: Person
        public ActionResult ListPeople(int r = 0)
        {
            if (people == null)
                people = InitPeople();

            Races race = (Races)r;

            List<Person> list = new List<Person>();
            list = people.Where(p => (p.Race == race) && (p.Age % 2 == 0)).ToList();
            list.Sort(SortPerson);

            ViewBag.people = list;
            ViewBag.race = r;
            return View();
        }

        public JsonResult ListPeopleJson(int r = 0)
        {
            if (people == null)
                people = InitPeople();

            Races race = (Races)r;

            List<Person> list = new List<Person>();
            list = people.Where(p => (p.Race == race) && (p.Age % 2 == 0)).ToList();
            list.Sort(SortPerson);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeopleInformation()
        {
            int num = people.Count;
            int min = 0, max = 0;
            double average = 0;
            int enumCount = Enum.GetValues(typeof(Races)).Length;

            max = people.Max(p => p.Age);
            min = people.Min(p => p.Age);
            average = Math.Round(people.Average(p => p.Age), 2);

            int[] numRace = new int[enumCount];
            for (int i = 0; i < enumCount; i++)
            {
                numRace[i] = people.Count(p => p.Race == (Races)i);
            }

            string strJson = "{";
            strJson += "'number':" + num.ToString() + ",";
            strJson += "'age':{'average':" + average.ToString() + ", 'min':" + min.ToString() + ", 'max':" + max.ToString() + "}, ";
            strJson += "'race':[";
            for (int i = 0; i < enumCount; i++)
            {
                strJson += "'" + Enum.GetName(typeof(Races), (Races)i) + "':" + numRace[i].ToString();
                if (i < enumCount - 1)
                    strJson += ", ";
            }
            strJson += "]}";

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        private static int SortPerson(Person p1, Person p2)
        {
            //if (p1.Age.CompareTo(p2.Age) != 0)
                return p1.Age.CompareTo(p2.Age);
            //else
            //    return p1.Name.CompareTo(p2.Name);
        }

        public static List<Person> InitPeople()
        {
            List<Person> people = new List<Person>();
            Random rnd = new Random();

            int enumCount = Enum.GetValues(typeof(Races)).Length;

            for (int i = 0; i < 10000; i++)
            {
                Person p = new Person();
                p.Name = "Person #" + i.ToString();
                p.Age = rnd.Next(1, 99);
                p.Race = (Races)rnd.Next(0, enumCount);
                people.Add(p);
            }

            return people;
        }

        public static List<Person> AddAge(List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                people[i].Age += 1;
            }
            return people;
        }
    }
}