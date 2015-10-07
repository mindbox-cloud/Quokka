grammar Quokka;
options { tokenVocab = QuokkaLex; }

template
	:
		templateBlock*
	;

templateBlock
	:
		(staticBlock | dynamicBlock)+
	;
	
staticBlock
	:
		(constantBlock | outputBlock)+
	;

dynamicBlock
	:
		ifStatement | forStatement  | commentBlock
	;

constantBlock
	:
		Fluff+
	;

outputBlock
	:
		OutputInstructionStart
		(
			filteredParameterValueExpression | arithmeticExpression
		)
		InstructionEnd
	;
	
parameterValueExpression
	:
		parameterExpression
		memberAccessExpression?
	;

parameterExpression
	:
		Identifier
	;
	
memberAccessExpression
	:
		MemberAccessOperator
		Identifier
		memberAccessExpression?
	;	

filteredParameterValueExpression
	:
		parameterValueExpression filterChain?
	;
	
filterChain
	:
		(
			Pipe
			filter
		)+
	;
	
filter
	:
		Identifier
		filterArgumentList?
	;

filterArgumentList
	:	
		LeftParen
		filterArgumentValue
		(CommaSeparator filterArgumentValue)*
		RightParen
	;

filterArgumentValue
	:
		DoubleQuotedString
	;
	

ifStatement
	:
		ifCondition
		elseIfCondition*
		elseCondition?
		endIfInstruction
	;
	
ifCondition
	:
		ifInstruction
		templateBlock?
	;

elseCondition
	:
		elseInstruction
		templateBlock?
	;
	
elseIfCondition
	:
		elseIfInstruction
		templateBlock?
	;
	
ifInstruction
	:
		ControlInstructionStart
		If
		booleanExpression
		InstructionEnd
	;

elseIfInstruction
	:
		ControlInstructionStart
		ElseIf
		booleanExpression
		InstructionEnd
	;

elseInstruction
	:
		ControlInstructionStart
		Else
		InstructionEnd
	;

endIfInstruction
	:
		ControlInstructionStart
		EndIf
		InstructionEnd
	;

forStatement
	:
		forInstruction
		templateBlock?
		endForInstruction
	;

forInstruction
	:
		ControlInstructionStart
		For	iterationVariable In parameterValueExpression
		InstructionEnd
	;
	
iterationVariable
	:
		Identifier
	;
	
endForInstruction
	:
		ControlInstructionStart
		EndFor
		InstructionEnd
	;

commentBlock
	:
		SingleInstructionComment
		|		
		commentInstruction
		templateBlock
		endCommentInstruction
	;

commentInstruction
	:
		ControlInstructionStart
		Comment
		InstructionEnd
	;
	
endCommentInstruction
	:
		ControlInstructionStart
		EndComment
		InstructionEnd
	;
	
booleanExpression
	:
		andExpression
		(Or andExpression)*
	;
	
andExpression
	:
		booleanAtom
		(And booleanAtom)*
	;	

notExpression
	:
		Not booleanAtom
	;
	
parenthesizedBooleanExpression
	:
		LeftParen booleanExpression RightParen	
	;
	
booleanAtom
	:
		parameterValueExpression
		| arithmeticComparisonExpression
		| notExpression
		| parenthesizedBooleanExpression
	;

arithmeticComparisonExpression
	:
		arithmeticExpression
		(Equals | NotEquals | LessThan | GreaterThan | LessThanOrEquals | GreaterThanOrEquals)
		arithmeticExpression
	;

arithmeticExpression
	:
		multiplicationExpression
		(plusOperand | minusOperand)*
	;
	
plusOperand
	:
		Plus multiplicationExpression
	;
	
minusOperand
	:
		Minus multiplicationExpression
	;

multiplicationExpression
	:
		arithmeticAtom
		(multiplicationOperand | divisionOperand)*
	;
	
multiplicationOperand
	:
		Multiply arithmeticAtom
	;
	
divisionOperand
	:
		Divide arithmeticAtom
	;

negationExpression
	:
		Minus
		arithmeticAtom
	;
	
arithmeticAtom
	:
		Number
		| parameterValueExpression
		| negationExpression
		| LeftParen arithmeticExpression RightParen	
	;