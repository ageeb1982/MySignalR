using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ageebSoft.SignlR.Core.Models.DB;

namespace ageebSoft.SignlR.Core.Models.data
{
    public class GroupsOnline : MainClass
    {
        public string GroupName { set; get; }
        public ICollection<UsersGroupsOnline>  UsersGroupsOnlines { set; get; }
        
        

    }
}
