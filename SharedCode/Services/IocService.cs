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
            //DI.DI.RegisterDIs();
        }

        public static IUnityContainer GetInstance()
        {
            return container ?? new UnityContainer();
        }
    }
}

