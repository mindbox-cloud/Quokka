namespace Quokka
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
			return childScope;
		}

		public  VariableDefinition DeclareVariable(VariableDeclaration variableDeclaration)
		{
			return Variables.CreateDefinitionForVariableDeclaration(variableDeclaration);
		}
		
		public VariableDefinition CreateOrUpdateVariableDefinition(VariableOccurence variableOccurence)
		{
			var scope = GetExistingScopeForVariable(variableOccurence);
			if (scope == null)
				scope = variableOccurence.IsExternal ? GetRootScope() : this;

			return scope.CreateOrUpdateVariableDefinitionIgnoringParentScopes(variableOccurence);
		}

		private VariableDefinition CreateOrUpdateVariableDefinitionIgnoringParentScopes(VariableOccurence variableOccurence)
		{
			return Variables.CreateOrUpdateVariableDefinition(variableOccurence);
		}

		public VariableDefinition TryGetVariableDefinition(VariableOccurence variableOccurence)
		{
			return Variables.TryGetVariableDefinition(variableOccurence)
					?? parentScope?.TryGetVariableDefinition(variableOccurence);
		}

		private CompilationVariableScope GetRootScope()
		{
			return parentScope == null ? this : parentScope.GetRootScope();
		}

		private CompilationVariableScope GetExistingScopeForVariable(VariableOccurence variableOccurence)
		{
			return Variables.CheckIfVariableExists(variableOccurence.Name) 
				? this 
				: parentScope?.GetExistingScopeForVariable(variableOccurence);
		}
	}
}
