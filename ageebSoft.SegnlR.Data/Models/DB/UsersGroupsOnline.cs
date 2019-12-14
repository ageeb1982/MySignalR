using System;
using System.ComponentModel.DataAnnotations.Schema;
using ageebSoft.SignlR.Web.Models.DB;

namespace ageebSoft.SignlR.Web.Models.data
{
    public class UsersGroupsOnline : MainClass
    {
        public Guid GroupsOnlineId { set; get; }
        [ForeignKey(nameof(GroupsOnlineId))]
        public GroupsOnline GroupsOnline { set; get; }


        public Guid MyUserId { set; get; }
        [ForeignKey(nameof(MyUserId))]
        public MyUser MyUser { set; get; }

    }
}
