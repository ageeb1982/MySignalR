﻿//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ageebSoft.SignlR.Core.Models.BackgroudService
//{
    
//    public class BGServiceStarter<T> : IHostedService
//    where T : IHostedService
//    {
//        private readonly T backgroundService;

//        public BGServiceStarter(T backgroundService)
//        {
//            this.backgroundService = backgroundService;
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            return backgroundService.StartAsync(cancellationToken);
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            return backgroundService.StopAsync(cancellationToken);
//        }
//    }
//}
