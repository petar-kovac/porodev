using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public FileReadRequestServiceToDatabase()
        {

        }

        public FileReadRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}
