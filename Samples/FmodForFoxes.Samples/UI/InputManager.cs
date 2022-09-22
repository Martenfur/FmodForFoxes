using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace FmodForFoxes.Samples.UI
{
	public static class InputManager
	{
		public static Vector2 MousePosition { get; private set; }

		public static bool MouseHeld => _click;
		public static bool MousePressed => _click && !_oldClick;
		public static bool MouseReleased => !_click && _oldClick;

		private static bool _click;
		private static bool _oldClick;

		public static void Update()
		{
			_oldClick = _click;

			if (OperatingSystem.IsAndroid())
			{
				MousePosition = Vector2.Zero;
				_click = false;
				var state = TouchPanel.GetState();

				if (state.Count > 0)
				{
					MousePosition = state[0].Position;
					_click = state[0].State == TouchLocationState.Moved;
				}
			}
			else
			{

				var mouse = Mouse.GetState();
				MousePosition = mouse.Position.ToVector2();
				_click = (mouse.LeftButton == ButtonState.Pressed);
			}
		}
	}
}
