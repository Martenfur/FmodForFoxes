
namespace FmodForFoxes.Samples.Scenes
{
	public static class SceneController
	{
		private static Scene _currentScene;

		public static void Update()
		{ 
			_currentScene?.Update();
		}
		
		public static void Draw()
		{ 
			_currentScene?.Draw();
		}

		public static void ChangeScene(Scene scene)
		{
			_currentScene?.Leave();
			_currentScene = scene;
			_currentScene?.Enter();
		}
	}
}
