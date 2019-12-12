using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ageebSoft.SignlR.Core.Models.DB
{
    public class MyUser: MainClass
    {
        public string UserName { set; get; }
        public ICollection<Message> Messages { set; get; }
    }
}
