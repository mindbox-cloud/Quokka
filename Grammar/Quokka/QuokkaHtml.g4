parser grammar QuokkaHtml;
options { tokenVocab = QuokkaHtmlLex; }

htmlBlock
	:
		(openingTag | closingTag | selfClosingTag | plainText | nonImportantHtml)+
	;

nonImportantHtml
	:
		HtmlComment | HtmlDtd | CDATA
	;

plainText
	:
		(Fluff | OutputBlock)+
	;

attribute
	:
		TAG_NAME
		(
			TAG_EQUALS
			attributeValue
		)?
	;

attributeValue
	:
		doubleQuotedValue | singleQuotedValue | unquotedValue
	;
	

unquotedValue
	:
		insideAttributeConstant | insideAttributeOutputBlock
	;

singleQuotedValue
	:
		OpeningSingleQuotes
		(insideAttributeOutputBlock | insideAttributeConstant)*
		ClosingSingleQuotes
	;

doubleQuotedValue
	:
		OpeningDoubleQuotes
		(insideAttributeOutputBlock | insideAttributeConstant)*
		ClosingDoubleQuotes
	;

insideAttributeOutputBlock
	:
		SQS_OUTPUTBLOCK | DQS_OUTPUTBLOCK | UnquotedOutputBlock
	;
	
insideAttributeConstant
	:
		SQS_FLUFF | DQS_FLUFF | UNQUOTED_ATTRIBUTE
	;	

openingTag
	:
		LeftAngularBracket
		TAG_NAME
		attribute*
		RightAngularBracket		
	;
	
closingTag
	:
		LeftAngularBracket
		Slash
		TAG_NAME
		attribute* // non-valid but it's best to support it in syntax and validate semantically	
		RightAngularBracket
	;
	
selfClosingTag
	:
		LeftAngularBracket
		TAG_NAME
		attribute*
		Slash
		RightAngularBracket
	;