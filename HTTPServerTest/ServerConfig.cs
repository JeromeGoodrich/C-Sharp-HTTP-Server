using System;

namespace HTTPServerTest
{
    internal class ServerConfig
    { 
        public void SetUp(params string[] args) {
            
        }

        public int GetPort() {
            return 5000;
        }

        public string GetPublicDir() {
            return "./public";
        }
    }
}