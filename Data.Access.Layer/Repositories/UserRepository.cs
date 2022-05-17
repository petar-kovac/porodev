using Data.Access.Layer.Data;
using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository 
    {
        private readonly SqlDataContext _context;
        //a
        public UserRepository(SqlDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
