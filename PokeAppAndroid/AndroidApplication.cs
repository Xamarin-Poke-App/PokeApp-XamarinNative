using System;
using Android.App;
using Android.App.Job;
using SharedCode.DI;
using SharedCode.Services;
using PokeAppAndroid.Utils;
using SharedCode.Interfaces;
using SharedCode.Util;
using System.ComponentModel;
using Unity;
using Org.Xmlpull.V1.Sax2;
using Unity.Injection;
using Xamarin.Google.ErrorProne.Annotations;
using SharedCode.Database;
using Unity.Lifetime;

namespace PokeAppAndroid
{
	[Application]
	public class AndroidApplication : Application
	{
		public AndroidApplication(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
		}

        public override void OnCreate()
        {
            base.OnCreate();

            IocContainer.CreateContainer();
            // Storage Utils
            IocContainer.RegisterType<IStorage, Storage>();
            IocContainer.RegisterType<IStorageUtils, StorageUtils>();

            // Db
            IocContainer.Instance.RegisterType<IDatabaseManager, DatabaseManager>(new ContainerControlledLifetimeManager());
            IocContainer.RegisterType<IPathManager, PathManager>();

            // Image Loader Service
            ImageLoaderService.CreateImageLoaderService();
        }
    }
}