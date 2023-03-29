using System;
using SharedCode.Repository;
using SharedCode.Services;
using SharedCode.Util;
using Unity;
using Unity.Lifetime;

namespace SharedCode.DI
{
	public static class DI
	{
		public static void RegisterDIs()
		{
			var container = IocContainer.GetInstance();

			// Singleton
			container.RegisterType<INetworkHandler, NetworkHandler>(new ContainerControlledLifetimeManager());

			container.RegisterType<IPokemonRepository, PokemonRepository>();


		}
	}
}

