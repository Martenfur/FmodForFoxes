using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Sound listener in 3D space. Listens to positioned 3D sounds.
	/// 
	/// NOTE: Do not mess with low-level listeners if you're using those.
	/// It *probably* should be fine, but you're doing that at your own risk.
	/// </summary>
	public interface IListener3D
	{
		/// <summary>
		/// Listener position in 3D space. Used for panning and attenuation
		/// </summary>
		Vector3 Position3D { get; set; }


		/// <summary>
		/// Listener velocity in 3D space. Used for doppler effect.
		/// </summary>
		Vector3 Velocity3D { get; set; }


		/// <summary>
		/// Forwards orientation, must be of unit length (1.0) and perpendicular to up.
		/// UnitY by default.
		/// </summary>
		Vector3 ForwardOrientation { get; set; }


		/// <summary>
		/// Upwards orientation, must be of unit length (1.0) and perpendicular to forward.
		/// UnitZ by default.
		/// </summary>
		Vector3 UpOrientation { get; set; }


		/// <summary>
		/// Gets all listener attributes at once.
		/// </summary>
		void GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
		

		/// <summary>
		/// Sets all listener attributes at once.
		/// </summary>
		void SetAttributes(Vector3 position, Vector3 velocity, Vector3 forwardVector, Vector3 upVector);


		/// <summary>
		/// Destroys the listener.
		/// </summary>
		void Destroy();

	}
}
