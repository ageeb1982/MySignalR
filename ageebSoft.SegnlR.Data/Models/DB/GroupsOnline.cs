using System.Collections.Generic;
using ageebSoft.SignlR.Web.Models.DB;

namespace ageebSoft.SignlR.Web.Models.data
{
    public class GroupsOnline : MainClass
    {
        public string GroupName { set; get; }
        public ICollection<UsersGroupsOnline> UsersGroupsOnlines { set; get; }



    }
}
