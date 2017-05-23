using System.Net;

namespace TaskBoard.Server.Exceptions {
	public class HttpStopServerException : HttpException {
		public HttpStopServerException(HttpStatusCode statusCode, string message) : base(statusCode, message) {
		}
	}
}