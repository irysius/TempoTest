using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempoTest
{
	public class Verse : IMeasure
	{
		public Verse() { }
		public string Name { get; set; }
		double targetMS = 0;
		public double TargetMS
		{
			get { return targetMS; }
			set
			{
				foreach (var m in measures)
				{
					m.TargetMS = value;
				}
				targetMS = value;
			}
		}

		List<IMeasure> measures = new List<IMeasure>();
		public List<IMeasure> Measures
		{
			get { return measures; }
		}
		int measure = 0;
		bool hasBegun;

		public bool Tick(double elapsed, double total, bool suppressEvents = false)
		{
			var isLastMeasure = false;
			if (!hasBegun)
			{
				if (!suppressEvents)
					hasBegun = true;
				if (OnVerseBegin != null && !suppressEvents)
					OnVerseBegin.Invoke(this, Name);
			}

			var result = measures[measure].Tick(elapsed, total, suppressEvents);
			if (result) 
			{
				if (measure == measures.Count - 1)
				{
					if (OnVerseEnd != null && !suppressEvents)
						OnVerseEnd.Invoke(this, Name);
					measure = 0;
					isLastMeasure = true;
					hasBegun = false;
				}
				else
					measure++;
			}

			return isLastMeasure;
		}

		public void Reset()
		{
			measure = 0;
			hasBegun = false;
			foreach (var m in measures)
			{
				m.Reset();
			}
		}

		public event EventHandler<string> OnVerseBegin;
		public event EventHandler<string> OnVerseEnd;
	}
}
