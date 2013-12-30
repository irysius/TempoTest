using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempoTest
{
	public interface IMeasure
	{
		string Name { get; }
		bool Tick(bool suppressEvents = false);
	}

	public class Measure44 : IMeasure
	{
		string name = "";
		public Measure44(string name)
		{
			this.name = name;
		}
		public Measure44() { }
		public string Name
		{
			get { return name; }
		}
		int beat = 0;

		public bool Tick(bool suppressEvents = false)
		{
			var isLastBeat = false;
			beat++;
			switch (beat)
			{
				case 1: 
					if (OnFirstBeat != null && !suppressEvents)
						OnFirstBeat.Invoke(this, name);
					break;
				case 2:
					if (OnSecondBeat != null && !suppressEvents)
						OnSecondBeat.Invoke(this, name);
					break;
				case 3:
					if (OnThirdBeat != null && !suppressEvents)
						OnThirdBeat.Invoke(this, name);
					break;
				default:
					if (OnLastBeat != null && !suppressEvents)
						OnLastBeat.Invoke(this, name);
					beat = 0;
					isLastBeat = true;
					break;
			}
			return isLastBeat;
		}
		public event EventHandler<string> OnFirstBeat;
		public event EventHandler<string> OnSecondBeat;
		public event EventHandler<string> OnThirdBeat;
		public event EventHandler<string> OnLastBeat;
	}

	public class Measure24 : IMeasure
	{
		string name = "";
		public Measure24(string name)
		{
			this.name = name;
		}
		public Measure24() { }
		public string Name
		{
			get { return name; }
		}
		int beat = 0;

		public bool Tick(bool suppressEvents = false)
		{
			var isLastBeat = false;
			beat++;
			switch (beat)
			{
				case 1: 
					if (OnFirstBeat != null && !suppressEvents)
						OnFirstBeat.Invoke(this, name);
					break;
				default:
					if (OnLastBeat != null && !suppressEvents)
						OnLastBeat.Invoke(this, name);
					beat = 0;
					isLastBeat = true;
					break;
			}
			return isLastBeat;
		}
		public event EventHandler<string> OnFirstBeat;
		public event EventHandler<string> OnLastBeat;
	}
}
