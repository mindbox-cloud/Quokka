using System.Collections.Generic;

namespace Quokka
{
	internal class Scope
	{
		public VariableCollection Variables { get; } = new VariableCollection();
		private readonly Scope parentScope;
		private readonly List<Scope> childScopes = new List<Scope>();

		public Scope()
			: this(null)
		{
		}

		private Scope(Scope parentScope)
		{
			this.parentScope = parentScope;
		}

		public Scope CreateChildScope()
		{
			var childScope = new Scope(this);
			childScopes.Add(childScope);
			return childScope;
		}

		public VariableDefinition CreateOrUpdateVariableDefinition(
			VariableOccurence variableOccurence,
			ISemanticErrorListener errorListener)
		{
			var variableScope = GetDeclarationScopeForVariable(variableOccurence) ?? this;
			return variableScope.CreateOrUpdateVariableDefinitionIgnoringParentScopes(variableOccurence, errorListener);
		}

		private VariableDefinition CreateOrUpdateVariableDefinitionIgnoringParentScopes(
			VariableOccurence variableOccurence,
			ISemanticErrorListener errorListener)
		{
			return Variables.CreateOrUpdateVariableDefinition(variableOccurence, errorListener);
		}

		private Scope GetDeclarationScopeForVariable(VariableOccurence variableOccurence)
		{
			return Variables.CheckIfVariableExists(variableOccurence.Name) 
				? this 
				: parentScope?.GetDeclarationScopeForVariable(variableOccurence);
		}
	}
}
