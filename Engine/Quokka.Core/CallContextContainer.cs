using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindbox.Quokka
{
	public class CallContextContainer
	{
		public static CallContextContainer Empty { get; } = new CallContextContainer();

		public static CallContextContainer Create<TCallContext>(TCallContext callContext)
			where TCallContext : class
		{
			return new CallContextContainer(typeof(TCallContext), callContext);
		}
		
		private readonly ReadOnlyDictionary<Type, object> callContextsByType;

		private CallContextContainer()
			: this(new Dictionary<Type, object>())
		{
		}

		private CallContextContainer(Type callContextType, object callContext)
			: this(
				new Dictionary<Type, object>
				{
					{ callContextType, callContext }
				})
		{
		}

		internal CallContextContainer(IDictionary<Type, object> callContextsByType)
		{
			this.callContextsByType = new ReadOnlyDictionary<Type, object>(callContextsByType);
		}

		public bool IsEmpty() => !callContextsByType.Any();

		public TCallContext GetCallContext<TCallContext>()
			where TCallContext : class 
		{
			if(!callContextsByType.TryGetValue(typeof(TCallContext), out object callContext))
				throw new InvalidOperationException($"Call context of type {typeof(TCallContext).FullName} wasn't registered");

			return (TCallContext)callContext;
		}
	}

	public class CallContextContainerBuilder
	{
		private readonly Dictionary<Type, object> callContextsByType = new Dictionary<Type, object>();

		public CallContextContainerBuilder WithCallContext<TCallContext>(TCallContext callContext)
		{
			callContextsByType.Add(typeof(TCallContext), callContext);
			return this;
		}

		public CallContextContainer Build()
		{
			return new CallContextContainer(callContextsByType);
		}
	}
}