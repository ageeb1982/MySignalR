using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ageebSoft.SignlR.Core.Models
{
    public class HubDetail
            {
         
        public ICollection<IdName> Groups { set; get; }
        public ICollection<IdName> Users { set; get; }
    }
}
