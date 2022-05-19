using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Helpers.GlobalExceptionHandler
{
    public static class TestGlobalException
    {
        public static void TestException(string errorMessage)
        {
            throw new AppException("Data layer exception.");
        }
    }
}
