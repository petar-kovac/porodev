using PoroDev.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.Update
{
    public class UserUpdateRequestServiceToDatabase
    {
        public Guid Id;
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public UserEnums.UserRole Role { get; set; }

        public UserEnums.UserDepartment Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
