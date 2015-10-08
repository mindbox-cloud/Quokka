﻿namespace Quokka
{
	/// <summary>
	/// Location in a template string
	/// </summary>
	public class Location
	{
		/// <summary>
		/// Line index (1-based)
		/// </summary>
		public int Line { get; }

		/// <summary>
		/// Column index (1-based)
		/// </summary>
		public int Column { get; }

		public Location(int line, int column)
		{
			Line = line;
			Column = column;
		}
	}
}
