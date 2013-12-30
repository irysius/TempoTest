using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TempoTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var midiFile = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\", "chickendance-240.mid"));

			player = new WMPLib.WindowsMediaPlayer();
			player.uiMode = "invisible";

			if (File.Exists(midiFile))
				Console.WriteLine("File exists");

			WMPLib.IWMPMedia3 song = (WMPLib.IWMPMedia3)player.newMedia(midiFile);
			player.currentMedia = song;
			player.settings.volume = 20;

			player.controls.play();
			player.PlayStateChange += player_PlayStateChange;

			chickenDance = new ChickenDance();
			chickenDance.OnDebugReceived += chickenDance_OnDebugReceived;

			Console.ReadKey();
		}


		static void player_PlayStateChange(int NewState)
		{
			if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsPlaying)
				chickenDance.Play();
		}

		static void chickenDance_OnDebugReceived(object sender, string e)
		{
			if (e == "---")
				Console.WriteLine(player.controls.currentPosition);
			else
				Console.WriteLine(e);
		}

		// WMPLib comes from COM in References.
		static WMPLib.WindowsMediaPlayer player;
		static ChickenDance chickenDance;
	}
}

