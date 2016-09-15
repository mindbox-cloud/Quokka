lexer grammar QuokkaLex;

SingleInstructionComment
	:
		'@{*' ~'}'* '*}'
	;

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
	
Null
	:
		[Nn][Uu][Ll][Ll]
	;

MemberAccessOperator
	:
		'.'
	;
	
Pipe
	:
		'|'
	;
	
CommaSeparator
	:
		','
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

Equals
	:
		'='
	;
	
NotEquals
	:
		'!='
	;
	
GreaterThan
	:
		'>'
	; 

LessThan
	:
		'<'
	;
	
GreaterThanOrEquals
	:
		'>='
	;
	
LessThanOrEquals
	:
		'<='
	;
	
Plus
	:
		'+'
	;
	
Minus
	:
		'-'
	;
 
Multiply
	:
		'*'
	;
	
Divide
	:
		'/'
	;

Number
	:
		Digit+ ('.'Digit+)?
	;

Digit
	:
		('0'..'9')
	; 
	
DoubleQuotedString
	:
		'"' ~'"'* '"'
	;
	
SingleQuotedString
	:
		'\'' ~'\''* '\''
	;
	
Identifier
    :
        [_a-zA-Z] [_a-zA-Z0-9]*
    ;


WhiteSpace
	:
		(' ' | '\t' | '\r' | '\n') -> skip
	;