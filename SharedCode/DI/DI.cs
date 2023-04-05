using System;
using SharedCode.Controller;
using SharedCode.Repository;
using SharedCode.Repository.DB;
using SharedCode.Repository.Interfaces;
using SharedCode.Services;
using SharedCode.Util;
using Unity;
using Unity.Lifetime;
using SharedCode.Database;

namespace SharedCode.DI
{
	public static class DI
	{
		public static void RegisterDIs()
		{
			var container = IocContainer.Instance;

			// Network Handler
			// Singleton
			container.RegisterType<INetworkHandler, NetworkHandler>(new ContainerControlledLifetimeManager());

			// Pokemon List
			container.RegisterType<IPokemonRepository, PokemonRepository>();
			container.RegisterType<IPokemonController, PokemonController>();
			container.RegisterType<IPokemonDetailController, PokemonDetailController>();

			// Managers
			container.RegisterType<IDatabaseManager, DatabaseManager>();

			// DB
			container.RegisterType<IPokemonRepositoryLocal, PokemonRepositoryLocal>();
		}
    }
}
	
