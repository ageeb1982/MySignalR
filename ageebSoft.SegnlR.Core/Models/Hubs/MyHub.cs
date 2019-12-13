﻿using ageebSoft.SignlR.Core.Models.data;
using ageebSoft.SignlR.Core.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ageebSoft.SignlR.Core.Models
{


    //ToDo:مراجعة الدخول بالجافا اسكربت في صفحات في مشروع adminx
    //ToDo:مراجعة الدخول بالجافا في صفحات مشروع datalayer
    public class MyHub : Hub
    {



        MyDB mydb;
        public Task SendUser(string user, string message)
        {
            return Clients.User(user).SendAsync("Rec", message);
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
                try
                {
                    mydb = new MyDB();
                    var IsAdd = false;
                    var group = mydb.GroupsOnline.FirstOrDefault(x => x.GroupName.Equals(GroupName, StringComparison.OrdinalIgnoreCase));
                    group = IsGroupExistOrCrate(GroupName, group);
                    var userName = Context.User.Identity.Name;
                   var User= IsUserExistOrCrate(userName);

                    //if (IsAdd) mydb.SaveChanges();

                  //ToDo:  mydb.gr


                }
                catch { }

                await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);


                Clients.Group(GroupName).SendAsync("RecOnline", $"{GetUserName()}", "Join Group : " + GroupName).Wait();
            }
            //Clients.Group(GroupName).SendAsync("RecOnline", $"{Context.ConnectionId}", "Join Group : " + GroupName).Wait();
        }

        private MyUser IsUserExistOrCrate(string userName)
        {
            if (mydb == null) mydb = new MyDB();
            var currUser = mydb.MyUsers.FirstOrDefault(x => x.UserName == userName);
            if (currUser == null)
            {
                currUser = new MyUser { UserName = userName };
                mydb.MyUsers.Add(currUser);
                mydb.SaveChanges();
            }
            return currUser;
        }

        private GroupsOnline IsGroupExistOrCrate(string GroupName, GroupsOnline group)
        {
            if (mydb == null) mydb = new MyDB();
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
