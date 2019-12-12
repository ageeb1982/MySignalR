using ageebSoft.SignlR.Core.Models.data;
using ageebSoft.SignlR.Core.Models.DB;
using ageebSoft.SignlR.Core.Models.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ageebSoft.SignlR.Core.Models
{


    //ToDo:مراجعة الدخول بالجافا اسكربت في صفحات في مشروع adminx
    //ToDo:مراجعة الدخول بالجافا في صفحات مشروع datalayer
    public class MyHub : Hub,IMyHub
    {
        private readonly MyDB mydb;
        private readonly UserManager<MyUser> userManger;

        public MyHub(MyDB _mydb, UserManager<MyUser> _userManger)
        {
            mydb = _mydb;
            userManger = _userManger;
        }
        //public static string ConnectionString= "workstation id=MORSALdb.mssql.somee.com;packet size=4096;user id=Ageeb_SQLLogin_1;pwd=9wiqgiwu4u;data source=MORSALdb.mssql.somee.com;persist security info=False;initial catalog=MORSALdb";
        public static string ConnectionString = "Data Source=.;Initial Catalog=SignalRDB;Integrated Security=True";
        public Task SendToUser(string user, string message)
        {
            return Clients.User(user).SendAsync("Rec", message);
        }


        public IQueryable<string> GetGroupsOnline()
        {
            return mydb.GroupsOnline.Select(x => x.Name);
        }

        public IQueryable<string> GetUsersOnline()
        {
            return mydb.UsersOnline.Select(x => x.Name);
        }

        [Authorize(Roles = "ChatRole")]
        //public async Task Send(string name, string msg)
        public async Task Send(string msg)
        {
            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);
        }

        // public async Task Notify(string name, string msg)
        public async Task Notify(string msg)
        {
            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);
        }

        //public async Task SendGroup(string GroupName,string name, string msg)
        [Authorize(Roles = "ChatRole")]
        public async Task SendGroup(string GroupName, string msg)
        {
            await Clients.Group(GroupName).SendAsync("Recgrp", $"{GetUserName()}", msg);
        }

        [Authorize(Roles = "Admin")]
        public async Task JoinGroup(string GroupName)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);


                Clients.Group(GroupName).SendAsync("RecOnline", $"{GetUserName()}", "Join Group : " + GroupName).Wait();
            }
            //Clients.Group(GroupName).SendAsync("RecOnline", $"{Context.ConnectionId}", "Join Group : " + GroupName).Wait();
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


        [Authorize]
        public async Task SendToRecOnline(string msg)
        {
            await Clients.All.SendAsync("RecOnline", $"{GetUserName()}", msg);

        }
        [Authorize]
        public async Task SendToRecgrp(string msg)
        {
            await Clients.All.SendAsync("Recgrp", $"{GetUserName()}", msg);

        }

        [Authorize]
        public async Task SendToRec(string msg)
        {
            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);

        }
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("RecOnline", $"{GetUserName()}", "Connected").Wait();
            // Clients.All.SendAsync("RecOnline", $"{Context.ConnectionId}", "Connected").Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("RecOnline", $"{GetUserName()}", "DisConnected").Wait();
            // Clients.All.SendAsync("RecOnline", $"{Context.ConnectionId}", "Connected").Wait();

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


    }
}
