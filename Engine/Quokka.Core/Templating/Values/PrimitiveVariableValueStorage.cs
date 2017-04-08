using System;

namespace Mindbox.Quokka
{
	internal class PrimitiveVariableValueStorage : VariableValueStorage
	{
		private readonly IPrimitiveModelValue primitiveModel;

		public override IModelValue ModelValue { get; }

		public PrimitiveVariableValueStorage(IPrimitiveModelValue primitiveModel)
		{
			this.primitiveModel = primitiveModel;
			ModelValue = primitiveModel;
		}

		public PrimitiveVariableValueStorage(object primitiveValue)
			: this(new PrimitiveModelValue(primitiveValue))
		{
		}

		public override object GetPrimitiveValue()
		{
			return primitiveModel.Value;
		}

		public override bool CheckIfValueIsNull()
		{
			return primitiveModel.Value == null;
		}
	}
}