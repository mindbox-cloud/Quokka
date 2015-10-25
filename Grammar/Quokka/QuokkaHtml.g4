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
		Fluff+
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
		UNQUOTED_ATTRIBUTE
	;

singleQuotedValue
	:
		OpeningSingleQuotes
		(SQS_OUTPUTBLOCK | SQS_FLUFF)*
		ClosingSingleQuotes
	;

doubleQuotedValue
	:
		OpeningDoubleQuotes
		(DQS_OUTPUTBLOCK | DQS_FLUFF)*
		ClosingDoubleQuotes
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