using System;
using System.Collections.Generic;
using System.Text;

namespace FmodForFoxes.Samples.UI
{
	public abstract class UIElement
	{
		public UIElement()
		{ 
			UIController.Add(this);	
		}

		public abstract void Update();
		public abstract void Draw();

		public void Destroy() =>
			UIController.Remove(this);
	}
}
