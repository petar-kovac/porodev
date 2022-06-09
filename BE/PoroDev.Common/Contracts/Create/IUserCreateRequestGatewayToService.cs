using PoroDev.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.Create
{
    public interface IUserCreateRequestGatewayToService
    {
        string AvatarUrl { get; set; }
        UserEnums.UserDepartment Department { get; set; }
        string Email { get; set; }
        string Lastname { get; set; }
        string Name { get; set; }
        string PasswordUnhashed { get; set; }
        string Position { get; set; }
        UserEnums.UserRole Role { get; set; }
    }
}
