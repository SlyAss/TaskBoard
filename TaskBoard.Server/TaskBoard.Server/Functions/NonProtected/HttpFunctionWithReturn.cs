using System.Net;
using TaskBoard.Common.Extensions;
using TaskBoard.Server.AdditionalObjects;
using TaskBoard.Server.Extensions;

namespace TaskBoard.Server.Functions.NonProtected {
	public abstract class HttpFunctionWithReturn<TResult> : IHttpFunction {
		public abstract string NameOfCalledMethod { get; }

		public void Execute(HttpListenerContext context, NameValues parameters, byte[] requestBody) {
			var outputBytes = Run(parameters, requestBody).ToJson();
			context.Response.Respond(HttpStatusCode.OK, outputBytes);
		}
		protected abstract TResult Run(NameValues parameters, byte[] requestBody);
	}
}