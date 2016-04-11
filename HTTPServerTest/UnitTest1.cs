using System;
using Xunit;

namespace HTTPServerTest
{
    
    public class ServerConfigTest {
        [Fact]
        public void TestDefaults() {
            var config = new ServerConfig();
            config.SetUp("");
            Assert.Equal(config.GetPort(), 5000);
            Assert.Equal(config.GetPublicDir(), "./public");
        }
    }
}
