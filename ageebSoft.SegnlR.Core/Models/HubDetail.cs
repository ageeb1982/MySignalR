using System.Collections.Generic;
using ageebSoft.SignlR.Web.Models;

namespace ageebSoft.SignlR.Core.Models
{
    public class HubDetail
    {

        public ICollection<IdName> Groups { set; get; }
        public ICollection<IdName> Users { set; get; }
    }
}
