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
using Microsoft.EntityFrameworkCore;

namespace connectingToDBTESTING.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;
 
        public WeddingController(WeddingContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
            List<Dictionary<string, object>> err = HttpContext.Session.GetObjectFromJson<List<Dictionary<string, object>>>("reg_errors"); 
            ViewBag.errors = err;
            string login_err = HttpContext.Session.GetObjectFromJson<string>("login_errors"); 
            ViewBag.err = login_err;
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid){
                User CurrentUser = new User(){
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    Email = model.Email,
                    ConPassword = model.ConPassword,
                };
                 _context.Add(CurrentUser);
                _context.SaveChanges();
                HttpContext.Session.SetObjectAsJson("cur_user", CurrentUser);
                return RedirectToAction("Dashboard");
            }else{
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                Console.WriteLine(messages);
                HttpContext.Session.SetObjectAsJson("reg_errors", ModelState.Values);
                return RedirectToAction("Index");
            }
            
            //List<Dictionary<string, object>> Allq = _dbConnector.Query("SELECT * FROM quotes ORDER BY created_at Desc");
            
        }
        
        [HttpPost]
        [Route("/logining")]
        public IActionResult Logining(string email, string password)
        {
            User RUser = _context.Users.SingleOrDefault(user => user.Email == email);
            if(RUser==null){
                string errors = "Invalid email or password";
                HttpContext.Session.SetObjectAsJson("login_errors", errors); 
                return RedirectToAction("Index");
            }
            Console.WriteLine("++++++++++++++++++");
            Console.WriteLine(RUser.Password);
            Console.WriteLine(password);
            Console.WriteLine("++++++++++++++++++");
            if(RUser.Password==password){
                HttpContext.Session.SetObjectAsJson("cur_user", RUser);
                return RedirectToAction("Dashboard");
            }
            string errors2 = "Invalid email or password";
            HttpContext.Session.SetObjectAsJson("login_errors", errors2); 
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetObjectFromJson<User>("cur_user")==null){
                return RedirectToAction("Index");
            }
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            User RetrievedUser2 = _context.Users.SingleOrDefault(user => user.UserId == cur_user.UserId);
            List<Wedding> filter = _context.Weddings.Where(wed => wed.Date < DateTime.Now).ToList();
            foreach(Wedding wed in filter){
                Console.WriteLine(wed.WedderOne);
                _context.Weddings.Remove(wed);
                _context.SaveChanges();
            }
            List<Wedding> AllWeddings = _context.Weddings.OrderBy(wed => wed.Date).Include(w=>w.Guests).ThenInclude(g => g.User).ToList();
            List<Guest> AllGuests = _context.Guests.ToList();
            ViewBag.AllGuests = AllGuests;
            ViewBag.cur_user = RetrievedUser2;
            ViewBag.AllWeddings = AllWeddings;
            return View();
        }
        [HttpGet]
        [Route("createwedding")]
        public IActionResult createwedding()
        {
            List<Dictionary<string, object>> wed_err = HttpContext.Session.GetObjectFromJson<List<Dictionary<string, object>>>("wed_errors"); 
            ViewBag.wed_errors = wed_err;
            return View();
        }
        [HttpPost]
        [Route("newwedding")]
        public IActionResult newwedding(Wedding model)
        {   
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            Console.WriteLine("+++++++++++++++++++++++++++++");

            TryValidateModel(model);
            Console.WriteLine("+++++++++++++++++++++++++++++");
            if(ModelState.IsValid){
                Wedding wed = new Wedding(){
                    WedderOne = model.WedderOne,
                    WedderTwo = model.WedderTwo,
                    UserId = cur_user.UserId,
                    Date = model.Date,
                    Address = model.Address
                };
                _context.Add(wed);
                _context.SaveChanges();
                HttpContext.Session.SetObjectAsJson("wed_errors", null);
                return RedirectToAction("Dashboard");
            }else{
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                Console.WriteLine(messages);
                HttpContext.Session.SetObjectAsJson("wed_errors", ModelState.Values);
                return RedirectToAction("createwedding");
            
        }
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id)
        {
            Console.WriteLine(id);
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            Wedding RetrievedWed = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == id);
            if(RetrievedWed.UserId==cur_user.UserId){
                _context.Weddings.Remove(RetrievedWed);
                _context.SaveChanges();
            }else{
                Console.WriteLine("WARNING! HACKER IS DETECTED! SEARCH AND DESTROY!");
            }
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("attend/{id}")]
        public IActionResult attend(int id)
        {
            Console.WriteLine(id);
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            Wedding RetrievedWed = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == id);
            RetrievedWed.GuestsAmount++;
            _context.SaveChanges();
            Guest NewGuest = new Guest{
                UserId = cur_user.UserId,
                WeddingId = id
            };
            _context.Add(NewGuest);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        
        [Route("changeyourmind/{id}")]
        public IActionResult changeyourmind(int id)
        {
            Console.WriteLine(id);
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            Guest RetrievedGuest = _context.Guests.Where(wed => wed.WeddingId == id).SingleOrDefault(user=> user.UserId == cur_user.UserId);
            _context.Guests.Remove(RetrievedGuest);
            _context.SaveChanges();
            Wedding RetrievedWed = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == id);
            RetrievedWed.GuestsAmount--;
            _context.SaveChanges();
            
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("wedding/{id}")]
        public IActionResult wedding(int id)
        {
            Console.WriteLine(id);
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            Wedding RetrievedWed = _context.Weddings.Include(w=>w.Guests).ThenInclude(g => g.User).SingleOrDefault(wed => wed.WeddingId == id);
            ViewBag.wedding = RetrievedWed;
            return View();
        }

            //     _bcontext.Add(tr);
            //     _bcontext.SaveChanges();
            //     User RetrievedUser = _bcontext.Users.SingleOrDefault(user => user.UserId == cur_user.UserId);
            //     Console.WriteLine("++++++++++++++++++");
            //     Console.WriteLine(RetrievedUser.Balance + model.Activity);
            //     RetrievedUser.Balance = RetrievedUser.Balance + model.Activity;
            //     Console.WriteLine(RetrievedUser.Balance);
            //     _bcontext.SaveChanges();
            
            
            // Console.WriteLine("++++++++++++++++++");
            
        

        // [HttpPost]
        // [Route("/addrev")]
        // public IActionResult adduser(Review NewRev)
        // {
            
        //     if(ModelState.IsValid){
        //         _context.Add(NewRev);
        //         HttpContext.Session.SetObjectAsJson("TheList", null);
        //     // OR _context.Users.Add(NewPerson);
        //         _context.SaveChanges();
        //         return RedirectToAction("allTransactions");
        //     }else{
        //         string messages = string.Join("; ", ModelState.Values
        //                                 .SelectMany(x => x.Errors)
        //                                 .Select(x => x.ErrorMessage));
        //         Console.WriteLine(messages);
        //         HttpContext.Session.SetObjectAsJson("TheList", ModelState.Values);
        //         return RedirectToAction("Index");
        //     }
            
        // }
        // [HttpGet]
        // [Route("account/{id}")]
        // public IActionResult allTransactions()
        // {
        //     List<Transaction> Transactions = _tcontext.Transactions.Include(tran => tran.User).OrderByDescending(tran => tran.CreatedAt).ToList();
        //     ViewBag.Transactions = Transactions;
        //     return View();
        // }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.SetObjectAsJson("cur_user", null);
            return RedirectToAction("Index");
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
