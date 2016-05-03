using System;
using ServerClassLibrary;
using Xunit;

namespace ServerClassLibraryTest {
    public class CommandLineConfigTest {

        [Fact]
        public void TestWithoutCommandLineArgs() {
            var args = new[] {""};
            var config = new CommandLineConfig(args);

            Assert.True(false);
        }

        [Fact]
        public void TestArgsProvided() {
            var args = new[] {"-p", "7000", "-d", "/this/directory"};
            var config = new CommandLineConfig(args);

            Assert.Equal(config.Port, 7000);
            Assert.Equal(config.PublicDir, "/this/directory");
        }

        [Fact]
        public void TestThrowsErrorForIncorrectArgs() {
            var args = new[] {"-p", "$PORT", "-d", "/this/directory"};
            var exception = Record.Exception(() => new CommandLineConfig(args));

            Assert.NotNull(exception);
            Assert.IsType<FormatException>(exception);
        }

        [Fact]
        public void TestIpAddress() {
            var args = new[] {""};
            var config = new CommandLineConfig(args);

            Assert.Equal(config.IpAddress.ToString(), "0.0.0.0");
        }
    }
}

