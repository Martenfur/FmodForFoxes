using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ChaiFoxes.FMODAudio.Demos.UI
{
	public static class UIController
	{
		public static readonly Color Backgroud = new Color(82, 45, 91);
		public static readonly Color Accent = new Color(251, 123, 107);
		public static readonly Color Text = new Color(231, 211, 159);

		public static SpriteBatch SpriteBatch { get; private set; }

		private static List<UIElement> _elements = new List<UIElement>();

		public static void Init(GraphicsDevice device)
		{ 
			SpriteBatch = new SpriteBatch(device);
		}

		public static void Update()
		{ 
			foreach(var element in _elements.ToArray()) // Yeah, yeah, the thing generates garbage, who cares. :S
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
