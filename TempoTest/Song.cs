using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempoTest
{
	public class Song
	{
		string name = "";
		public Song() { }
		public Song(string name)
		{
			this.name = name;
		}
		public string Name
		{
			get { return name; }
		}
		List<IMeasure> verses = new List<IMeasure>();
		public List<IMeasure> Verses
		{
			get { return verses; }
		}
		int verse = 0;
		bool hasBegun;
		public bool Tick(bool suppressEvents = false)
		{
			var hasSongEnded = false;
			if (!hasBegun)
			{
				if (!suppressEvents)
					hasBegun = true;
				if (OnSongBegin != null && !suppressEvents)
					OnSongBegin.Invoke(this, name);
			}

			var result = verses[verse].Tick();
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

		public event EventHandler<string> OnSongBegin;
		public event EventHandler<string> OnSongEnd;
	}
}
