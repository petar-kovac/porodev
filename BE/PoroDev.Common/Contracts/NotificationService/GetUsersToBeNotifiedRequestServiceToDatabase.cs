using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.NotificationService
{
    public class GetUsersToBeNotifiedRequestServiceToDatabase
    {
        public int Hour { get; set; }
        public int Day { get; set; }
    }
}
