﻿using System.Threading;
using HTTPServer;
using HTTPServerTest.Mocks;
using Xunit;

namespace HTTPServerTest {
    public class ServerTest {
        private readonly MockService _mockService;
        private readonly MockSocket _mockSocket;
        private readonly Server _server;
        private CancellationTokenSource _cancellationSource;

        public ServerTest() {
            _cancellationSource = new CancellationTokenSource();
            _mockSocket = new MockSocket();
            var mockListener = new MockListener(_mockSocket);
            _mockService = new MockService();
            var mockServiceFactory = new MockServiceFactory(_mockService);
            _server = new Server(mockListener, mockServiceFactory);
        }

        [Fact]
        public void TestServiceIsNotRunningBeforeStartingServer() {
            Assert.Equal(_mockService.IsRunning(), false);
        }

        [Fact]
        public void TestServiceIsRunningAfterStartingServer() {
            _server.Start(_cancellationSource.Token);
            Assert.Equal(_mockService.IsRunning(), true);
        }

        [Fact]
        public void TestServiceIsPassedSocket() {
            _server.Start(_cancellationSource.Token);
            Assert.Equal(_mockService.Socket, _mockSocket);
        }
    }
}