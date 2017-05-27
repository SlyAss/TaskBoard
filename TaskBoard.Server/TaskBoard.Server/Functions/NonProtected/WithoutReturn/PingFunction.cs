using TaskBoard.Server.AdditionalObjects;

namespace TaskBoard.Server.Functions.NonProtected.WithoutReturn {
	// ReSharper disable UnusedMember.Global

	public class PingFunction : HttpFunctionWithoutReturn {
		public override string NameOfCalledMethod => "Ping";

		protected override void Run(NameValues parameters, byte[] requestBody) {
		}
	}
}