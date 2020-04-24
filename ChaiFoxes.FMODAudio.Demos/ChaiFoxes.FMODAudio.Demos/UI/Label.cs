using Microsoft.Xna.Framework;
using System;

namespace ChaiFoxes.FMODAudio.Demos.UI
{
	public class Label : UIElement
	{
		public string Text;
		public Vector2 Position { get; private set; }

		private readonly Func<Vector2> _positionUpdate;

		public Label(
			string text,
			Func<Vector2> positionUpdate
		) : base()
		{
			Text = text;
			_positionUpdate = positionUpdate;
			Position = _positionUpdate();
		}

		public override void Update()
		{
			_positionUpdate();
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
