﻿using System;

namespace Quokka
{
	[Serializable]
	public class InvalidTemplateModelException : TemplateException
	{
		public string Details { get; }

		public InvalidTemplateModelException(string message, string details)
			: base(message)
		{
			Details = details;
		}
	}
}