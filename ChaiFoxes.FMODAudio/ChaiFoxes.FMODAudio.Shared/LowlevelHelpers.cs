using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChaiFoxes.FMODAudio
{
	public static class LowlevelHelpers
	{
		public static T[] IntPtrToArray<T>(IntPtr ptr) where T : struct
		{
			var sizeInBytes = Marshal.SizeOf(typeof(FMOD.VECTOR));
			var output = new Vector3[vectorsCount];

			for (var i = 0; i < vectorsCount; i += 1)
			{
				pointer += i * sizeInBytes;
				output[i] = ((FMOD.VECTOR)Marshal.PtrToStructure(pointer, typeof(FMOD.VECTOR))).ToVector3();
			}

			return output;
		}

		public static IntPtr ArrayToIntPtr<T>(T array) where T : struct
		{
			var vectors = new FMOD.VECTOR[value.Length];

			for (var i = 0; i < value.Length; i += 1)
			{
				vectors[i] = value[i].ToFmodVector();
			}
			Native.set3DCustomRolloff(ref vectors[0], vectors.Length);
		}
	}
}
