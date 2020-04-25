using System;
using System.Collections.Generic;
using System.Text;

namespace ChaiFoxes.FMODAudio.Demos.Scenes
{
	public class StudioDemoScene : Scene
	{
		public override void Enter()
		{
			// Initialized here only for the sake of the demo.
			// Usually has to be initialized right at startup.
			FMODManager.Init(FMODMode.CoreAndStudio, "Content");
			InitUI();
		}

		public override void Update()
		{
		}

		public override void Draw()
		{
			FMODManager.Update();
		}

		public override void Leave()
		{
		}

		private void InitUI()
		{


		}
	}
}
