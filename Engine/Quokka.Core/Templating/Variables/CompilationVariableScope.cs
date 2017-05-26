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

		public VariableCollection Variables { get; } = new VariableCollection();
		
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

		public  VariableDefinition DeclareVariable(VariableDeclaration variableDeclaration)
		{
			return Variables.CreateDefinitionForVariableDeclaration(variableDeclaration);
		}
		
		public void CreateOrUpdateVariableDefinition(VariableOccurence variableOccurence)
		{
			var scope = GetExistingScopeForVariable(variableOccurence.Name);
			if (scope == null)
				scope = variableOccurence.IsExternal ? GetRootScope() : this;

			scope.CreateOrUpdateVariableDefinitionIgnoringParentScopes(variableOccurence);
		}

		public void CheckForChildScopesDeclarationConflicts(SemanticAnalysisContext context)
		{
			if (parentScope != null)
			{
				foreach (var variable in Variables.Items)
				{
					if (parentScope.GetExistingScopeForVariable(variable.Name) != null)
						context.ErrorListener.AddVariableDeclarationScopeConflictError(variable, variable.GetFirstLocation());
				}
			}

			foreach (var childScope in childScopes)
				childScope.CheckForChildScopesDeclarationConflicts(context);
		} 

		private VariableDefinition CreateOrUpdateVariableDefinitionIgnoringParentScopes(VariableOccurence variableOccurence)
		{
			return Variables.CreateOrUpdateVariableDefinition(variableOccurence);
		}

		public VariableDefinition TryGetVariableDefinition(string variableName)
		{
			return Variables.TryGetVariableDefinition(variableName)
					?? parentScope?.TryGetVariableDefinition(variableName);
		}

		private CompilationVariableScope GetRootScope()
		{
			return parentScope == null ? this : parentScope.GetRootScope();
		}

		private CompilationVariableScope GetExistingScopeForVariable(string variableName)
		{
			return Variables.CheckIfVariableExists(variableName) 
				? this 
				: parentScope?.GetExistingScopeForVariable(variableName);
		}
	}
}
