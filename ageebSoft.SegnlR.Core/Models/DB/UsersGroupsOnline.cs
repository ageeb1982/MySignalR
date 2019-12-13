using System;
using System.ComponentModel.DataAnnotations.Schema;
using ageebSoft.SignlR.Core.Models.DB;

namespace ageebSoft.SignlR.Core.Models.data
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
