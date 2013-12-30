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

			var bpm = 240.0;

			System.Timers.Timer timer = new System.Timers.Timer(60 * 1000 / bpm);
			timer.AutoReset = true;
			timer.Elapsed += timer_Elapsed;

			player = new WMPLib.WindowsMediaPlayer();
			player.uiMode = "invisible";

			if (File.Exists(midiFile))
				Console.WriteLine("File exists");

			WMPLib.IWMPMedia3 song = (WMPLib.IWMPMedia3)player.newMedia(midiFile);
			player.currentMedia = song;
			player.settings.volume = 10;

			player.controls.play();
			timer.Start();

			chickenDance = new ChickenDance();
			chickenDance.OnDebugReceived += chickenDance_OnDebugReceived;

			Console.ReadKey();
		}

		static void chickenDance_OnDebugReceived(object sender, string e)
		{
			Console.WriteLine(e);
		}

		// WMPLib comes from COM in References.
		static WMPLib.WindowsMediaPlayer player;
		static ChickenDance chickenDance;

		static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			chickenDance.Tick();
		}
	}
}

