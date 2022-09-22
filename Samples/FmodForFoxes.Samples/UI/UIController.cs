﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FmodForFoxes.Samples.UI
{
	public static class UIController
	{
		public static readonly Color Backgroud = new Color(110, 87, 115);
		public static readonly Color Accent = new Color(234, 144, 133);
		public static readonly Color Text = new Color(233, 225, 204);

		public static SpriteBatch SpriteBatch { get; private set; }

		private static List<UIElement> _elements = new List<UIElement>();

		public static void Init(GraphicsDevice device)
		{ 
			SpriteBatch = new SpriteBatch(device);
		}

		public static void Update()
		{
			InputManager.Update();
			foreach (var element in _elements.ToArray()) // Yeah, yeah, the thing generates garbage, who cares. :S
			{ 
				element.Update();
			}
		}
		
		public static void Draw()
		{ 
			SpriteBatch.Begin();
			foreach(var element in _elements.ToArray()) // Yeah, yeah, the thing generates garbage, who cares. :S
			{ 
				element.Draw();
			}
			SpriteBatch.End();
		}

		public static void Add(UIElement element) =>
			_elements.Add(element);
		
		public static void Remove(UIElement element) =>
			_elements.Remove(element);

	}
}
