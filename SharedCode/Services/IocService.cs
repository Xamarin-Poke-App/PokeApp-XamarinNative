using System;
using Unity;

namespace SharedCode.Services
{
    public static class IocContainer
    {
        private static IUnityContainer container;
        public static void CreateContainer()
        {
            if (container == null)
                container = new UnityContainer();
            DI.DI.RegisterDIs();
        }

        public static IUnityContainer Instance
        {
            get
            {
                return container ?? new UnityContainer();
            }
        }

        public static T GetDependency<T>()
        {
            return container.Resolve<T>();
        }

        public static void RegisterType<I, T>() where T : I
        {
            DI.DI.RegisterType<I, T>();
        }
    }
}
