using System.Net;
using TaskBoard.Common.Database;
using TaskBoard.Common.Extensions;
using TaskBoard.Server.AdditionalObjects;
using TaskBoard.Server.Extensions;

namespace TaskBoard.Server.Functions.Protected {
	public abstract class HttpProtectedFunctionWithReturn<TResult> : HttpProtectedFunction {
		protected HttpProtectedFunctionWithReturn(IDatabaseAuthorizer databaseAuthorizer) : base(databaseAuthorizer) {
		}

		protected sealed override void PerformRun(HttpListenerContext context, NameValues parameters, byte[] requestBody) {
			var outputBytes = Run(parameters, requestBody).ToJson();
			context.Response.Respond(HttpStatusCode.OK, outputBytes);
		}
		protected abstract TResult Run(NameValues parameters, byte[] requestBody);
	}
}