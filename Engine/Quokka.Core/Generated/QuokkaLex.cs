//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Code\Quokka\Grammar\Quokka\QuokkaLex.g4 by ANTLR 4.5.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace Quokka.Generated {
using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.1")]
[System.CLSCompliant(false)]
internal partial class QuokkaLex : Lexer {
	public const int
		SingleInstructionComment=1, OutputInstructionStart=2, ControlInstructionStart=3, 
		Fluff=4, InstructionEnd=5, If=6, EndIf=7, End=8, Else=9, ElseIf=10, For=11, 
		In=12, EndFor=13, Null=14, MemberAccessOperator=15, Pipe=16, CommaSeparator=17, 
		LeftParen=18, RightParen=19, And=20, Or=21, Not=22, Equals=23, NotEquals=24, 
		GreaterThan=25, LessThan=26, GreaterThanOrEquals=27, LessThanOrEquals=28, 
		Plus=29, Minus=30, Multiply=31, Divide=32, Number=33, Digit=34, DoubleQuotedString=35, 
		Identifier=36, WhiteSpace=37;
	public const int Instruction = 1;
	public static string[] modeNames = {
		"DEFAULT_MODE", "Instruction"
	};

	public static readonly string[] ruleNames = {
		"SingleInstructionComment", "OutputInstructionStart", "ControlInstructionStart", 
		"Fluff", "InstructionEnd", "If", "EndIf", "End", "Else", "ElseIf", "For", 
		"In", "EndFor", "Null", "MemberAccessOperator", "Pipe", "CommaSeparator", 
		"LeftParen", "RightParen", "And", "Or", "Not", "Equals", "NotEquals", 
		"GreaterThan", "LessThan", "GreaterThanOrEquals", "LessThanOrEquals", 
		"Plus", "Minus", "Multiply", "Divide", "Number", "Digit", "DoubleQuotedString", 
		"Identifier", "WhiteSpace"
	};


	public QuokkaLex(ICharStream input)
		: base(input)
	{
		Interpreter = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, "'${'", "'@{'", null, "'}'", null, null, null, null, null, 
		null, null, null, null, "'.'", "'|'", "','", "'('", "')'", null, null, 
		null, "'='", "'!='", "'>'", "'<'", "'>='", "'<='", "'+'", "'-'", "'*'", 
		"'/'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "SingleInstructionComment", "OutputInstructionStart", "ControlInstructionStart", 
		"Fluff", "InstructionEnd", "If", "EndIf", "End", "Else", "ElseIf", "For", 
		"In", "EndFor", "Null", "MemberAccessOperator", "Pipe", "CommaSeparator", 
		"LeftParen", "RightParen", "And", "Or", "Not", "Equals", "NotEquals", 
		"GreaterThan", "LessThan", "GreaterThanOrEquals", "LessThanOrEquals", 
		"Plus", "Minus", "Multiply", "Divide", "Number", "Digit", "DoubleQuotedString", 
		"Identifier", "WhiteSpace"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "QuokkaLex.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x2\'\xFB\b\x1\b\x1"+
		"\x4\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b"+
		"\t\b\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF"+
		"\x4\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x3\x2\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\a\x2T\n\x2\f\x2\xE\x2W\v\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\a\x5i\n\x5"+
		"\f\x5\xE\x5l\v\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\a\x5s\n\x5\f\x5\xE\x5"+
		"v\v\x5\x3\x5\x3\x5\x3\x5\x6\x5{\n\x5\r\x5\xE\x5|\x5\x5\x7F\n\x5\x3\x6"+
		"\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\b\x3\b\x6\b\x8A\n\b\r\b\xE\b\x8B"+
		"\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x6\v"+
		"\x9B\n\v\r\v\xE\v\x9C\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3"+
		"\xE\x3\xE\x6\xE\xAA\n\xE\r\xE\xE\xE\xAB\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF"+
		"\x3\xF\x3\xF\x3\x10\x3\x10\x3\x11\x3\x11\x3\x12\x3\x12\x3\x13\x3\x13\x3"+
		"\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x17\x3"+
		"\x17\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x1A\x3\x1A\x3"+
		"\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3"+
		"\x1F\x3\x1F\x3 \x3 \x3!\x3!\x3\"\x6\"\xE2\n\"\r\"\xE\"\xE3\x3#\x3#\x3"+
		"$\x3$\a$\xEA\n$\f$\xE$\xED\v$\x3$\x3$\x3%\x3%\a%\xF3\n%\f%\xE%\xF6\v%"+
		"\x3&\x3&\x3&\x3&\x4jt\x2\'\x4\x3\x6\x4\b\x5\n\x6\f\a\xE\b\x10\t\x12\n"+
		"\x14\v\x16\f\x18\r\x1A\xE\x1C\xF\x1E\x10 \x11\"\x12$\x13&\x14(\x15*\x16"+
		",\x17.\x18\x30\x19\x32\x1A\x34\x1B\x36\x1C\x38\x1D:\x1E<\x1F> @!\x42\""+
		"\x44#\x46$H%J&L\'\x4\x2\x3\x15\x3\x2\x7F\x7F\x4\x2&&\x42\x42\x3\x2}}\x4"+
		"\x2KKkk\x4\x2HHhh\x4\x2GGgg\x4\x2PPpp\x4\x2\x46\x46\x66\x66\x4\x2NNnn"+
		"\x4\x2UUuu\x4\x2QQqq\x4\x2TTtt\x4\x2WWww\x4\x2\x43\x43\x63\x63\x4\x2V"+
		"Vvv\x3\x2$$\x5\x2\x43\\\x61\x61\x63|\x6\x2\x32;\x43\\\x61\x61\x63|\x5"+
		"\x2\v\f\xF\xF\"\"\x106\x2\x4\x3\x2\x2\x2\x2\x6\x3\x2\x2\x2\x2\b\x3\x2"+
		"\x2\x2\x2\n\x3\x2\x2\x2\x3\f\x3\x2\x2\x2\x3\xE\x3\x2\x2\x2\x3\x10\x3\x2"+
		"\x2\x2\x3\x12\x3\x2\x2\x2\x3\x14\x3\x2\x2\x2\x3\x16\x3\x2\x2\x2\x3\x18"+
		"\x3\x2\x2\x2\x3\x1A\x3\x2\x2\x2\x3\x1C\x3\x2\x2\x2\x3\x1E\x3\x2\x2\x2"+
		"\x3 \x3\x2\x2\x2\x3\"\x3\x2\x2\x2\x3$\x3\x2\x2\x2\x3&\x3\x2\x2\x2\x3("+
		"\x3\x2\x2\x2\x3*\x3\x2\x2\x2\x3,\x3\x2\x2\x2\x3.\x3\x2\x2\x2\x3\x30\x3"+
		"\x2\x2\x2\x3\x32\x3\x2\x2\x2\x3\x34\x3\x2\x2\x2\x3\x36\x3\x2\x2\x2\x3"+
		"\x38\x3\x2\x2\x2\x3:\x3\x2\x2\x2\x3<\x3\x2\x2\x2\x3>\x3\x2\x2\x2\x3@\x3"+
		"\x2\x2\x2\x3\x42\x3\x2\x2\x2\x3\x44\x3\x2\x2\x2\x3\x46\x3\x2\x2\x2\x3"+
		"H\x3\x2\x2\x2\x3J\x3\x2\x2\x2\x3L\x3\x2\x2\x2\x4N\x3\x2\x2\x2\x6[\x3\x2"+
		"\x2\x2\b`\x3\x2\x2\x2\n~\x3\x2\x2\x2\f\x80\x3\x2\x2\x2\xE\x84\x3\x2\x2"+
		"\x2\x10\x87\x3\x2\x2\x2\x12\x8F\x3\x2\x2\x2\x14\x93\x3\x2\x2\x2\x16\x98"+
		"\x3\x2\x2\x2\x18\xA0\x3\x2\x2\x2\x1A\xA4\x3\x2\x2\x2\x1C\xA7\x3\x2\x2"+
		"\x2\x1E\xAF\x3\x2\x2\x2 \xB4\x3\x2\x2\x2\"\xB6\x3\x2\x2\x2$\xB8\x3\x2"+
		"\x2\x2&\xBA\x3\x2\x2\x2(\xBC\x3\x2\x2\x2*\xBE\x3\x2\x2\x2,\xC2\x3\x2\x2"+
		"\x2.\xC5\x3\x2\x2\x2\x30\xC9\x3\x2\x2\x2\x32\xCB\x3\x2\x2\x2\x34\xCE\x3"+
		"\x2\x2\x2\x36\xD0\x3\x2\x2\x2\x38\xD2\x3\x2\x2\x2:\xD5\x3\x2\x2\x2<\xD8"+
		"\x3\x2\x2\x2>\xDA\x3\x2\x2\x2@\xDC\x3\x2\x2\x2\x42\xDE\x3\x2\x2\x2\x44"+
		"\xE1\x3\x2\x2\x2\x46\xE5\x3\x2\x2\x2H\xE7\x3\x2\x2\x2J\xF0\x3\x2\x2\x2"+
		"L\xF7\x3\x2\x2\x2NO\a\x42\x2\x2OP\a}\x2\x2PQ\a,\x2\x2QU\x3\x2\x2\x2RT"+
		"\n\x2\x2\x2SR\x3\x2\x2\x2TW\x3\x2\x2\x2US\x3\x2\x2\x2UV\x3\x2\x2\x2VX"+
		"\x3\x2\x2\x2WU\x3\x2\x2\x2XY\a,\x2\x2YZ\a\x7F\x2\x2Z\x5\x3\x2\x2\x2[\\"+
		"\a&\x2\x2\\]\a}\x2\x2]^\x3\x2\x2\x2^_\b\x3\x2\x2_\a\x3\x2\x2\x2`\x61\a"+
		"\x42\x2\x2\x61\x62\a}\x2\x2\x62\x63\x3\x2\x2\x2\x63\x64\b\x4\x2\x2\x64"+
		"\t\x3\x2\x2\x2\x65\x66\t\x3\x2\x2\x66j\n\x4\x2\x2gi\n\x3\x2\x2hg\x3\x2"+
		"\x2\x2il\x3\x2\x2\x2jk\x3\x2\x2\x2jh\x3\x2\x2\x2km\x3\x2\x2\x2lj\x3\x2"+
		"\x2\x2m\x7F\a\x7F\x2\x2no\t\x3\x2\x2op\a}\x2\x2pt\a}\x2\x2qs\n\x3\x2\x2"+
		"rq\x3\x2\x2\x2sv\x3\x2\x2\x2tu\x3\x2\x2\x2tr\x3\x2\x2\x2uw\x3\x2\x2\x2"+
		"vt\x3\x2\x2\x2w\x7F\a\x7F\x2\x2x\x7F\t\x3\x2\x2y{\n\x3\x2\x2zy\x3\x2\x2"+
		"\x2{|\x3\x2\x2\x2|z\x3\x2\x2\x2|}\x3\x2\x2\x2}\x7F\x3\x2\x2\x2~\x65\x3"+
		"\x2\x2\x2~n\x3\x2\x2\x2~x\x3\x2\x2\x2~z\x3\x2\x2\x2\x7F\v\x3\x2\x2\x2"+
		"\x80\x81\a\x7F\x2\x2\x81\x82\x3\x2\x2\x2\x82\x83\b\x6\x3\x2\x83\r\x3\x2"+
		"\x2\x2\x84\x85\t\x5\x2\x2\x85\x86\t\x6\x2\x2\x86\xF\x3\x2\x2\x2\x87\x89"+
		"\x5\x12\t\x2\x88\x8A\x5L&\x2\x89\x88\x3\x2\x2\x2\x8A\x8B\x3\x2\x2\x2\x8B"+
		"\x89\x3\x2\x2\x2\x8B\x8C\x3\x2\x2\x2\x8C\x8D\x3\x2\x2\x2\x8D\x8E\x5\xE"+
		"\a\x2\x8E\x11\x3\x2\x2\x2\x8F\x90\t\a\x2\x2\x90\x91\t\b\x2\x2\x91\x92"+
		"\t\t\x2\x2\x92\x13\x3\x2\x2\x2\x93\x94\t\a\x2\x2\x94\x95\t\n\x2\x2\x95"+
		"\x96\t\v\x2\x2\x96\x97\t\a\x2\x2\x97\x15\x3\x2\x2\x2\x98\x9A\x5\x14\n"+
		"\x2\x99\x9B\x5L&\x2\x9A\x99\x3\x2\x2\x2\x9B\x9C\x3\x2\x2\x2\x9C\x9A\x3"+
		"\x2\x2\x2\x9C\x9D\x3\x2\x2\x2\x9D\x9E\x3\x2\x2\x2\x9E\x9F\x5\xE\a\x2\x9F"+
		"\x17\x3\x2\x2\x2\xA0\xA1\t\x6\x2\x2\xA1\xA2\t\f\x2\x2\xA2\xA3\t\r\x2\x2"+
		"\xA3\x19\x3\x2\x2\x2\xA4\xA5\t\x5\x2\x2\xA5\xA6\t\b\x2\x2\xA6\x1B\x3\x2"+
		"\x2\x2\xA7\xA9\x5\x12\t\x2\xA8\xAA\x5L&\x2\xA9\xA8\x3\x2\x2\x2\xAA\xAB"+
		"\x3\x2\x2\x2\xAB\xA9\x3\x2\x2\x2\xAB\xAC\x3\x2\x2\x2\xAC\xAD\x3\x2\x2"+
		"\x2\xAD\xAE\x5\x18\f\x2\xAE\x1D\x3\x2\x2\x2\xAF\xB0\t\b\x2\x2\xB0\xB1"+
		"\t\xE\x2\x2\xB1\xB2\t\n\x2\x2\xB2\xB3\t\n\x2\x2\xB3\x1F\x3\x2\x2\x2\xB4"+
		"\xB5\a\x30\x2\x2\xB5!\x3\x2\x2\x2\xB6\xB7\a~\x2\x2\xB7#\x3\x2\x2\x2\xB8"+
		"\xB9\a.\x2\x2\xB9%\x3\x2\x2\x2\xBA\xBB\a*\x2\x2\xBB\'\x3\x2\x2\x2\xBC"+
		"\xBD\a+\x2\x2\xBD)\x3\x2\x2\x2\xBE\xBF\t\xF\x2\x2\xBF\xC0\t\b\x2\x2\xC0"+
		"\xC1\t\t\x2\x2\xC1+\x3\x2\x2\x2\xC2\xC3\t\f\x2\x2\xC3\xC4\t\r\x2\x2\xC4"+
		"-\x3\x2\x2\x2\xC5\xC6\t\b\x2\x2\xC6\xC7\t\f\x2\x2\xC7\xC8\t\x10\x2\x2"+
		"\xC8/\x3\x2\x2\x2\xC9\xCA\a?\x2\x2\xCA\x31\x3\x2\x2\x2\xCB\xCC\a#\x2\x2"+
		"\xCC\xCD\a?\x2\x2\xCD\x33\x3\x2\x2\x2\xCE\xCF\a@\x2\x2\xCF\x35\x3\x2\x2"+
		"\x2\xD0\xD1\a>\x2\x2\xD1\x37\x3\x2\x2\x2\xD2\xD3\a@\x2\x2\xD3\xD4\a?\x2"+
		"\x2\xD4\x39\x3\x2\x2\x2\xD5\xD6\a>\x2\x2\xD6\xD7\a?\x2\x2\xD7;\x3\x2\x2"+
		"\x2\xD8\xD9\a-\x2\x2\xD9=\x3\x2\x2\x2\xDA\xDB\a/\x2\x2\xDB?\x3\x2\x2\x2"+
		"\xDC\xDD\a,\x2\x2\xDD\x41\x3\x2\x2\x2\xDE\xDF\a\x31\x2\x2\xDF\x43\x3\x2"+
		"\x2\x2\xE0\xE2\x5\x46#\x2\xE1\xE0\x3\x2\x2\x2\xE2\xE3\x3\x2\x2\x2\xE3"+
		"\xE1\x3\x2\x2\x2\xE3\xE4\x3\x2\x2\x2\xE4\x45\x3\x2\x2\x2\xE5\xE6\x4\x32"+
		";\x2\xE6G\x3\x2\x2\x2\xE7\xEB\a$\x2\x2\xE8\xEA\n\x11\x2\x2\xE9\xE8\x3"+
		"\x2\x2\x2\xEA\xED\x3\x2\x2\x2\xEB\xE9\x3\x2\x2\x2\xEB\xEC\x3\x2\x2\x2"+
		"\xEC\xEE\x3\x2\x2\x2\xED\xEB\x3\x2\x2\x2\xEE\xEF\a$\x2\x2\xEFI\x3\x2\x2"+
		"\x2\xF0\xF4\t\x12\x2\x2\xF1\xF3\t\x13\x2\x2\xF2\xF1\x3\x2\x2\x2\xF3\xF6"+
		"\x3\x2\x2\x2\xF4\xF2\x3\x2\x2\x2\xF4\xF5\x3\x2\x2\x2\xF5K\x3\x2\x2\x2"+
		"\xF6\xF4\x3\x2\x2\x2\xF7\xF8\t\x14\x2\x2\xF8\xF9\x3\x2\x2\x2\xF9\xFA\b"+
		"&\x4\x2\xFAM\x3\x2\x2\x2\xF\x2\x3Ujt|~\x8B\x9C\xAB\xE3\xEB\xF4\x5\a\x3"+
		"\x2\x6\x2\x2\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Quokka.Generated
