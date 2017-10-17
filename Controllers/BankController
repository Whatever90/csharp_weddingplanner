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
    public class BankController : Controller
    {
        private TransactionContext _tcontext;
        private UserContext _ucontext;
        private BankContext _bcontext;
 
        public BankController(TransactionContext tcontext, UserContext ucontext, BankContext bcontext)
        {
            _tcontext = tcontext;
            _ucontext = ucontext;
            _bcontext = bcontext;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
            List<Dictionary<string, object>> err = HttpContext.Session.GetObjectFromJson<List<Dictionary<string, object>>>("reg_errors"); 
            ViewBag.errors = err;
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
                    Balance = 400
                };
                //List<Dictionary<string, object>> User = _dbConnector.Query($"INSERT INTO users (first_name, last_name, email, password, age) VALUES ('{model.FirstName}', '{model.LastName}', '{model.Email}', '{model.Password}', 0)");
                //userFactory.Add(CurrentUser);
                _bcontext.Add(CurrentUser);
                _bcontext.SaveChanges();
                HttpContext.Session.SetObjectAsJson("cur_user", CurrentUser);
                return RedirectToAction("Account");
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
        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            

            string err = HttpContext.Session.GetObjectFromJson<string>("login_errors"); 
            ViewBag.err = err;
            return View();
        }
        [HttpPost]
        [Route("/logining")]
        public IActionResult Logining(string email, string password)
        {
            User RUser = _bcontext.Users.SingleOrDefault(user => user.Email == email);
            if(RUser==null){
                string errors = "Invalid email or password";
                HttpContext.Session.SetObjectAsJson("login_errors", errors); 
                return RedirectToAction("Login");
            }
            Console.WriteLine("++++++++++++++++++");
            Console.WriteLine(RUser.Password);
            Console.WriteLine(password);
            Console.WriteLine("++++++++++++++++++");
            if(RUser.Password==password){
                HttpContext.Session.SetObjectAsJson("cur_user", RUser);
                return RedirectToAction("Account");
            }
            string errors2 = "Invalid email or password";
            HttpContext.Session.SetObjectAsJson("login_errors", errors2); 
            return RedirectToAction("Login");
        }
        
        [HttpGet]
        [Route("account")]
        public IActionResult Account()
        {
            if(HttpContext.Session.GetObjectFromJson<User>("cur_user")==null){
                return RedirectToAction("Index");
            }
            // User RetrievedUser2 = _bcontext.Users.SingleOrDefault(user => user.UserId == cur_user.UserId);
            // List<Transaction> RetrievedUser = _bcontext.Transactions.Where(tr => tr.UserId == cur_user.UserId).OrderByDescending(tr => tr.TransactionId).ToList();
            // ViewBag.user = RetrievedUser2;
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            //-----------------------------
            User RetrievedUser = _bcontext.Users.SingleOrDefault(user => user.UserId == cur_user.UserId);
            RetrievedUser.Transactions = _bcontext.Transactions.Where(tr => tr.UserId == cur_user.UserId).OrderByDescending(tr => tr.TransactionId).ToList();
            //-----------------------------
            ViewBag.cur_user = RetrievedUser;
            ViewBag.account_err = HttpContext.Session.GetObjectFromJson<String>("account_error");
            return View();
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.SetObjectAsJson("cur_user", null);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("action")]
        public IActionResult Action(Transaction model)
        {   
            User cur_user = HttpContext.Session.GetObjectFromJson<User>("cur_user");
            User RetrievedUser2 = _bcontext.Users.SingleOrDefault(user => user.UserId == cur_user.UserId);
            if(RetrievedUser2.Balance+model.Activity<0){
                    Console.WriteLine(model.Activity*-1);
                    string err = "Unsufficient funds";
                    HttpContext.Session.SetObjectAsJson("account_error", err);
                    return RedirectToAction("Account");
                }
            HttpContext.Session.SetObjectAsJson("account_error", null);
            Transaction tr = new Transaction(){
                    Activity = model.Activity,
                    UserId = cur_user.UserId,
                    CreatedAt = DateTime.Today
                };
                _bcontext.Add(tr);
                _bcontext.SaveChanges();
                User RetrievedUser = _bcontext.Users.SingleOrDefault(user => user.UserId == cur_user.UserId);
                Console.WriteLine("++++++++++++++++++");
                Console.WriteLine(RetrievedUser.Balance + model.Activity);
                RetrievedUser.Balance = RetrievedUser.Balance + model.Activity;
                Console.WriteLine(RetrievedUser.Balance);
                _bcontext.SaveChanges();
            
            
            Console.WriteLine("++++++++++++++++++");
            
            return RedirectToAction("Account");
        }

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

