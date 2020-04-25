using ChaiFoxes.FMODAudio.Demos.UI;
using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio.Demos.Scenes
{
	public class DemoSelectorScene : Scene
	{
		private Button _selectCore;
		private Button _selectStudio;
		private Label _title;

		public override void Enter()
		{
			// If Studio is not loaded, skip the selection screen, 
			// since we can only select Core.
			if (!FMODManager.UsesStudio)
			{
				SceneController.ChangeScene(new CoreDemoScene());
				return;
			}

			_title = new Label(
				"Choose your destiny",
				() => new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y / 2f)
			);

			_selectCore = new Button(
				"Core Demo",
				() => new Vector2(Game1.ScreenSize.X / 2 - Game1.ScreenSize.X / 4, Game1.ScreenSize.Y * 0.75f),
				new Vector2(200, 100),
				null,
				() => SceneController.ChangeScene(new CoreDemoScene())
			);

			_selectStudio = new Button(
				"Studio Demo",
				() => new Vector2(Game1.ScreenSize.X / 2 + Game1.ScreenSize.X / 4, Game1.ScreenSize.Y * 0.75f),
				new Vector2(200, 100),
				null,
				() => SceneController.ChangeScene(new StudioDemoScene())
			);
		}


		public override void Update()
		{
		}


		public override void Draw()
		{
		}


		public override void Leave()
		{
			_title?.Destroy();
			_selectCore?.Destroy();
			_selectStudio?.Destroy();
		}
	}
}
