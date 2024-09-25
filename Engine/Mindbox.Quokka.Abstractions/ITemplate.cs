﻿// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka
{
	public interface ITemplate
	{
		bool IsConstant { get; }

		IList<ITemplateError> Errors { get; }

		ICompositeModelDefinition GetModelDefinition();

		string Render(ICompositeModelValue model, ICallContextContainer callContextContainer = null);

		string Render(ICompositeModelValue model, RenderSettings settings, ICallContextContainer callContextContainer = null);

		void Render(TextWriter textWriter, ICompositeModelValue model, ICallContextContainer callContextContainer = null);

		void Render(TextWriter textWriter, ICompositeModelValue model, RenderSettings settings, ICallContextContainer callContextContainer = null);

		BlockDTO GetTestDTO();
	}

	[DataContract]
	public record BlockDTO()
	{
		[DataMember]
		public string type { get; set; } = "";

		[DataMember]
		public string content { get; set; } = "";

		[DataMember]
		public string iterationVariableName { get; set; } = "";

		[DataMember]
		public string? assignmentVariableName { get; set; } = "";

		[DataMember]
		public ExpressionDTO condition { get; set; } = new ExpressionDTO();

		[DataMember]
		public ExpressionDTO expression { get; set; } = new ExpressionDTO();
		[DataMember]
		public List<BlockDTO> children { get; set; } = new();


	}

	[DataContract]
	public record ExpressionDTO()
	{
		[DataMember]
		public string type { get; set; } = "";

		[DataMember]
		public string variableName { get; set; } = "";


		[DataMember]
		public List<ExpressionDTO> members { get; set; } = [];


		[DataMember]
		public List<ExpressionDTO> arguments { get; set; } = [];


		[DataMember]
		public double? number { get; set; }

		[DataMember]
		public string? stringValue { get; set; }

		[DataMember]
		public string? quoteType { get; set; }

		[DataMember]
		public string? comparisonOperation { get; set; }

		[DataMember]
		public ExpressionDTO? argumentExpression { get; set; }
		[DataMember]
		public ExpressionDTO? operandExpression { get; set; }
	}
}
