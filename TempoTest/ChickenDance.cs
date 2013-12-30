using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempoTest
{
	public class ChickenDance
	{
		public ChickenDance()
		{
 			// Setting up the song
			MeasureWaitUntil intro = new MeasureWaitUntil(3380);
			intro.Name = "Intro";
			intro.OnMeasureBegin += onVerseBegin;
			intro.OnMeasureEnd += onVerseEnd;

			Verse chickenDanceSection = new Verse();
			var chickenBeak = new MeasureBeat(4);
			chickenBeak.Name = "Chicken Beak";
			chickenBeak.OnFirstBeat += chickenBeak_OnFirstBeat;
			chickenDanceSection.Measures.Add(chickenBeak);

			var chickenWings = new MeasureBeat(4);
			chickenWings.Name = "Chicken Wings";
			chickenWings.OnFirstBeat += chickenWings_OnFirstBeat;
			chickenDanceSection.Measures.Add(chickenWings);

			var tailFeathers = new MeasureBeat(4);
			tailFeathers.Name = "Shake Your Tail Feathers";
			tailFeathers.OnFirstBeat += tailFeathers_OnFirstBeat;
			chickenDanceSection.Measures.Add(tailFeathers);

			var clap = new MeasureBeat(4);
			clap.Name = "Clap";
			clap.OnFirstBeat += clap_OnFirstBeat;
			chickenDanceSection.Measures.Add(clap);

			Verse chickenDance = new Verse();
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.OnVerseBegin += chickenDance_OnVerseBegin;
			chickenDance.OnVerseEnd += chickenDance_OnVerseEnd;

			MeasureWaitUntil interlude1 = new MeasureWaitUntil(36650);
			interlude1.Name = "Interlude1";
			interlude1.OnMeasureBegin += onVerseBegin;
			interlude1.OnMeasureEnd += onVerseEnd;

			MeasureWaitUntil interlude2 = new MeasureWaitUntil(69900);
			interlude2.Name = "Interlude2";
			interlude2.OnMeasureBegin += onVerseBegin;
			interlude2.OnMeasureEnd += onVerseEnd;

			MeasureWaitUntil interlude3 = new MeasureWaitUntil(102750);
			interlude3.Name = "Interlude3";
			interlude3.OnMeasureBegin += onVerseBegin;
			interlude3.OnMeasureEnd += onVerseEnd;

			MeasureWaitUntil interlude4 = new MeasureWaitUntil(136020);
			interlude4.Name = "Interlude4";
			interlude4.OnMeasureBegin += onVerseBegin;
			interlude4.OnMeasureEnd += onVerseEnd;

			Verse outro = new Verse();
			outro.Name = "Outro";
			outro.OnVerseBegin += onVerseBegin;
			outro.OnVerseEnd += onVerseEnd;
			outro.Measures.Add(new MeasureBeat(10));

			// Setting up the song
			song = new Song(240);
			song.Name = "Chicken Dance";
			song.Verses.Add(intro);

			song.Verses.Add(chickenDance);
			song.Verses.Add(interlude1);

			song.Verses.Add(chickenDance);
			song.Verses.Add(interlude2);

			song.Verses.Add(chickenDance);
			song.Verses.Add(interlude3);

			song.Verses.Add(chickenDance);
			song.Verses.Add(interlude4);

			song.Verses.Add(outro);
		}

		Song song;
		DateTime begin;

		public void Play()
		{
			song.Play();
		}

		private void onVerseBegin(object sender, string e)
		{
			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, e + " Begin");
		}
		private void onVerseEnd(object sender, string e)
		{
			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, e + " End");
		}

		private void chickenBeak_OnFirstBeat(object sender, string e)
		{
			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, e);

			if (OnDanceMove != null)
				OnDanceMove.Invoke(null, "Chicken Beak");
		}
		private void chickenWings_OnFirstBeat(object sender, string e)
		{
			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, e);

			if (OnDanceMove != null)
				OnDanceMove.Invoke(null, "Chicken Wings");
		}
		private void tailFeathers_OnFirstBeat(object sender, string e)
		{
			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, e);

			if (OnDanceMove != null)
				OnDanceMove.Invoke(null, "Tail Feathers");
		}
		private void clap_OnFirstBeat(object sender, string e)
		{
			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, e);

			if (OnDanceMove != null)
				OnDanceMove.Invoke(null, "Clap");
		}

		private void chickenDance_OnVerseBegin(object sender, string e)
		{
			Console.WriteLine(song.TotalTime);
			begin = DateTime.Now;

			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, "---");

			if (ChickenDanceBegun != null)
				ChickenDanceBegun.Invoke(null, EventArgs.Empty);
		}
		private void chickenDance_OnVerseEnd(object sender, string e)
		{
			Console.WriteLine(song.TotalTime);
			var elapsed = DateTime.Now - begin;

			// Spits out time elapsed for 4 repeats of the chicken dance routine.
			if (OnDebugReceived != null)
				OnDebugReceived.Invoke(this, "Elapsed: " + elapsed.TotalSeconds);

			if (ChickenDanceEnded != null)
				ChickenDanceEnded.Invoke(null, EventArgs.Empty);
		}

		public event EventHandler<string> OnDebugReceived;
		public event EventHandler ChickenDanceBegun;
		public event EventHandler ChickenDanceEnded;
		public event EventHandler<string> OnDanceMove;
	}
}
