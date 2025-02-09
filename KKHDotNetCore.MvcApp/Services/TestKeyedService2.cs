namespace KKHDotNetCore.MvcApp.Services
{
    public class TestKeyedService2 : ITestKeyedService
    {
        string ITestKeyedService.Print()
        {
            return "I am Keyed Service 2";
        }
    }
}
