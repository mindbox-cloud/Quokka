using System;

namespace Mindbox.Quokka
{
	internal abstract class SemanticErrorSubListenerBase
	{
		private Action<SemanticError> addError;

		public void Register(Action<SemanticError> addError)
		{
			if (this.addError != null)
				throw new InvalidOperationException("SubListener is already registered");

			this.addError = addError;
		}

		protected void AddError(SemanticError error)
		{
			if (addError == null)
				throw new InvalidOperationException("SubListener is not registered");

			addError(error);
		}
	}
}