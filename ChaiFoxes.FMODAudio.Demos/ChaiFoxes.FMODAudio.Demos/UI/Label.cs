using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio.Demos.UI
{
	public class Label : UIElement
	{
		public string Text;
		public Vector2 Position;

		public Label(
			string text,
			Vector2 position
		) : base()
		{
			Text = text;
			Position = position;
		}

		public override void Update()
		{

		}

		public override void Draw()
		{
			UIController.SpriteBatch.DrawString(
				Resources.Arial,
				Text,
				(Position - Resources.Arial.MeasureString(Text) / 2f).ToPoint().ToVector2(),
				UIController.Text
			);
		}
	}
}
