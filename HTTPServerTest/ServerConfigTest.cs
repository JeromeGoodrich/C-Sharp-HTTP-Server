using System;
using System.IO;
using Xunit;
using HTTPServer;

namespace HTTPServerTest {
    
    public class ServerConfigTest {
        [Fact]
        public void TestDefaults() {
            var args = new[] { "" };
            var config = new ServerConfig(args);
            
            Assert.Equal(config.GetPort(), 5000);
            Assert.Equal(config.GetPublicDir(), Path.Combine(Environment.CurrentDirectory, @"..\..\Fixtures\"));
        }
        [Fact]
        public void TestArgsProvided() {
            var args = new[] { "-p", "7000", "-d", "/this/directory" };
            var config = new ServerConfig(args);
            Assert.Equal(config.GetPort(), 7000);
            Assert.Equal(config.GetPublicDir(), "/this/directory");
        }
        [Fact]
        public void TestThrowsErrorForIncorrectArgs() {
            var args = new[] {"-p", "$PORT", "-d", "/this/directory"};
            var exception = Record.Exception(() => new ServerConfig(args));

            Assert.NotNull(exception);
            Assert.IsType<FormatException>(exception);
        }

        [Fact]
        public void TestIpAddress() {
            var args = new[] { "" };
            var config = new ServerConfig(args);

            Assert.Equal(config.GetIpAddress().ToString(), "172.16.11.128");
        }
    }
}
