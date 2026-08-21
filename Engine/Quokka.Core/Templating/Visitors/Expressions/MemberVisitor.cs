// // Copyright 2022 Mindbox Ltd
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class MemberVisitor : QuokkaBaseVisitor<Member>
    {
	    public MemberVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override Member VisitField(QuokkaParser.FieldContext context)
	    {
		    return new FieldMember(
			    context.Identifier().GetText(),
			    GetLocationFromToken(context.Identifier().Symbol));
	    }

	    public override Member VisitMethodCall(QuokkaParser.MethodCallContext context)
	    {
		    var name = context.Identifier().GetText();
		    var argumentList = context.argumentList();

			return new MethodMember(
				name,
				argumentList.Accept(new ArgumentListVisitor(VisitingContext)),
				GetLocationFromToken(context.Identifier().Symbol),
				VisitingContext.GetNextCallOrdinal(name, argumentList.GetText()));
	    }
    }
}
