using System;

namespace Mindbox.Quokka
{
	public class CallContextContainer
	{
		public static CallContextContainer Empty { get; } = new CallContextContainer();

		public static CallContextContainer Create<TCallContext>(TCallContext callContext)
			where TCallContext : class
		{
			return new CallContextContainer(callContext);
		}

		private readonly object callContext;

		private CallContextContainer()
		{
			
		}

		private CallContextContainer(object callContext) : this()
		{
			if (callContext == null) 
				throw new ArgumentNullException(nameof(callContext));

			this.callContext = callContext;
		}

		public bool IsEmpty() => callContext == null;

		public TCallContext GetCallContext<TCallContext>()
			where TCallContext : class 
		{
			if (IsEmpty())
				throw new InvalidOperationException($"Can't get call context from empty {nameof(CallContextContainer)}");

			var requestedCallContext = callContext as TCallContext;
			if (requestedCallContext == null)
				throw new InvalidOperationException(
					$"Call context contains object of type {callContext.GetType()}, " +
					$"which is not compatible with requested type {typeof(TCallContext)}");

			return requestedCallContext;
		}
	}
}