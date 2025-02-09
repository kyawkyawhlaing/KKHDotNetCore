namespace KKHDotNetCore.MvcApp.Services
{
    public class TestKeyedService1 : ITestKeyedService
    {
        string ITestKeyedService.Print()
        {
            return "I am Keyed Service 1";
        }
    }
}
