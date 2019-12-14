using Microsoft.AspNetCore.Mvc;

namespace ageebSoft.SignalR.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class msgController : ControllerBase
    {
        public class msg
        {
            public string Name { set; get; }
        }
        public string Get(string name)
        {
            return $"Hello {name}";
        }

        public string Post(msg msg)
        {
            return $"Hello {msg.Name}";
        }
    }
}