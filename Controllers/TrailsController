using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using connectingToDBTESTING.Models;
using System.Linq;
using Newtonsoft.Json;
using connectingToDBTESTING.Factory;
using Dapper;
namespace connectingToDBTESTING.Controllers
{
    public class TrailsController : Controller
    {
        private readonly TrailFactory trailFactory;
        public TrailsController(DbConnector connect)
        {
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            IEnumerable<Trail>  all = trailFactory.FindAll();
            ViewBag.all = all;
            return View();
        }

        [HttpGet]
        [Route("newtrail")]
        public IActionResult NewTrail()
        {
            List<Dictionary<string, object>> err = HttpContext.Session.GetObjectFromJson<List<Dictionary<string, object>>>("TheList");
            
            ViewBag.errors = err;
            return View();
        }

        [HttpGet]
        [Route("trail/{id}")]
        public IActionResult Trail(int id)
        {
            Trail res = trailFactory.FindByID(id);
            ViewBag.trail = res;
            return View();
        }

        [HttpPost]
        [Route("store")]
        public IActionResult Store(Trail model)
        {
            if(ModelState.IsValid){
                
                //List<Dictionary<string, object>> User = _dbConnector.Query($"INSERT INTO users (first_name, last_name, email, password, age) VALUES ('{model.FirstName}', '{model.LastName}', '{model.Email}', '{model.Password}', 0)");
                trailFactory.Add(model);
                
                return RedirectToAction("Index");
            }else{
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                Console.WriteLine(messages);
                HttpContext.Session.SetObjectAsJson("TheList", ModelState.Values);
                return RedirectToAction("NewTrail");
            }
            
            //List<Dictionary<string, object>> Allq = _dbConnector.Query("SELECT * FROM quotes ORDER BY created_at Desc");
            
        }
            
    }
    

public static class SessionExtensions
{
    // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        // This helper function simply serializes theobject to JSON and stores it as a string in session
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
       
    // generic type T is a stand-in indicating that we need to specify the type on retrieval
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        string value = session.GetString(key);
        // Upon retrieval the object is deserialized based on the type we specified
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}
}