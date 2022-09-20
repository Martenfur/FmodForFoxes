
namespace FmodForFoxes
{
	public struct ConeSettings3D
	{
		public float InsideConeAngle;
		public float OutsideConeAngle;
		public float OutsideVolume;

		public ConeSettings3D(
			float insideConeAngle, 
			float outsideConeAngle, 
			float outsideVolume
		)
		{ 
			InsideConeAngle = insideConeAngle;
			OutsideConeAngle = outsideConeAngle;
			OutsideVolume = outsideVolume;
		}
	}
}
