using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ageebSoft.SignlR.Core.Models.data;
using ageebSoft.SignlR.Core.Models.DB;
using Microsoft.AspNetCore.Identity;

namespace ageebSoft.SignlR.Core.Models
{
    public class NotificationHub : MyHub
    {
        public NotificationHub(MyDB _mydb, UserManager<MyUser> _userManger) : base(_mydb, _userManger)
        {
        }
    }
}
