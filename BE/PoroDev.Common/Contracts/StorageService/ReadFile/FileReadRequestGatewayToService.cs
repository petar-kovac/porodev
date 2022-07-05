using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public FileReadRequestGatewayToService()
        {

        }

        public FileReadRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}
