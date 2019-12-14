using ageebSoft.SignlR.Web.Models.data;

namespace ageebSoft.SignlR.Core.Models
{
    public class NotificationHub : MyHub
    {
        public NotificationHub(MyDB mydbX) : base(mydbX)
        {

        }
    }
}
