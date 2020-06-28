using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ChaiFoxes.FMODAudio.Demos.UI
{
	public class Button : UIElement
	{
		public string Text;
		public Vector2 Position { get; private set; }
		public Vector2 Size;
		private readonly Func<Vector2> _positionUpdate; 
		private readonly Action _click;
		private readonly Action _clickRelease;

		private bool _animationRunning = false;
		private float _animation = 0;
		private float _animationSpeed = 1f / 10f;
		private float _animationAmplitude = 8;

		public Button(
			string text,
			Func<Vector2> positionUpdate,
			Vector2 size,
			Action click,
			Action clickRelease = null
		) : base()
		{
			Text = text;
			_positionUpdate = positionUpdate;
			Position = _positionUpdate();
			Size = size;
			_click = click;
			_clickRelease = clickRelease;
		}

		public override void Update()
		{
			_positionUpdate();

			if (PointInRectangleBySize(InputManager.MousePosition, Position, Size))
			{
				if (InputManager.MouseHeld)
				{
					_click?.Invoke();
					_animationRunning = true;
				}
				if (InputManager.MousePressed)
				{ 
					_animation = 0;
				}
				if (InputManager.MouseReleased)
				{
					_clickRelease?.Invoke();
				}
			}

			if (_animationRunning)
			{
				_animation += _animationSpeed;
				if (InputManager.MouseHeld)
				{
					if (_animation > 0.25f)
					{
						_animation = 0.25f;	
					}
				}
				if (_animation > 1)
				{
					_animationRunning = false;
					_animation = 0;
				}
			}
		}

		public override void Draw()
		{
			UIController.SpriteBatch.Draw(
				Resources.Button,
				Position,
				null,
				UIController.Accent,
				0,
				new Vector2(Resources.Button.Width, Resources.Button.Height) / 2f,
				Size + Vector2.One * _animationAmplitude * (float)Math.Sin(Math.PI * _animation * 2),
				SpriteEffects.None,
				0
			);
			UIController.SpriteBatch.DrawString(
				Resources.Arial,
				Text,
				(Position - Resources.Arial.MeasureString(Text) / 2f).ToPoint().ToVector2(),
				UIController.Text
			);
		}

		public static bool PointInRectangleBySize(Vector2 point, Vector2 rectCenter, Vector2 rectSize)
		{
			var rectHalfSize = rectSize / 2f;
			var pt1 = rectCenter - rectHalfSize;
			var pt2 = rectCenter + rectHalfSize;
			return point.X >= pt1.X && point.X <= pt2.X && point.Y >= pt1.Y && point.Y <= pt2.Y;
		}

	}
}
