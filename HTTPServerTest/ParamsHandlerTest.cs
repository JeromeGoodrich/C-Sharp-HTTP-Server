using System.Text;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ParamsHandlerTest {
        [Fact]
        public void TestResponseBodyContainsExpectedValues() {
            var request = new Request() {
                Method = "GET",
                Path = "/parameters",
                Version = "HTTP/1.1",
            };
            request.AddParameters("variable_1", "Operators <, >, =, !=; +, -, *, &, @, #, $, [, ]: \"is that all\"?");
            request.AddParameters("variable_2", "stuff");
           
            var handler = new ParamsHandler();
            var response = handler.Handle(request);

            Assert.Contains("variable_1 = Operators <, >, =, !=; +, -, *, &, @, #, $, [, ]: \"is that all\"?",
                Encoding.UTF8.GetString(response.Body));
            Assert.Contains("variable_2 = stuff", Encoding.UTF8.GetString(response.Body));

        }
        
    }
}