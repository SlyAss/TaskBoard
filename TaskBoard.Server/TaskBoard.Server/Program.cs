using System;
using Ninject;
using Ninject.Extensions.Conventions;
using TaskBoard.Server.Database;
using TaskBoard.Server.Server;

namespace TaskBoard.Server {
	public static class Program {
		public static void Main() {
			try {
				RunServer();
			}
			catch (Exception) {
				// ignored
			}
		}
		public static void RunServer() {
			CreateHttpServer().Run(ServerConfiguration.ReadConfiguration().ServerAddress);
		}
		private static IHttpServer CreateHttpServer() {
			var container = new StandardKernel(new NinjectDatabaseModule());

			container.Bind(c => c.FromThisAssembly().SelectAllClasses().BindAllInterfaces());
			container.Bind(c => c.FromThisAssembly().SelectAllClasses().BindAllBaseClasses());

			return container.Get<IHttpServer>();
		}
	}
}