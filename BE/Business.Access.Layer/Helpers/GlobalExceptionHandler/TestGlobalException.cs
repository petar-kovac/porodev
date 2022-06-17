namespace Business.Access.Layer.Helpers.GlobalExceptionHandler
{
    public static class TestGlobalException
    {
        public static void TestException(string errorMessage)
        {
            throw new AppException("Business layer exception.");
        }
    }
}