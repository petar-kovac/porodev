using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.UserModels.DeleteUser
{
    public class DeleteUserModel
    {
        public bool Deleted { get; set; }
        public DeleteUserModel(bool deleted)
        {
            Deleted = deleted;
        }
        public DeleteUserModel()
        {

        }
    }
}
