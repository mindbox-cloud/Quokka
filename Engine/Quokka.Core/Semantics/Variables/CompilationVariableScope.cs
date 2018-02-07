using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	/// <summary>
	/// Variable scope used for template analysis and parameter discovery.
	/// </summary>
	/// <remarks>
	/// Stores all the distinct variables that are declared in this scope implicitly (by using a global parameter)
	/// or explicitly (by declaring a new variable in a for loop and possibly other places).
	/// 
	/// The collection of variables gets filled as we traverse the template tree.
	/// </remarks>
	internal class CompilationVariableScope
	{
		private readonly CompilationVariableScope parentScope;
		private readonly List<CompilationVariableScope> childScopes = new List<CompilationVariableScope>(); 

		public MemberCollection<string> Variables { get; } = new MemberCollection<string>();
		
		public CompilationVariableScope()
			: this(null)
		{
		}

		private CompilationVariableScope(CompilationVariableScope parentScope)
		{
			this.parentScope = parentScope;
		}

		public CompilationVariableScope CreateChildScope()
		{
			var childScope = new CompilationVariableScope(this);
			childScopes.Add(childScope);
			return childScope;
		}

		public ValueUsageSummary DeclareVariable(string name, ValueUsage valueUsage)
		{
			return Variables.CreateOrUpdateMember(name, valueUsage);
		}
		
		public void RegisterVariableValueUsage(string name, ValueUsage valueUsage)
		{
			var scope = GetExistingScopeForVariable(name) ?? GetRootScope();

			scope.RegisterVariableValueUsageIgnoringParentScopes(name, valueUsage);
		}

		public void Compile(AnalysisContext context)
		{
			if (parentScope != null)
			{
				foreach (var item in Variables.Items)
				{
					if (parentScope.GetExistingScopeForVariable(item.Key) != null)
						context.ErrorListener.AddVariableDeclarationScopeConflictError(item.Value, item.Value.GetFirstLocation());
				}
			}

			foreach (var item in Variables.Items)
				item.Value.Compile(context.ErrorListener);

			foreach (var childScope in childScopes)
				childScope.Compile(context);
		} 

		private void RegisterVariableValueUsageIgnoringParentScopes(string name, ValueUsage valueUsage)
		{
			Variables.CreateOrUpdateMember(name, valueUsage);
		}

		public ValueUsageSummary TryGetVariableDefinition(string variableName)
		{
			return Variables.TryGetMemberUsageSummary(variableName)
					?? parentScope?.TryGetVariableDefinition(variableName);
		}

		private CompilationVariableScope GetRootScope()
		{
			return parentScope == null ? this : parentScope.GetRootScope();
		}

		private CompilationVariableScope GetExistingScopeForVariable(string variableName)
		{
			return Variables.CheckIfMemberExists(variableName) 
				? this 
				: parentScope?.GetExistingScopeForVariable(variableName);
		}
	}
}
