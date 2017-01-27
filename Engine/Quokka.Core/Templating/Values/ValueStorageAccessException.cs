﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka.Templating
{
	internal class ValueStorageAccessException : Exception
	{
		public VariableOccurence Member;

		public ValueStorageAccessException(string message, VariableOccurence member)
			:base(message)
		{
			Member = member;
		}
	}
}