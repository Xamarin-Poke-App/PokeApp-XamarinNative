using System;
using Android.Widget;
using FFImageLoading;

namespace SharedCode.Services
{
	public static class ImageLoaderService
	{
		public static void CreateImageLoaderService()
		{
			var config = new FFImageLoading.Config.Configuration()
			{
				ExecuteCallbacksOnUIThread = true
			};
			ImageService.Instance.Initialize(config);
		}
		public static IImageService Instance
		{
			get
			{
				return ImageService.Instance;
			}
		}

		public static FFImageLoading.Work.TaskParameter LoadImageFromUrl(string url)
		{
			return ImageService.Instance.LoadUrl(url)
			.LoadingPlaceholder("loading.png")
			.ErrorPlaceholder("error.png")
			.Retry(3, 200);
		}
	}
}

