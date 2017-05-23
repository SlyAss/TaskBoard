using System.Net;
using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions {
	public interface IHttpFunction {
		string NameOfCalledMethod { get; }
		void Execute(HttpListenerContext context, NameValues parameters, byte[] requestBody);
	}
}