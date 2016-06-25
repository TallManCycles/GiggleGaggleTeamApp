using System;
using GiggleGaggleApp.Droid;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Provider;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CameraService))]
namespace GiggleGaggleApp.Droid
{

	public class CameraService : ICammeraService
	{
		public CameraService()
		{
		}

		private static File file;
		private static File pictureDirectory;

		private static TaskCompletionSource<CameraResult> tcs;

		public Task<CameraResult> TakePictureAsync()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);

			pictureDirectory = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "BikeSquat");

			if (!pictureDirectory.Exists())
			{
				pictureDirectory.Mkdirs();
			}

			file = new File(pictureDirectory, String.Format("bikeSquat{0}.jpg", Guid.NewGuid()));

			intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(file));

			var activity = (Activity)Forms.Context;
			activity.StartActivityForResult(intent, 0);

			tcs = new TaskCompletionSource<CameraResult>();

			return tcs.Task;
		}

		public static void OnResult(Result resultCode)
		{
			if (resultCode == Result.Canceled)
			{
				tcs.TrySetResult(null);
				return;
			}

			if (resultCode != Result.Ok)
			{
				tcs.TrySetException(new Exception("Unexpected error"));
				return;
			}

			CameraResult res = new CameraResult();
			res.Image = ImageSource.FromFile(file.Path);
			res.FileUri = file.Path;

			tcs.TrySetResult(res);
		}
	}
}