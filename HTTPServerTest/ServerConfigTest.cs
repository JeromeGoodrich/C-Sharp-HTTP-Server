using System;
using Xunit;
using HTTPServer;

namespace HTTPServerTest
{
    
    public class ServerConfigTest {
        [Fact]
        public void TestDefaults() {
            var config = new ServerConfig();
            var args = new[] { "" };
            config.SetUp(args);
            Assert.Equal(config.GetPort(), 5000);
            Assert.Equal(config.GetPublicDir(), "./public");
        }
        [Fact]
        public void TestArgsProvided() {
            var config = new ServerConfig();
            var args = new[] {"-p", "7000", "-d", "/this/directory"};
            config.SetUp(args);
            Assert.Equal(config.GetPort(), 7000);
            Assert.Equal(config.GetPublicDir(), "/this/directory");
        }
        [Fact]
        public void TestThrowsErrorForIncorrectArgs() {
            var config = new ServerConfig();
            var args = new[] {"-p", "$PORT", "-d", "/this/directory"};
            var exception = Record.Exception(() => config.SetUp(args));
            Assert.NotNull(exception);
            Assert.IsType<FormatException>(exception);
        }
    }
}
