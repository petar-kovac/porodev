using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService
{
    public interface IUpdateFileExe
    {
        string FileName { get; set; }

        Guid? UserId { get; set; }

        bool IsExe { get; set; }
    }
}
