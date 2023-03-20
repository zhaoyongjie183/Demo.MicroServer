using Demo.MicroService.Core.Utils;
using Xunit;

namespace XUnitTestDemoMicroService
{
    public class JsonTest
    {
        [Fact]
        public void Test1()
        {
            var a=new { name="ssss",age=18};
            var c=JsonHelper.ObjectToJSON(a);
            Console.WriteLine( c);
            Assert.NotNull(c);
        }
    }
}