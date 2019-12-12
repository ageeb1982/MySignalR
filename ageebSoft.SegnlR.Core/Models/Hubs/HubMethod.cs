//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ageebSoft.SignlR.Core.Models.data;
//using ageebSoft.SignlR.Core.Models.DB;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.SignalR;

//namespace ageebSoft.SignlR.Core.Models.Hubs
//{
//    public  class MyHubMethods:MyHub
//    {
//        private readonly IHubContext<MyHub> _hubContext;
//        private readonly MyDB mydb;
//        private readonly UserManager<MyUser> userManger;
//        public MyHubMethods(IHubContext<MyHub> hubContext, MyDB _mydb, UserManager<MyUser> _userManger) :base(_mydb, _userManger)
//        {
//            _hubContext = hubContext;
//            mydb = _mydb;
//            userManger = _userManger;
//        }

//         public Task SendToUser(string user, string message)
//        {
//            _hubContext.Clients.All.SendAsync("");
//          //  return _hubContext.Clients.        
//        }


//        public IQueryable<string> GetGroupsOnline()
//        {
//            return mydb.GroupsOnline.Select(x => x.Name);
//        }

//        public IQueryable<string> GetUsersOnline()
//        {
//            return mydb.UsersOnline.Select(x => x.Name);
//        }

//        [Authorize(Roles = "ChatRole")]
//        //public async Task Send(string name, string msg)
//        public async Task Send(string msg)
//        {
//            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);
//        }

//        // public async Task Notify(string name, string msg)
//        public async Task Notify(string msg)
//        {
//            await Clients.All.SendAsync("Rec", $"{GetUserName()}", msg);
//        }

//        //public async Task SendGroup(string GroupName,string name, string msg)
//        [Authorize(Roles = "ChatRole")]
//        public async Task SendGroup(string GroupName, string msg)
//        {
//            await Clients.Group(GroupName).SendAsync("Recgrp", $"{GetUserName()}", msg);
//        }

//        [Authorize(Roles = "Admin")]
//        public async Task JoinGroup(string GroupName)
//        {
//            if (Context.User.Identity.IsAuthenticated)
//            {
//                await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);


//                Clients.Group(GroupName).SendAsync("RecOnline", $"{GetUserName()}", "Join Group : " + GroupName).Wait();
//            }
//            //Clients.Group(GroupName).SendAsync("RecOnline", $"{Context.ConnectionId}", "Join Group : " + GroupName).Wait();
//        }
//        [Authorize(Roles = "Admin")]
//        public async Task RemoveFromGroup(string groupName)
//        {
//            if (Context.User.Identity.IsAuthenticated)
//            {
//                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

//                // await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
//                await Clients.Group(groupName).SendAsync("Send", $"{GetUserName()} has left the group {groupName}.");
//            }
//        }
//}
