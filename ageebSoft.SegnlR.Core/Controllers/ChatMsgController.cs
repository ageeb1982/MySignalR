//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ageebSoft.SignlR.Core.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;

//namespace ageebSoft.SignlR.Core.Controllers
//{
//    public class ChatController : Controller
//    {
//        public IHubContext<MyHub> myhub { get; }

//        public ChatController(IHubContext<ChatHub, IChatClient> chatHubContext)
//        {
//            _strongChatHubContext = chatHubContext;
//        }

//        public async Task SendMessage(string message)
//        {
//            await _strongChatHubContext.Clients.All.ReceiveMessage(message);
//        }
//    }
     
//}