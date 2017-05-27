using System.Data.Entity;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using TaskBoard.Server.Database.Models;

namespace TaskBoard.Server.Database {
	public class NinjectDatabaseModule : NinjectModule {
		public override void Load() {
			System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ModelDatabase>());

			Kernel?.Bind<ModelDatabase>().ToSelf().InSingletonScope();
			Kernel?.Bind(c => c.FromThisAssembly().SelectAllClasses().BindAllInterfaces().Configure(y => y.InSingletonScope()));
		}
	}
}