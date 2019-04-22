using System;
using System.Collections.Generic;
using System.Text;
using ChaiFoxes.FMODAudio;

namespace ChaiFoxes.FMODAudio.Demos
{
	public static class Test
	{
		public static void Play()
		{
			AudioMgr.Init("Content/");

			var sound = AudioMgr.LoadStreamedSound("test.mp3");
			
			var group = AudioMgr.CreateChannelGroup("group");
			
			sound.Play(group);
			sound.LowPass = 0.5f;

			//AudioMgr.PlaySound(sound, group);


		}
	}
}
