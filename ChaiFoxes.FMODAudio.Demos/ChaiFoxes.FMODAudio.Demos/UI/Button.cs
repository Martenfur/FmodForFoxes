using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace ChaiFoxes.FMODAudio.Demos.UI
{
	public class Button : UIElement
	{
		public string Text;
		public Vector2 Position;
		public Vector2 Size;
		public readonly Action Click;
		public readonly Action ClickRelease;

		private bool _oldClick = false;

		private bool _animationRunning = false;
		private float _animation = 0;
		private float _animationSpeed = 1f / 10f;
		private float _animationAmplitude = 8;

		public Button(
			string text,
			Vector2 position,
			Vector2 size,
			Action click,
			Action clickRelease = null
		) : base()
		{
			Text = text;
			Position = position;
			Size = size;
			Click = click;
			ClickRelease = clickRelease;
		}

		public override void Update()
		{
#if ANDROID
			var mousePosition = Vector2.Zero;
			var click = false;
			var state = TouchPanel.GetState();
			if (state.Count > 0)
			{
				mousePosition = state[0].Position;
				click = state[0].State == TouchLocationState.Pressed;
			}
#else
			var mouse = Mouse.GetState();
			var mousePosition = mouse.Position.ToVector2();
			var click = (mouse.LeftButton == ButtonState.Pressed);
#endif

			if (PointInRectangleBySize(mousePosition, Position, Size))
			{
				if (click)
				{
					Click?.Invoke();
					_animationRunning = true;
				}
				if (click && !_oldClick) // Press.
				{ 
					_animation = 0;
				}
				if (!click && _oldClick) // Release.
				{
					ClickRelease?.Invoke();
				}
			}
			_oldClick = click;

			if (_animationRunning)
			{
				_animation += _animationSpeed;
				if (click)
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
