using Microsoft.AspNetCore.SignalR;

namespace ageebSoft.SignlR.Core
{
    internal class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}