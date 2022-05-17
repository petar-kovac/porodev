using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();

    }
}
