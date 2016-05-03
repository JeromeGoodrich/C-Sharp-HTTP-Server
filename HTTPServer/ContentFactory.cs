using System;

namespace CobSpecServer
{
    public class ContentFactory
    {
        public IContent Build(string acceptType, string dirName){
            switch (acceptType) {
                case "application/json":
                    return new JSONContent("application/json", dirName);
                default:
                    return new HTMLContent("text/html", dirName);
            }

        }
    }
}