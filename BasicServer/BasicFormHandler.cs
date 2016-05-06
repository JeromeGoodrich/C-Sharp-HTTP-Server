using ServerClassLibrary;
using System.Text;

namespace BasicServer {

   public class BasicFormHandler : IHandler {

        private static string _formData;

        public IResponse Handle(Request request) {
            if (request.Method.Equals("GET")) {
                return HandleGet(request);
            } else {
                return HandlePost(request);
            }
        }

    private Response HandleGet(Request request) {
        string HTMLBoilerPlate = "<!DOCTYPE html>\n<html>\n<header>\n</header>\n<body>\n";
        string openFormTag = "<form method=\"post\">";
        string field = "First Name:\n<input type=\"text\" name=\"name\">\n";
        string submit = "<input type=\"submit\" value=\"Submit\">\n</form>\n";
        string endboilderplate = "</body>\n</html>";
        string htmlContent;
        if (_formData != null) {
            htmlContent =  HTMLBoilerPlate + openFormTag + field  + submit + _formData.Split('=')[1] + endboilderplate;
        } else {
            htmlContent = HTMLBoilerPlate + openFormTag + field  + submit;
        }
        byte[] data = Encoding.UTF8.GetBytes(htmlContent);
        return new Response(200, request.Version) { Body = data };
    }

    private Response HandlePost(Request request) {
        _formData = request.Body;
        return new Response(200, request.Version);
    }

    }
}
