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

		public MemberCollection<string> Members { get; } = new MemberCollection<string>(StringComparer.OrdinalIgnoreCase);
		
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

		public ValueUsageSummary DeclareVariable(string name, VariableDeclaration variableDeclaration)
		{
			return Members.CreateOrUpdateMember(name, variableDeclaration);
		}
		
		public void CreateOrUpdateVariableDefinition(string name, ValueUsage valueUsage)
		{
			var scope = GetExistingScopeForVariable(name);
			if (scope == null)
				scope = valueUsage.IsExternal ? GetRootScope() : this;

			scope.CreateOrUpdateVariableDefinitionIgnoringParentScopes(name, valueUsage);
		}

		public void CheckForChildScopesDeclarationConflicts(SemanticAnalysisContext context)
		{
			if (parentScope != null)
			{
				foreach (var item in Members.Items)
				{
					if (parentScope.GetExistingScopeForVariable(item.Key) != null)
						context.ErrorListener.AddVariableDeclarationScopeConflictError(item.Value, item.Value.GetFirstLocation());
				}
			}

			foreach (var childScope in childScopes)
				childScope.CheckForChildScopesDeclarationConflicts(context);
		} 

		private void CreateOrUpdateVariableDefinitionIgnoringParentScopes(string name, ValueUsage valueUsage)
		{
			Members.CreateOrUpdateMember(name, valueUsage);
		}

		public ValueUsageSummary TryGetVariableDefinition(string variableName)
		{
			return Members.TryGetMemberUsageSummary(variableName)
					?? parentScope?.TryGetVariableDefinition(variableName);
		}

		private CompilationVariableScope GetRootScope()
		{
			return parentScope == null ? this : parentScope.GetRootScope();
		}

		private CompilationVariableScope GetExistingScopeForVariable(string variableName)
		{
			return Members.CheckIfMemberExists(variableName) 
				? this 
				: parentScope?.GetExistingScopeForVariable(variableName);
		}
	}
}
