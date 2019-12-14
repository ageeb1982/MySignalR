using System;
using System.Linq;
using System.Threading.Tasks;
using ageebSoft.SignlR.Core.Models.data;
using ageebSoft.SignlR.Core.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ageebSoft.SignlR.Core.Models
{


    //ToDo:مراجعة الدخول بالجافا اسكربت في صفحات في مشروع adminx
    //ToDo:مراجعة الدخول بالجافا في صفحات مشروع datalayer
    [Authorize(Roles = "ChatRole")]
    public class MyHub : Hub
    {
        MyDB mydb;
        public MyHub(MyDB myDB)
        {
            mydb = myDB;
        }

        #region ToAllUser
         public async Task Send(string msg)
        {
            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);
            await getGroupOnline();
        }

        public async Task SendToRec(string msg)
        {
            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);
            await getGroupOnline();
        }

        public async Task SendToRecOnline(string msg)
        {
            await Clients.All.SendAsync("RecOnline", $"{GetUserName()}", msg);
            await getGroupOnline();
        }


        public async Task Notify(string msg)
        {
            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);
            await getGroupOnline();
        }


        #endregion

        #region GroupSection
         public async Task SendGroup(string GroupName, string msg)
        {
            await Clients.Group(GroupName).SendAsync("Recgrp", $"{GetUserName()}", msg);
            await getGroupOnline();
        }
        [Authorize(Roles = "Admin")]
        public async Task JoinGroup(string GroupName)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                try
                {
                    OpenMydbIfClose();
                    var group = IsGroupExistOrCrate(GroupName);
                    var userName = Context.User.Identity.Name;
                    var User = IsUserExistOrCrate(userName);
                    UsersGroupsOnline groupUser = IsGoupsUsersExistOrCreate(group, User);

                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
                Clients.Group(GroupName).SendAsync("RecOnline", $"{GetUserName()}", "Join Group : " + GroupName).Wait();
                await getGroupOnline();
            }
        }

        private async Task getGroupOnline()
        {
            OpenMydbIfClose();
            if (!Context.User.Identity.IsAuthenticated) return;

            var userGrp = mydb.UsersGroupsOnline.Include(x => x.GroupsOnline).Include(y => y.MyUser).OrderBy(w => w.GroupsOnline.GroupName);
            var Groups = await userGrp
                .GroupBy(x => x.GroupsOnline)
                .Select(w => new GroupCount { groupName = w.Key.GroupName, Count = w.Count() }).ToListAsync();



            string sJSON = JsonConvert.SerializeObject(Groups, Formatting.Indented);


            Clients.All.SendAsync("GroupsOnline", sJSON).Wait();
        }

        public async Task OutFromGroups()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = IsUserExistOrCrate();
                if (user == null) return;
                var grp = mydb.UsersGroupsOnline.Where(x => x.MyUserId == user.Id);
                if (grp == null) return;
                if (grp.Count() != 0)
                {
                    mydb.UsersGroupsOnline.RemoveRange(grp);
                    await mydb.SaveChangesAsync();
                }
            }
        }
        private UsersGroupsOnline IsGoupsUsersExistOrCreate(GroupsOnline group, MyUser user)
        {
            OpenMydbIfClose();
            if (group == null || user == null) return null;
            var usersGrps = mydb.UsersGroupsOnline.FirstOrDefault(x => x.GroupsOnlineId == group.Id && x.MyUserId == user.Id);
            if (usersGrps == null)
            {
                usersGrps = new UsersGroupsOnline { GroupsOnlineId = group.Id, MyUserId = user.Id };
                mydb.UsersGroupsOnline.Add(usersGrps);
                mydb.SaveChanges();
            }
            return usersGrps;
        }


        private GroupsOnline IsGroupExistOrCrate(string GroupName)
        {
            OpenMydbIfClose();
            if (string.IsNullOrEmpty(GroupName)) return null;
            var groupx = mydb.GroupsOnline.ToList();
            var group = groupx.FirstOrDefault(x => x.GroupName.Equals(GroupName, StringComparison.OrdinalIgnoreCase));
            if (group == null)
            {
                group = new GroupsOnline { GroupName = GroupName };
                mydb.GroupsOnline.Add(group);
                mydb.SaveChanges();
            }

            return group;
        }

        [Authorize(Roles = "Admin")]
        public async Task RemoveFromGroup(string groupName)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

                // await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
                await Clients.Group(groupName).SendAsync("Send", $"{GetUserName()} has left the group {groupName}.");
            }
        }


        public async Task SendToRecgrp(string msg)
        {
            await Clients.All.SendAsync("Recgrp", $"{GetUserName()}", msg);
            await getGroupOnline();
        }


        #endregion

        #region ToSomeUser
        public async Task SendUser(string user, string message)
        {
            await getGroupOnline();

            await Clients.User(user).SendAsync("Rec", message);
        }


        #endregion

        #region OtherMethod
        private void OpenMydbIfClose()
        {
            if (mydb == null) mydb = new MyDB();
        }

        private MyUser IsUserExistOrCrate(string userName = null)
        {
            if (!Context.User.Identity.IsAuthenticated) return null;

            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(Context.User.Identity.Name)) return null;
            if (string.IsNullOrEmpty(userName)) userName = Context.User.Identity.Name;
            OpenMydbIfClose();

            var currUser = mydb.MyUsers.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (currUser == null)
            {
                currUser = new MyUser { UserName = userName };
                mydb.MyUsers.Add(currUser);
                mydb.SaveChanges();
            }
            return currUser;
        }


        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("RecOnline", $"{GetUserName()}", $"Connected At:{DateTime.Now}").Wait();
            getGroupOnline().Wait();
            // Clients.All.SendAsync("RecOnline", $"{Context.ConnectionId}", "Connected").Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("RecOnline", $"{GetUserName()}", "DisConnected").Wait();
            OutFromGroups().Wait();
            // Clients.All.SendAsync("RecOnline", $"{Context.ConnectionId}", "Connected").Wait();
            getGroupOnline().Wait();
            return base.OnDisconnectedAsync(exception);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        public string GetUserName()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                return Context.User.Identity.Name;
                // Clients.All.SendAsync("RecOnline", $"{Context.ConnectionId}", "Connected").Wait();
            }
            else
            {
                return $"Unknow User";

            }
        }

        #endregion







    }
}
