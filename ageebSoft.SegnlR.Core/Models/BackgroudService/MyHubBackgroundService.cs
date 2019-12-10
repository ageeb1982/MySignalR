//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ageebSoft.SignlR.Core.Models
//{
//    /// <summary>
//    /// لشتغيلها على الخلفية
//    /// </summary>
//    public class MyHubBackgroundService : IHostedService, IDisposable
//    {
//        public static MyHub MyHubContext;

//        public MyHubBackgroundService(IHubContext<MyHub> hubContext)
//        {
//            MyHubContext =(MyHub) hubContext;
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            //TODO: your start logic, some timers, singletons, etc
//            return Task.CompletedTask;
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            //TODO: your stop logic
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//        }
//    }

//    //
//   // BackgroundService.HubContext.Clients.All.SendAsync("UpdateData", myData).Wait();

//}

