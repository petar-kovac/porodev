using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Models
{
    public static class Enums
    {
        public enum UserRole : int
        {
            SuperAdmin = 0,
            User = 1
        }

        public enum UserDepartment : int
        {
            notDefined = 0
        }

        public enum UserPosition : int
        {
            notDefined = 0
        }
    }
}
