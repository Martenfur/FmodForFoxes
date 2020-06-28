using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio.Studio
{
	public struct Attributes3D
	{
		/// <summary>
		/// Position in 3D space. Used for panning and attenuation.
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// Velocity in 3D space. Used for doppler effect.
		/// </summary>
		public Vector3 Velocity;

		/// <summary>
		/// Forwards orientation, must be of unit length (1.0) and perpendicular to up.
		/// </summary>
		public Vector3 ForwardVector;

		/// <summary>
		/// Upwards orientation, must be of unit length (1.0) and perpendicular to forward.
		/// </summary>
		public Vector3 UpVector;

		internal Attributes3D(FMOD.ATTRIBUTES_3D attributes)
		{
			Position = attributes.position.ToVector3();
			Velocity = attributes.velocity.ToVector3();
			ForwardVector = attributes.forward.ToVector3();
			UpVector = attributes.up.ToVector3();
		}

		public FMOD.ATTRIBUTES_3D ToFmodAttributes()
		{
			return new FMOD.ATTRIBUTES_3D
			{
				position = Position.ToFmodVector(),
				velocity = Velocity.ToFmodVector(),
				forward = ForwardVector.ToFmodVector(),
				up = UpVector.ToFmodVector()
			};
		}
	}
}
