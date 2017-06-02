using System.Net;
using TaskBoard.Server.AdditionalObjects;
using TaskBoard.Server.Extensions;

namespace TaskBoard.Server.Functions.NonProtected {
	// ReSharper disable UnusedParameter.Global

	public abstract class HttpFunctionWithoutReturn : IHttpFunction {
		public abstract string NameOfCalledMethod { get; }

		public void Execute(HttpListenerContext context, NameValues parameters, byte[] requestBody) {
			Run(parameters, requestBody);
			context.Response.Respond(HttpStatusCode.OK, null);
		}
		protected abstract void Run(NameValues parameters, byte[] requestBody);
	}
}