namespace Mindbox.Quokka
{
    internal class FieldMember : Member
    {
	    private readonly string fieldName;

	    public FieldMember(string fieldName, Location location)
			: base(location)
	    {
		    this.fieldName = fieldName;
	    }

	    public override string StringRepresentation => fieldName;

		public override void PerformSemanticAnalysis(AnalysisContext analysisContext, ValueUsageSummary ownerValueUsageSummary, TypeDefinition memberType)
	    {
		    ownerValueUsageSummary.Fields.CreateOrUpdateMember(fieldName, new ValueUsage(Location, memberType));
	    }

	    public override ValueUsageSummary GetMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary)
	    {
		    return ownerValueUsageSummary.Fields.TryGetMemberUsageSummary(fieldName);
	    }

	    public override VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage)
	    {
		    return ownerValueStorage.GetFieldValueStorage(fieldName);
	    }
    }
}
