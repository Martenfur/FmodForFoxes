using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Various FMOD.VECTOR and Xna.Vector extensions. 
	/// </summary>
	public static class VectorExtensions
	{
		public static FMOD.VECTOR ToFmodVector(this Vector3 v)
		{
			return new FMOD.VECTOR 
			{
				x = v.X,
				y = v.Y,
				z = v.Z
			};
		}

		public static FMOD.VECTOR ToFmodVector(this Vector2 v)
		{
			return new FMOD.VECTOR 
			{
				x = v.X,
				y = v.Y,
				z = 0
			};
		}

		public static Vector3 ToVector3(this FMOD.VECTOR v)
		{
			return new Vector3
			{
				X = v.x,
				Y = v.y,
				Z = v.z
			};
		}

		public static Vector2 ToVector2(this FMOD.VECTOR v)
		{
			return new Vector2
			{
				X = v.x,
				Y = v.y,
			};
		}

	}
}
