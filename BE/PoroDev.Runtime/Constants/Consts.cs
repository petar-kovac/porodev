using System;

namespace PoroDev.Runtime.Constants
{
    public static class Consts
    {
        public static readonly string RUNTIME_FOLDER_ROUTE = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "Runtime");
    }
}
