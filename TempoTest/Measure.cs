using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempoTest
{
	public interface IMeasure
	{
		string Name { get; set; }
		bool Tick(double elapsed, double total, bool suppressEvents = false);
		void Reset();
		double TargetMS { get; set; }
	}


	public class MeasureWaitUntil : IMeasure
	{
		double targetTime = 0;
		bool hasBegun;

		public MeasureWaitUntil(double milliseconds) 
		{
			this.targetTime = milliseconds;
		}

		public string Name { get; set; }
		public double TargetMS { get; set; }
		public double TargetTime
		{
			get { return targetTime; }
		}
		public bool Tick(double elapsed, double total, bool suppressEvents = false)
		{
			if (!hasBegun)
			{
				if (!suppressEvents)
					hasBegun = true;
				if (OnMeasureBegin != null && !suppressEvents)
					OnMeasureBegin.Invoke(this, Name);
			}
			if (total >= targetTime)
			{
				if (OnMeasureEnd != null && !suppressEvents)
					OnMeasureEnd.Invoke(this, Name);
				return true;
			}
			return false;
		}

		public void Reset() 
		{
			hasBegun = false;
		}

		public event EventHandler<string> OnMeasureBegin;
		public event EventHandler<string> OnMeasureEnd;
	}

	public class MeasureBeat : IMeasure
	{
		int beats;
		int beatIndex = 0;
		double currMS = 0;

		public MeasureBeat(int beats) 
		{
			this.beats = beats;
		}

		public string Name { get; set; }
		public double TargetMS { get; set; }
		public int Beats
		{
			get { return beats; }
		}

		public bool Tick(double elapsed, double total, bool suppressEvents = false)
		{
			currMS += elapsed;
			var isLastBeat = false;
			if (currMS >= TargetMS)
			{
				currMS -= TargetMS;
				beatIndex++;
				if (beatIndex == 1)
				{
					if (OnFirstBeat != null && !suppressEvents)
						OnFirstBeat.Invoke(this, Name);
				}
				else if (beatIndex == beats)
				{
					if (OnLastBeat != null && !suppressEvents)
						OnLastBeat.Invoke(this, Name);
					beatIndex = 0;
					currMS = 0;
					isLastBeat = true;
				}
				else
				{
					if (OnMiddleBeats != null && !suppressEvents)
						OnMiddleBeats.Invoke(this, Tuple.Create(beatIndex, Name));
				}
			}
			return isLastBeat;
		}

		public void Reset()
		{
			beatIndex = 0;
			currMS = 0;
		}

		public event EventHandler<string> OnFirstBeat;
		public event EventHandler<Tuple<int, string>> OnMiddleBeats;
		public event EventHandler<string> OnLastBeat;
	}

}
