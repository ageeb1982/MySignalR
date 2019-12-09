//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Threading.Tasks;

//namespace ageebSoft.SignlR.Core.Models
//{
//    public class NotificationHub : BaseHub
//    {
//        private IFragStateManager _stateManager;
//        private static IHubContext _context;

//        public NotificationHub(IFragStateManager stateManager) : this(stateManager, _context)
//        {
//            _stateManager = stateManager;
//            _context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
//        }

//        private NotificationHub(IFragStateManager stateManager, IHubContext context) : base(stateManager, context)
//        { }

//        /// <summary>
//        /// Broadcasts payload to all clients
//        /// </summary>
//        /// <param name="payload"></param>
//        public void Broadcast(object payload)
//        {
//            try
//            {
//                _context.Clients.All.broadcast(payload);
//            }
//            catch (Exception e)
//            {
//                Log.Error(e.ToString());
//            }
//        }

//        //Investigate more why it's not hitting breakpoint while it actually connects and broadcasts messages
//        public override Task OnConnected()
//        {
//            return base.OnConnected();
//        }

//        public override Task OnDisconnected(bool stopCalled)
//        {
//            return base.OnDisconnected(stopCalled);
//        }
//    }
//    public abstract class BaseHub : Hub
//    {
//        protected IPrincipal User
//        {
//            get { return base.Context.User ?? _user; }
//        }

//        private ClaimsPrincipal _user;
//        private Guid _userId;

//        private IFragStateManager _stateManager;
//        private IHubContext _context;

//        public BaseHub(IFragStateManager stateManager, IHubContext context)
//        {
//            _stateManager = stateManager;
//            _context = context;
//        }

//        public override async Task OnConnected()
//        {
//            GetUserId();

//            var map = await _stateManager.GetOrAddAsync<FragDictionary<Guid, string>>("UserConnectionMap");

//            using (var tx = _stateManager.CreateTransaction())
//            {
//                var exisingValue = await map.TryGetValueAsync(tx, _userId);
//                await map.SetAsync(tx, _userId, Context.ConnectionId);
//                await tx.CommitAsync();
//            }

//            await base.OnConnected();
//        }

//        public override async Task OnDisconnected(bool stopCalled)
//        {
//            var map = await _stateManager.GetOrAddAsync<FragDictionary<Guid, string>>("UserConnectionMap");

//            using (var tx = _stateManager.CreateTransaction())
//            {
//                await map.TryRemoveAsync(tx, _userId);
//                await tx.CommitAsync();
//            }

//            await base.OnDisconnected(stopCalled);
//        }

//        private void GetUserId()
//        {

//            if (Context.User == null)
//                _user = (ClaimsPrincipal)(IPrincipal)Context.Request.Environment["server.User"];
//            else
//                _userId = new Guid(_user.FindFirst("AccountId").Value);
//        }
//    }
//}
