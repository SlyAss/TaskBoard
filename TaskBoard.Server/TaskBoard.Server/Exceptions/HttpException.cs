using System;
using System.Net;

namespace TaskBoard.Server.Exceptions {
	public class HttpException : Exception {
		public readonly HttpStatusCode StatusCode;

		public HttpException(HttpStatusCode statusCode, string message) : base(message) {
			StatusCode = statusCode;
		}
	}
}