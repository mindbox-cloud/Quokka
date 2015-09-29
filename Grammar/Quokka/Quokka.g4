grammar Quokka;
options { tokenVocab = QuokkaLex; }

template
	: 
		templateBlock
	;

templateBlock
	:
		(staticBlock | dynamicBlock)+
	;
	
staticBlock
	:
		(constantBlock | outputInstruction | commentBlock)+
	;

dynamicBlock
	:
		ifStatement | forStatement
	;

constantBlock
	:
		Fluff+
	;

outputInstruction
	:
		OutputInstructionStart
		parameterValueExpression
		InstructionEnd
	;
	
parameterValueExpression
	:
		parameterExpression | parameterMemberExpression
	;
	
parameterExpression
	:
		Identifier
	;
	
parameterMemberExpression
	:
		parameterExpression
		memberAccessExpression
	;
	
memberAccessExpression
	:
		MemberAccessOperator
		Identifier
		memberAccessExpression?
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
		templateBlock
	;

elseCondition
	:
		elseInstruction
		templateBlock
	;
	
elseIfCondition
	:
		elseIfInstruction
		templateBlock
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
		templateBlock
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

booleanAtom
	:
		parameterValueExpression
		| notExpression
		| LeftParen booleanExpression RightParen		
	;
	
