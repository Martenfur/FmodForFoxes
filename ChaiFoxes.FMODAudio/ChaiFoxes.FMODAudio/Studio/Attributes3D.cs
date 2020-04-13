using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio.Studio
{
	public struct Attributes3D
	{
		/// <summary>
		/// Position in 3D space. Used for panning and attenuation.
		/// </summary>
		public Vector3 position;

		/// <summary>
		/// Velocity in 3D space. Used for doppler effect.
		/// </summary>
		public Vector3 velocity;

		/// <summary>
		/// Forwards orientation, must be of unit length (1.0) and perpendicular to up.
		/// </summary>
		public Vector3 forwardVector;

		/// <summary>
		/// Upwards orientation, must be of unit length (1.0) and perpendicular to forward.
		/// </summary>
		public Vector3 upVector;
	}

	/// <summary>
	/// Extensions to FMOD.ATTRIBUTES_3D and Attributes3D. 
	/// </summary>
	public static class AttributeExtensions
	{
		public static FMOD.ATTRIBUTES_3D ToFmodAttributes(this Attributes3D attributes)
		{
			return new FMOD.ATTRIBUTES_3D
			{
				position = attributes.position.ToFmodVector(),
				velocity = attributes.velocity.ToFmodVector(),
				forward = attributes.forwardVector.ToFmodVector(),
				up = attributes.upVector.ToFmodVector()
			};
		}

		public static Attributes3D ToAttributes3D(this FMOD.ATTRIBUTES_3D attributes)
		{
			return new Attributes3D
			{
				position = attributes.position.ToVector3(),
				velocity = attributes.velocity.ToVector3(),
				forwardVector = attributes.forward.ToVector3(),
				upVector = attributes.up.ToVector3()
			};
		}
	}
}