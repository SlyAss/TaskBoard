using System;
using System.Net;
using TaskBoard.Common.Extensions;

namespace TaskBoard.Server.Extensions {
	public static class HttpListenerResponseExtensions {
		public static void Respond(this HttpListenerResponse response, HttpStatusCode statusCode, byte[] bytes) {
			try {
				response.StatusCode = (int)statusCode;
				response.OutputStream.WriteAndDispose(bytes);
			}
			catch (Exception) {
				// ignored
			}
		}
	}
}