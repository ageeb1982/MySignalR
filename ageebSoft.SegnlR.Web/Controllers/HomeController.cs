using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ageebSoft.SignlR.Core.Models;
using ageebSoft.SignlR.Web.Models;
using ageebSoft.SignlR.Web.Models.data;
using ageebSoft.SignlR.Web.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ageebSoft.SignlR.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHubContext<MyHub> Imyhub;





        public HomeController(IHubContext<MyHub> _Imyhub)//, BGServiceStarter<MyHubBackgroundService> _starter  )
        {

            //starter = _starter;
            Imyhub = _Imyhub;
            //myhub =(MyHub) _Imyhub;
        }
        public async Task<IActionResult> Index()
        {
            var mydb = new MyDB();
            var message = new List<Message>();

            try
            {
                message = await mydb.Messages.ToListAsync();
            }
            catch
            { }


            var usr = "Unknow User";

            if (HttpContext.User.Identity.IsAuthenticated) usr = HttpContext.User.Identity.Name;
            string userX = HttpContext.Request.Query["user"];
            string GrpNameX = "room";// HttpContext.Request.Query["Group"];

            if (usr == "Unknow User" && !string.IsNullOrEmpty(userX) && userX != "undefined" && !userX.Equals("null"))
            {
                usr = userX;
            }
            //   Imyhub.Clients.All.SendAsync("RecOnline", usr, DateTime.Now.ToString()).Wait();
            var GrpName = "A";


            if (GrpName == "A" && !string.IsNullOrEmpty(GrpNameX) && GrpNameX != "undefined" && !GrpNameX.Equals("null"))
            {
                GrpName = GrpNameX;
            }
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                usr = HttpContext.User.Identity.Name;
                var currUser = mydb.MyUsers.FirstOrDefault(x => x.UserName == usr);
                if (currUser != null)
                {

                }

            }

            var res = new List<Message>();
            //Imyhub.Clients.All.SendToRecOnline( DateTime.Now.ToString()).Wait();
            // Imyhub.Clients.All.Send("");
            try
            {
                if (message.Count() != 0)
                {

                    var msg = message.Where(x => x.GroupName == GrpName);
                    if (msg.Count() != 0) res = msg.ToList();

                }
            }
            catch
            {

            }

            ViewData["UserName"] = usr;
            ViewData["GrpName"] = GrpName;

            if (res == null)
            {
                res = new List<Message>();
            }

            return View(res);
        }

        public IActionResult MyHubX(string groupName = "GroupMorsal", string userName = "ageeb")
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
