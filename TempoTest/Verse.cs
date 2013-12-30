using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempoTest
{
	public class Verse : IMeasure
	{
		string name = "";
		public Verse(string name)
		{
			this.name = name;
		}
		public Verse() { }
		public string Name
		{
			get { return name; }
		}

		List<IMeasure> measures = new List<IMeasure>();
		public List<IMeasure> Measures
		{
			get { return measures; }
		}
		int measure = 0;
		bool hasBegun;

		public bool Tick(bool suppressEvents = false)
		{
			var isLastMeasure = false;
			if (!hasBegun)
			{
				if (!suppressEvents)
					hasBegun = true;
				if (OnVerseBegin != null && !suppressEvents)
					OnVerseBegin.Invoke(this, name);
			}

			var result = measures[measure].Tick();
			if (result) 
			{
				if (measure == measures.Count - 1)
				{
					if (OnVerseEnd != null && !suppressEvents)
						OnVerseEnd.Invoke(this, name);
					measure = 0;
					isLastMeasure = true;
					hasBegun = false;
				}
				else
					measure++;
			}

			return isLastMeasure;
		}
		public void AddMeasures44(int count)
		{
			for (int i = 0; i < count; ++i)
			{
				measures.Add(new Measure44());
			}
		}
		public void AddMeasures24(int count)
		{
			for (int i = 0; i < count; ++i)
			{
				measures.Add(new Measure24());
			}
		}
		public event EventHandler<string> OnVerseBegin;
		public event EventHandler<string> OnVerseEnd;
	}
}
