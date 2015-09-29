lexer grammar QuokkaLex;

OutputInstructionStart
	:
		'${' -> pushMode(Instruction)
	;
	
ControlInstructionStart
	:
		'@{' -> pushMode(Instruction)
	;
	
Fluff : [$@] ~'{' ~[$@]*? '}'
      | [$@] '{' '{' ~[$@]*? '}'
      | [$@]
      | ~[$@]+
      ;
	
mode Instruction;

InstructionEnd
    :
        '}' -> popMode
    ;
    
If
	:
		[Ii][Ff]
	;
	
EndIf
	:
		End WhiteSpace+ If
	;
	
End
	:
		[Ee][Nn][Dd]
	;
	
Else
	:
		[Ee][Ll][Ss][Ee]
	;
	
ElseIf
	:
		Else WhiteSpace+ If
	;
	
For
	:
		[Ff][Oo][Rr]
	;
	
In
	:
		[Ii][Nn]
	;

EndFor
	:
		End WhiteSpace+ For
	;

Comment
	:
		[Cc][Oo][Mm][Mm][Ee][Nn][Tt]
	;
	
EndComment
	:
		End WhiteSpace+ Comment
	;
   
MemberAccessOperator
	:
		'.'
	;
	
LeftParen
	:
		'('
	;
	
RightParen
	:
		')'
	;
	
And
	:
		[Aa][Nn][Dd]
	;
	
Or
	:
		[Oo][Rr]
	;
	
Not
	:
		[Nn][Oo][Tt]
	;
  
 
Identifier
    :
        [a-zA-Z] [a-zA-Z0-9]*
    ;
    
WhiteSpace
	:
		(' ' | '\t' | '\r' | '\n') -> skip
	;