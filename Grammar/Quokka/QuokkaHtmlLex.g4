lexer grammar QuokkaHtmlLex;

HtmlComment
	:
		'<!--' .*? '-->'
	;

HtmlDtd
	:
		'<!' .*? '>'
	;

CDATA       
    : '<![CDATA[' .*? ']]>' 
    ;

LeftAngularBracket
	:
		'<' -> pushMode(InsideTag)
	;
/* 
Fluff
	:
		~'<'+
	;
*/	

OutputBlock
	:
		'${' .*? '}'
	;	

Fluff : [$] ~'{' ~[$]*? '}'
      | [$] '{' '{' ~[$]*? '}'
      | [$]
      | ~[<$]+
      ;
	
mode InsideTag;

RightAngularBracket      
    : '>' -> popMode
    ;
/*
TAG_SLASH_CLOSE     
    : '/>' -> popMode
    ;
*/
Slash      
    : '/' 
    ;

//
// lexing mode for attribute values
//
TAG_EQUALS     
    : 
    	'='
    	[ \t\r\n]* -> pushMode(ATTVALUE)
    ;

TAG_NAME      
    : TAG_NameStartChar TAG_NameChar* 
    ;

TAG_WHITESPACE
    : [ \t\r\n] -> skip 
    ;

fragment
HEXDIGIT        
    : [a-fA-F0-9]
    ;

fragment
DIGIT           
    : [0-9]
    ;

fragment
TAG_NameChar        
    : TAG_NameStartChar
    | '-' 
    | '_' 
    | '.' 
    | DIGIT 
    |   '\u00B7'
    |   '\u0300'..'\u036F'
    |   '\u203F'..'\u2040'
    ;

fragment
TAG_NameStartChar
    :   [:a-zA-Z]
    |   '\u2070'..'\u218F' 
    |   '\u2C00'..'\u2FEF' 
    |   '\u3001'..'\uD7FF' 
    |   '\uF900'..'\uFDCF' 
    |   '\uFDF0'..'\uFFFD'
    ;

//
// <scripts>
//
mode SCRIPT;

SCRIPT_BODY
    : .*? '</script>' -> popMode
    ;

SCRIPT_SHORT_BODY
    : .*? '</>' -> popMode
    ;

//
// <styles>
//
mode STYLE;

STYLE_BODY
    : .*? '</style>' -> popMode
    ;

STYLE_SHORT_BODY
    : .*? '</>' -> popMode
    ;

//
// attribute values
//
mode ATTVALUE;


UNQUOTED_ATTRIBUTE
	:
	
     ( 
     	ATTCHARS 
	    | HEXCHARS
	    | DECCHARS
    ) -> popMode
    
    ;

fragment ATTCHAR
    : '-'
    | '_'
    | '.'
    | '/'
    | '+'
    | ','
    | '?'
    | '='
    | ':'
    | ';'
    | '#'
    | [0-9a-zA-Z]
    ;

fragment ATTCHARS
    : ATTCHAR+ ' '?
    ;

fragment HEXCHARS
    : '#' [0-9a-fA-F]+
    ;

fragment DECCHARS
    : [0-9]+ '%'?
    ;

OpeningDoubleQuotes
	:
		'"' -> pushMode(DoubleQuotes)
	;

OpeningSingleQuotes
	:
		'\'' -> pushMode(SingleQuotes)
	;

mode DoubleQuotes;

ClosingDoubleQuotes
	:
		'"' -> popMode, popMode 
	;
/*
DOUBLE_QUOTE_STRING
    : 
    	~[<"]+
    ;
*/    
DQS_OUTPUTBLOCK
	:
		'${' ~'}'* '}'
	;
    
DQS_FLUFF 
	: 
		'$' ~'{' ~[$"]*? '}'
		  | '$' '{' '{' ~[$"]*? '}'
		  | '$'
		  | ~[$"]+
	;
	
mode SingleQuotes;

ClosingSingleQuotes
	:
		'\'' -> popMode, popMode 
	;
/*
SINGLE_QUOTE_STRING
    :
    	~[<']+
    ;
  */  
SQS_OUTPUTBLOCK
	:
		'${' ~'}'* '}'
	;

SQS_FLUFF 
	: 
		'$' ~'{' ~[$']*? '}'
		  | '$' '{' '{' ~[$']*? '}'
		  | '$'
		  | ~[$']+
	;