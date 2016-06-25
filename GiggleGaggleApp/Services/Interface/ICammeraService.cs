using System;
using System.Threading.Tasks;

namespace GiggleGaggleApp
{
	public interface ICammeraService
	{
		/// <summary>
		/// Takes the picture async.
		/// </summary>
		/// <returns>The picture async.</returns>
		Task<CameraResult> TakePictureAsync();
	}
}

