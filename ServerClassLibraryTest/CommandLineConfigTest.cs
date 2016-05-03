using System;
using ServerClassLibrary;
using Xunit;
using System.Diagnostics;

namespace ServerClassLibraryTest {
    public class CommandLineConfigTest {

        [Fact]
        public void TestArgsProvided() {
            var args = new[] {"-p", "7000", "-d", "/this/directory", "-l", "./logfile.txt"};
            var config = new CommandLineConfig(args);

            Assert.Equal(config.Port, 7000);
            Assert.Equal(config.PublicDir, "/this/directory");
            Assert.Equal(config.LogFile, "./logfile.txt");
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

