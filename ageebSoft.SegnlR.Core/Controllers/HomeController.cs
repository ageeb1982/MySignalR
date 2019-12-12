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
using ageebSoft.SignlR.Core.Models.Hubs;
using Microsoft.AspNetCore.Identity;
using ageebSoft.SignlR.Core.Models.DB;
using ageebSoft.SignlR.Core.Models.data;

namespace ageebSoft.SignlR.Core.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHubContext<MyHub> Imyhub;
         private readonly MyDB mydb;

        public HomeController(IHubContext<MyHub> _Imyhub,MyDB _mydb)
        {
             mydb = _mydb;
            Imyhub = _Imyhub;
         }


          public async Task< IActionResult> Index(string GroupName)
        {
            var message = mydb.Messages;

            var usr = "Unknow User";
            var GrpName = "A";

            
            string userX = HttpContext.Request.Query["user"];
            string GrpNameX = GroupName;
            if (usr == "Unknow User" && !string.IsNullOrEmpty(userX) && userX != "undefined" && !userX.Equals("null"))
            {
                usr = userX;
            } 
            
            if (GrpName == "A" && !string.IsNullOrEmpty(GrpNameX) && GrpNameX != "undefined" && !GrpNameX.Equals("null"))
            {
                GrpName = GrpNameX;
            }
            if (HttpContext.User.Identity.IsAuthenticated) { 
                usr = HttpContext.User.Identity.Name;
                var currUser = mydb.MyUsers.FirstOrDefault(x=>x.UserName==usr) ;
                if(currUser!=null)
                {
                      
                }
                
            }

          var res = message.Where(x => x.GroupName == GrpName).ToList();
            //Imyhub.Clients.All.SendToRecOnline( DateTime.Now.ToString()).Wait();
            // Imyhub.Clients.All.Send("");
             

            ViewData["UserName"] = usr;
            ViewData["GrpName"] = GrpName;

            if (res==null)
            {
                res = new List<Message>();
            }

            return View(res);
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
             

            return View(mydb.Messages);
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

    internal class HubMethods
    {
    }
}
