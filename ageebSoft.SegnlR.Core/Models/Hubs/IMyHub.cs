using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ageebSoft.SignlR.Core.Models.Hubs
{
    public interface IMyHub//<T> where T : MyHub
    {
        //IHubCallerClients Clients { get; set; }
        //HubCallerContext Context { get; set; }
        //IGroupManager Groups { get; set; }

       
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception exce);
       
        Task SendToUser(string user, string message);


        IQueryable<string> GetGroupsOnline();

        IQueryable<string> GetUsersOnline();
         
        Task Send(string msg);
        // Task Notify(string name, string msg)
        Task Notify(string msg);
        Task SendGroup(string GroupName, string msg);
        Task JoinGroup(string GroupName);
        Task RemoveFromGroup(string groupName);


         Task SendToRecOnline(string msg);

         Task SendToRecgrp(string msg);

       
         Task SendToRec(string msg);
        
        //Task OnConnectedAsync();

      //Task OnDisconnectedAsync(Exception exception);
        string GetConnectionId();
        string GetUserName();


    }
}
