using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ageebSoft.SignlR.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace ageebSoft.SignlR.Core.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHubContext<MyHub> myhub;

        public HomeController(IHubContext<MyHub> _myhub)
        {
            myhub = _myhub;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyHub(string groupName="GroupMorsal", string userName = "ageeb")
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest("GroupName Is Null");
            }
            ViewData["GroupName"] = groupName;
            ViewData["userName"] = userName;

            myhub.Clients.All.SendAsync("Send", userName, $"Home page loaded at: {DateTime.Now}").Wait();
            //myhub.Groups.AddToGroupAsync(myhub.Clie
            // MyHub HubObj = new MyHub();
            //  var RequiredId = HubObj..InvokeHubMethod();
            //var hub = new MyHub();
            //hub.JoinGroup(groupName).Wait();

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
