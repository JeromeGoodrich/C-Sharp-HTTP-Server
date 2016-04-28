using System;
using System.IO;
using HTTPServer;
using Xunit;

namespace HTTPServerTest {
    public class ServerConfigTest {
        [Fact]
        public void TestWithoutCommandLineArgs() {
            var args = new[] {""};
            var config = new ServerConfig(args);

            Assert.Equal(5000, config.Port);
            Assert.Equal(Path.Combine(Environment.CurrentDirectory, @"..\..\..\HTTPServerTest\Fixtures\"),
                config.PublicDir);
        }

        [Fact]
        public void TestArgsProvided() {
            var args = new[] {"-p", "7000", "-d", "/this/directory"};
            var config = new ServerConfig(args);

            Assert.Equal(config.Port, 7000);
            Assert.Equal(config.PublicDir, "/this/directory");
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
            var args = new[] {""};
            var config = new ServerConfig(args);

            Assert.Equal(config.IpAddress.ToString(), "0.0.0.0");
        }
    }
}