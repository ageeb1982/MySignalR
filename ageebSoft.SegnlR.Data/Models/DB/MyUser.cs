using System.Collections.Generic;
using ageebSoft.SignlR.Web.Models.data;

namespace ageebSoft.SignlR.Web.Models.DB
{
    public class MyUser : MainClass
    {
        public string UserName { set; get; }

        public ICollection<UsersGroupsOnline> UsersGroupsOnlines { set; get; }
        public ICollection<Message> Messages { set; get; }
    }
}
