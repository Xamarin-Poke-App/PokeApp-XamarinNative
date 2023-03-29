using System;
using Android.App;
using Android.App.Job;
using SharedCode.DI;
using SharedCode.Services;

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
            DI.RegisterDIs();
        }
    }
}