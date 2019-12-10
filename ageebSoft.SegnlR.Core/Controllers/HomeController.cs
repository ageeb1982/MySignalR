using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ageebSoft.SignlR.Core.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
 using System.Threading;

namespace ageebSoft.SignlR.Core.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHubContext<MyHub> Imyhub;
        
 

         

        public HomeController(IHubContext<MyHub> _Imyhub)//, BGServiceStarter<MyHubBackgroundService> _starter  )
        {

            //starter = _starter;
            Imyhub =  _Imyhub;
            //myhub =(MyHub) _Imyhub;
        }
        public IActionResult Index()
        {

            
            var usr = "Unknow User";
            
            if (HttpContext.User.Identity.IsAuthenticated) usr = HttpContext.User.Identity.Name;
            string userX = HttpContext.Request.Query["user"];
            if (usr == "Unknow User" && !string.IsNullOrEmpty(userX) && userX != "undefined" && !userX.Equals("null"))
            {
                usr = userX;
            }
                Imyhub.Clients.All.SendAsync("RecOnline", usr, DateTime.Now.ToString()).Wait();

            return View();
        }

        public IActionResult MyHubX(string groupName="GroupMorsal", string userName = "ageeb")
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest("GroupName Is Null");
            }
            ViewData["GroupName"] = groupName;
            ViewData["userName"] = userName;

           // myhub.Clients.All.SendAsync("Send", userName, $"Home page loaded at: {DateTime.Now}").Wait();
            

            return View();
        }
        
       

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult WithOutProxy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
