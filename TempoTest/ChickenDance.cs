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
			Verse intro = new Verse("Intro");
			intro.OnVerseBegin += onVerseBegin;
			intro.OnVerseEnd += onVerseEnd;
			intro.AddMeasures24(1);
			intro.AddMeasures44(3);

			Verse chickenDanceSection = new Verse();
			var chickenBeak = new Measure44("Chicken Beak");
			chickenBeak.OnFirstBeat += chickenBeak_OnFirstBeat;
			chickenDanceSection.Measures.Add(chickenBeak);

			var chickenWings = new Measure44("Chicken Wings");
			chickenWings.OnFirstBeat += chickenWings_OnFirstBeat;
			chickenDanceSection.Measures.Add(chickenWings);

			var tailFeathers = new Measure44("Shake Your Tail Feathers");
			tailFeathers.OnFirstBeat += tailFeathers_OnFirstBeat;
			chickenDanceSection.Measures.Add(tailFeathers);

			var clap = new Measure44("Clap");
			clap.OnFirstBeat += clap_OnFirstBeat;
			chickenDanceSection.Measures.Add(clap);

			Verse chickenDance = new Verse();
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.Measures.Add(chickenDanceSection);
			chickenDance.OnVerseBegin += chickenDance_OnVerseBegin;
			chickenDance.OnVerseEnd += chickenDance_OnVerseEnd;

			Verse interlude = new Verse("Interlude");
			interlude.OnVerseBegin += onVerseBegin;
			interlude.OnVerseEnd += onVerseEnd;
			interlude.AddMeasures44(16);

			Verse outro = new Verse("Outro");
			outro.OnVerseBegin += onVerseBegin;
			outro.OnVerseEnd += onVerseEnd;
			outro.AddMeasures44(2);

			// Setting up the song
			song = new Song("Chicken Dance");
			song.Verses.Add(intro);
			for (int i = 0; i < 4; ++i)
			{
				song.Verses.Add(chickenDance);
				song.Verses.Add(interlude);
			}
			song.Verses.Add(outro);
		}


		Song song;
		bool songEnded;
		DateTime begin;
		

		public void Tick()
		{
			if (songEnded) return;
			songEnded = song.Tick();
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
			begin = DateTime.Now;

			if (ChickenDanceBegun != null)
				ChickenDanceBegun.Invoke(null, EventArgs.Empty);
		}
		private void chickenDance_OnVerseEnd(object sender, string e)
		{
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
