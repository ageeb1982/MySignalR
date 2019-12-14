using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ageebSoft.SignlR.Core.Models.data;

namespace ageebSoft.SignlR.Core.Models
{
    public class NotificationHub : MyHub
    {
        public NotificationHub(MyDB mydbX):base(mydbX)
        {

        }
    }
}
