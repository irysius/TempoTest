using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TempoTest
{
	public class Song
	{
		string name = "";
		int bpm;
		double tick;
		List<IMeasure> verses = new List<IMeasure>();
		int verse = 0;
		bool hasBegun;
		bool hasEnded;
		System.Timers.Timer timer;
		DateTime prevTime = DateTime.MinValue;
		DateTime startTime;
		Dispatcher dispatcher;
		double totalTime;

		public Song(int bpm) 
		{
			this.bpm = bpm;
			this.tick = 60.0 * 1000.0 / bpm;
			timer = new System.Timers.Timer(16);
			timer.Elapsed += timer_Elapsed;
			timer.AutoReset = true;
			dispatcher = Dispatcher.CurrentDispatcher;
		}

		public string Name { get; set; }
		public int BPM
		{
			get { return bpm; }
		}
		public List<IMeasure> Verses
		{
			get { return verses; }
		}
		public double TotalTime
		{
			get { return totalTime; }
		}

		public void Play()
		{
			totalTime = 0;
			foreach (var v in verses)
			{
				v.TargetMS = tick;
			}
			startTime = DateTime.Now;
			timer.Start();
		}

		public void Stop()
		{
			timer.Stop();
			Reset();
		}

		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			double elapsed = 0;
			if (prevTime == DateTime.MinValue)
				elapsed = 16;
			else
				elapsed = (e.SignalTime - prevTime).TotalMilliseconds;
			prevTime = e.SignalTime;

			totalTime += elapsed;

			if (hasEnded)
			{
				Stop();
				return;
			}

			hasEnded = Tick(elapsed, totalTime);
		}
		public bool Tick(double elapsed, double total, bool suppressEvents = false)
		{
			var hasSongEnded = false;
			if (!hasBegun)
			{
				if (!suppressEvents)
					hasBegun = true;
				if (OnSongBegin != null && !suppressEvents)
					OnSongBegin.Invoke(this, name);
			}

			var result = verses[verse].Tick(elapsed, total, suppressEvents);
			if (result)
			{
				if (verse == verses.Count - 1)
				{
					if (OnSongEnd != null && !suppressEvents)
						OnSongEnd.Invoke(this, name);
					verse = 0;
					hasSongEnded = true;

				}
				else
					verse++;
			}

			return hasSongEnded;
		}

		public void Reset()
		{
			verse = 0;
			hasBegun = false;
			foreach (var v in verses)
			{
				v.Reset();
			}
		}

		public event EventHandler<string> OnSongBegin;
		public event EventHandler<string> OnSongEnd;
	}
}
