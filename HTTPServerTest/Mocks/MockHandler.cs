using System;
using System.Collections.Generic;
using HTTPServer;

namespace HTTPServerTest.Mocks {

    internal class MockHandler : IHandler {
        private readonly MockResponse _mockResponse;
        private int _callsToHandle;
        private Request _request;

        public MockHandler(MockResponse mockResponse) { 
            _mockResponse = mockResponse;
        }

        public int GetCallsToHandle() {
            return _callsToHandle;
        }
    
        public IResponse Handle(Request request) {
            _request = request;
            _callsToHandle++;
            return _mockResponse;
        }

        public Request GetLastRequestPassedToHandle() {
            return _request;
        }
    }
}