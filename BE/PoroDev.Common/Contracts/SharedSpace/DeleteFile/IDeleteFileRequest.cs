using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace.DeleteFile
{
    public interface IDeleteFileRequest
    {
        public Guid SpaceId { get; set; }

        public string FileId { get; set; }
    }
}
