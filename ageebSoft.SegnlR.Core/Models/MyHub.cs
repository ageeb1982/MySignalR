using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ageebSoft.SignlR.Core.Models
{
    public class MyHub:Hub
    {
        public Task SendUser(string user, string message)
        {
            return Clients.User(user).SendAsync("Rec", message);
        }


        public async Task Send(string name, string msg)
        {
          await  Clients.All.SendAsync("Rec",name,msg);
        }  
        
        public async Task Notify(string name, string msg)
        {
          await  Clients.All.SendAsync("Rec",name,msg);
        }

 public async Task SendGroup(string GroupName,string name, string msg)
        {
          await  Clients.Group(GroupName).SendAsync("Recgrp",name,msg);
        }

        
        public async Task JoinGroup(string GroupName)
        {

          await  Groups.AddToGroupAsync(Context.ConnectionId,GroupName);

            Clients.Group(GroupName).SendAsync("RecOnline", $"{Context.ConnectionId}", "Join Group : " + GroupName).Wait();
        }
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }


        public override  Task OnConnectedAsync()
        {
             Clients.All.SendAsync("RecOnline", $"{Context.ConnectionId}", "Connected").Wait();

            return base.OnConnectedAsync();
        }

        public override  Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("RecOnline", Context.ConnectionId, "DisConnected").Wait();

            return base.OnDisconnectedAsync(exception);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
