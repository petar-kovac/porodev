using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.UserModels.RegisterUser
{
    public class RegisterUserRequestGatewayToService
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public RegisterUserRequestGatewayToService()
        {

        }

        public RegisterUserRequestGatewayToService(string name, string lastname, string email, string password, int department, string position, string avatarUrl)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Password = password;
            Department = department;
            Position = position;
            AvatarUrl = avatarUrl;
        }
    }
}
