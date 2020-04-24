using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChaiFoxes.FMODAudio.Demos
{
	public static class Resources
	{
		public static SpriteFont Arial;
		
		public static Texture2D Button;
		public static Texture2D Gato;
		
		public static void Load(ContentManager content)
		{ 
			Arial = content.Load<SpriteFont>("Arial");

			Button = content.Load<Texture2D>("Button");
			Gato = content.Load<Texture2D>("Gato");
		}
	}
}
