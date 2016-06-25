using System;
using UIKit;
using Foundation;

namespace GiggleGaggleApp.iOS
{
	/// <summary>
	/// A class for the IOS Camera.
	/// </summary>
	public static class Camera
	{
		static UIImagePickerController picker;
		static Action<NSDictionary> _callback;

		static void Init()
		{
			if (picker != null)
				return;

			picker = new UIImagePickerController();
			picker.Delegate = new CameraDelegate();
		}

		class CameraDelegate : UIImagePickerControllerDelegate
		{
			public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
			{
				var cb = _callback;
				_callback = null;

				picker.DismissViewController(true, (Action)null);
				cb(info);
			}

			public override void Canceled(UIImagePickerController picker)
			{
				var cb = _callback;
				_callback = null;

				picker.DismissViewController(true, (Action)null);
				cb(null);
			}
		}

		/// <summary>
		/// Takes the picture.
		/// </summary>
		/// <returns>The picture.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="callback">Callback.</param>
		public static void TakePicture(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.Camera;
			_callback = callback;
			parent.PresentViewController((UIViewController)picker, true, (Action)null);
		}

		/// <summary>
		/// Selects the picture.
		/// </summary>
		/// <returns>The picture.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="callback">Callback.</param>
		public static void SelectPicture(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			_callback = callback;
			parent.PresentViewController((UIViewController)picker, true, (Action)null);
		}
	}
}


