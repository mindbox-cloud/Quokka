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

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Grammar\Quokka\QuokkaHtmlLex.g4 by ANTLR 4.7

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Mindbox.Quokka.Generated {
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7")]
[System.CLSCompliant(false)]
internal partial class QuokkaHtmlLex : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		HtmlComment=1, HtmlDtd=2, CDATA=3, LeftAngularBracket=4, OutputBlock=5, 
		Fluff=6, RightAngularBracket=7, Slash=8, TAG_EQUALS=9, TAG_NAME=10, TAG_WHITESPACE=11, 
		SCRIPT_BODY=12, SCRIPT_SHORT_BODY=13, STYLE_BODY=14, STYLE_SHORT_BODY=15, 
		UNQUOTED_ATTRIBUTE=16, OpeningDoubleQuotes=17, OpeningSingleQuotes=18, 
		UnquotedOutputBlock=19, ClosingDoubleQuotes=20, DQS_OUTPUTBLOCK=21, DQS_FLUFF=22, 
		ClosingSingleQuotes=23, SQS_OUTPUTBLOCK=24, SQS_FLUFF=25;
	public const int
		InsideTag=1, SCRIPT=2, STYLE=3, ATTVALUE=4, DoubleQuotes=5, SingleQuotes=6;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE", "InsideTag", "SCRIPT", "STYLE", "ATTVALUE", "DoubleQuotes", 
		"SingleQuotes"
	};

	public static readonly string[] ruleNames = {
		"HtmlComment", "HtmlDtd", "CDATA", "LeftAngularBracket", "OutputBlock", 
		"Fluff", "RightAngularBracket", "Slash", "TAG_EQUALS", "TAG_NAME", "TAG_WHITESPACE", 
		"HEXDIGIT", "DIGIT", "TAG_NameChar", "TAG_NameStartChar", "SCRIPT_BODY", 
		"SCRIPT_SHORT_BODY", "STYLE_BODY", "STYLE_SHORT_BODY", "UNQUOTED_ATTRIBUTE", 
		"ATTCHAR", "ATTCHARS", "HEXCHARS", "DECCHARS", "OpeningDoubleQuotes", 
		"OpeningSingleQuotes", "UnquotedOutputBlock", "ClosingDoubleQuotes", "DQS_OUTPUTBLOCK", 
		"DQS_FLUFF", "ClosingSingleQuotes", "SQS_OUTPUTBLOCK", "SQS_FLUFF"
	};


	public QuokkaHtmlLex(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public QuokkaHtmlLex(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, "'<'", null, null, "'>'", "'/'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "HtmlComment", "HtmlDtd", "CDATA", "LeftAngularBracket", "OutputBlock", 
		"Fluff", "RightAngularBracket", "Slash", "TAG_EQUALS", "TAG_NAME", "TAG_WHITESPACE", 
		"SCRIPT_BODY", "SCRIPT_SHORT_BODY", "STYLE_BODY", "STYLE_SHORT_BODY", 
		"UNQUOTED_ATTRIBUTE", "OpeningDoubleQuotes", "OpeningSingleQuotes", "UnquotedOutputBlock", 
		"ClosingDoubleQuotes", "DQS_OUTPUTBLOCK", "DQS_FLUFF", "ClosingSingleQuotes", 
		"SQS_OUTPUTBLOCK", "SQS_FLUFF"
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

	public override string GrammarFileName { get { return "QuokkaHtmlLex.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static QuokkaHtmlLex() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x1B', '\x190', '\b', '\x1', '\b', '\x1', '\b', '\x1', 
		'\b', '\x1', '\b', '\x1', '\b', '\x1', '\b', '\x1', '\x4', '\x2', '\t', 
		'\x2', '\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', 
		'\t', '\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', 
		'\b', '\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', 
		'\v', '\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', 
		'\xE', '\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', 
		'\x4', '\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', 
		'\t', '\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', 
		'\x4', '\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', 
		'\t', '\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', 
		'\x4', '\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', 
		'\t', '\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', 
		'\x4', ' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', 
		'\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', 
		'\x3', '\x2', '\a', '\x2', 'R', '\n', '\x2', '\f', '\x2', '\xE', '\x2', 
		'U', '\v', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\a', '\x3', '_', 
		'\n', '\x3', '\f', '\x3', '\xE', '\x3', '\x62', '\v', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', 
		'\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', 
		'\x3', '\x4', '\x3', '\x4', '\a', '\x4', 'q', '\n', '\x4', '\f', '\x4', 
		'\xE', '\x4', 't', '\v', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', 
		'\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\a', '\x6', '\x82', 
		'\n', '\x6', '\f', '\x6', '\xE', '\x6', '\x85', '\v', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\a', '\a', '\x8C', 
		'\n', '\a', '\f', '\a', '\xE', '\a', '\x8F', '\v', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\a', '\a', '\x96', 
		'\n', '\a', '\f', '\a', '\xE', '\a', '\x99', '\v', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x6', '\a', '\x9E', '\n', '\a', '\r', '\a', 
		'\xE', '\a', '\x9F', '\x5', '\a', '\xA2', '\n', '\a', '\x3', '\b', '\x3', 
		'\b', '\x3', '\b', '\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\n', 
		'\x3', '\n', '\a', '\n', '\xAC', '\n', '\n', '\f', '\n', '\xE', '\n', 
		'\xAF', '\v', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\v', '\x3', '\v', 
		'\a', '\v', '\xB5', '\n', '\v', '\f', '\v', '\xE', '\v', '\xB8', '\v', 
		'\v', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\r', 
		'\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\xF', '\x5', '\xF', '\xC6', '\n', '\xF', '\x3', '\x10', 
		'\x5', '\x10', '\xC9', '\n', '\x10', '\x3', '\x11', '\a', '\x11', '\xCC', 
		'\n', '\x11', '\f', '\x11', '\xE', '\x11', '\xCF', '\v', '\x11', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x12', '\a', '\x12', '\xDE', '\n', '\x12', 
		'\f', '\x12', '\xE', '\x12', '\xE1', '\v', '\x12', '\x3', '\x12', '\x3', 
		'\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', 
		'\x13', '\a', '\x13', '\xEA', '\n', '\x13', '\f', '\x13', '\xE', '\x13', 
		'\xED', '\v', '\x13', '\x3', '\x13', '\x3', '\x13', '\x3', '\x13', '\x3', 
		'\x13', '\x3', '\x13', '\x3', '\x13', '\x3', '\x13', '\x3', '\x13', '\x3', 
		'\x13', '\x3', '\x13', '\x3', '\x13', '\x3', '\x14', '\a', '\x14', '\xFB', 
		'\n', '\x14', '\f', '\x14', '\xE', '\x14', '\xFE', '\v', '\x14', '\x3', 
		'\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', 
		'\x14', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x5', '\x15', '\x109', 
		'\n', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x16', '\x5', '\x16', 
		'\x10E', '\n', '\x16', '\x3', '\x17', '\x6', '\x17', '\x111', '\n', '\x17', 
		'\r', '\x17', '\xE', '\x17', '\x112', '\x3', '\x17', '\x5', '\x17', '\x116', 
		'\n', '\x17', '\x3', '\x18', '\x3', '\x18', '\x6', '\x18', '\x11A', '\n', 
		'\x18', '\r', '\x18', '\xE', '\x18', '\x11B', '\x3', '\x19', '\x6', '\x19', 
		'\x11F', '\n', '\x19', '\r', '\x19', '\xE', '\x19', '\x120', '\x3', '\x19', 
		'\x5', '\x19', '\x124', '\n', '\x19', '\x3', '\x1A', '\x3', '\x1A', '\x3', 
		'\x1A', '\x3', '\x1A', '\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1B', '\x3', 
		'\x1B', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\a', 
		'\x1C', '\x132', '\n', '\x1C', '\f', '\x1C', '\xE', '\x1C', '\x135', '\v', 
		'\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', 
		'\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', 
		'\x1E', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1E', '\a', '\x1E', '\x144', 
		'\n', '\x1E', '\f', '\x1E', '\xE', '\x1E', '\x147', '\v', '\x1E', '\x3', 
		'\x1E', '\x3', '\x1E', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\a', 
		'\x1F', '\x14E', '\n', '\x1F', '\f', '\x1F', '\xE', '\x1F', '\x151', '\v', 
		'\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', 
		'\x1F', '\a', '\x1F', '\x158', '\n', '\x1F', '\f', '\x1F', '\xE', '\x1F', 
		'\x15B', '\v', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x6', 
		'\x1F', '\x160', '\n', '\x1F', '\r', '\x1F', '\xE', '\x1F', '\x161', '\x5', 
		'\x1F', '\x164', '\n', '\x1F', '\x3', ' ', '\x3', ' ', '\x3', ' ', '\x3', 
		' ', '\x3', ' ', '\x3', '!', '\x3', '!', '\x3', '!', '\x3', '!', '\a', 
		'!', '\x16F', '\n', '!', '\f', '!', '\xE', '!', '\x172', '\v', '!', '\x3', 
		'!', '\x3', '!', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\a', '\"', '\x179', 
		'\n', '\"', '\f', '\"', '\xE', '\"', '\x17C', '\v', '\"', '\x3', '\"', 
		'\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\a', '\"', '\x183', 
		'\n', '\"', '\f', '\"', '\xE', '\"', '\x186', '\v', '\"', '\x3', '\"', 
		'\x3', '\"', '\x3', '\"', '\x6', '\"', '\x18B', '\n', '\"', '\r', '\"', 
		'\xE', '\"', '\x18C', '\x5', '\"', '\x18F', '\n', '\"', '\x10', 'S', '`', 
		'r', '\x83', '\x8D', '\x97', '\xCD', '\xDF', '\xEB', '\xFC', '\x14F', 
		'\x159', '\x17A', '\x184', '\x2', '#', '\t', '\x3', '\v', '\x4', '\r', 
		'\x5', '\xF', '\x6', '\x11', '\a', '\x13', '\b', '\x15', '\t', '\x17', 
		'\n', '\x19', '\v', '\x1B', '\f', '\x1D', '\r', '\x1F', '\x2', '!', '\x2', 
		'#', '\x2', '%', '\x2', '\'', '\xE', ')', '\xF', '+', '\x10', '-', '\x11', 
		'/', '\x12', '\x31', '\x2', '\x33', '\x2', '\x35', '\x2', '\x37', '\x2', 
		'\x39', '\x13', ';', '\x14', '=', '\x15', '?', '\x16', '\x41', '\x17', 
		'\x43', '\x18', '\x45', '\x19', 'G', '\x1A', 'I', '\x1B', '\t', '\x2', 
		'\x3', '\x4', '\x5', '\x6', '\a', '\b', '\xF', '\x3', '\x2', '&', '&', 
		'\x3', '\x2', '}', '}', '\x4', '\x2', '&', '&', '>', '>', '\x5', '\x2', 
		'\v', '\f', '\xF', '\xF', '\"', '\"', '\x5', '\x2', '\x32', ';', '\x43', 
		'H', '\x63', 'h', '\x3', '\x2', '\x32', ';', '\x4', '\x2', '/', '\x30', 
		'\x61', '\x61', '\x5', '\x2', '\xB9', '\xB9', '\x302', '\x371', '\x2041', 
		'\x2042', '\n', '\x2', '<', '<', '\x43', '\\', '\x63', '|', '\x2072', 
		'\x2191', '\x2C02', '\x2FF1', '\x3003', '\xD801', '\xF902', '\xFDD1', 
		'\xFDF2', '\xFFFF', '\t', '\x2', '%', '%', '-', '=', '?', '?', '\x41', 
		'\x41', '\x43', '\\', '\x61', '\x61', '\x63', '|', '\x3', '\x2', '\x7F', 
		'\x7F', '\x4', '\x2', '$', '$', '&', '&', '\x4', '\x2', '&', '&', ')', 
		')', '\x2', '\x1AA', '\x2', '\t', '\x3', '\x2', '\x2', '\x2', '\x2', '\v', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\r', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\xF', '\x3', '\x2', '\x2', '\x2', '\x2', '\x11', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x13', '\x3', '\x2', '\x2', '\x2', '\x3', '\x15', '\x3', 
		'\x2', '\x2', '\x2', '\x3', '\x17', '\x3', '\x2', '\x2', '\x2', '\x3', 
		'\x19', '\x3', '\x2', '\x2', '\x2', '\x3', '\x1B', '\x3', '\x2', '\x2', 
		'\x2', '\x3', '\x1D', '\x3', '\x2', '\x2', '\x2', '\x4', '\'', '\x3', 
		'\x2', '\x2', '\x2', '\x4', ')', '\x3', '\x2', '\x2', '\x2', '\x5', '+', 
		'\x3', '\x2', '\x2', '\x2', '\x5', '-', '\x3', '\x2', '\x2', '\x2', '\x6', 
		'/', '\x3', '\x2', '\x2', '\x2', '\x6', '\x39', '\x3', '\x2', '\x2', '\x2', 
		'\x6', ';', '\x3', '\x2', '\x2', '\x2', '\x6', '=', '\x3', '\x2', '\x2', 
		'\x2', '\a', '?', '\x3', '\x2', '\x2', '\x2', '\a', '\x41', '\x3', '\x2', 
		'\x2', '\x2', '\a', '\x43', '\x3', '\x2', '\x2', '\x2', '\b', '\x45', 
		'\x3', '\x2', '\x2', '\x2', '\b', 'G', '\x3', '\x2', '\x2', '\x2', '\b', 
		'I', '\x3', '\x2', '\x2', '\x2', '\t', 'K', '\x3', '\x2', '\x2', '\x2', 
		'\v', 'Z', '\x3', '\x2', '\x2', '\x2', '\r', '\x65', '\x3', '\x2', '\x2', 
		'\x2', '\xF', 'y', '\x3', '\x2', '\x2', '\x2', '\x11', '}', '\x3', '\x2', 
		'\x2', '\x2', '\x13', '\xA1', '\x3', '\x2', '\x2', '\x2', '\x15', '\xA3', 
		'\x3', '\x2', '\x2', '\x2', '\x17', '\xA7', '\x3', '\x2', '\x2', '\x2', 
		'\x19', '\xA9', '\x3', '\x2', '\x2', '\x2', '\x1B', '\xB2', '\x3', '\x2', 
		'\x2', '\x2', '\x1D', '\xB9', '\x3', '\x2', '\x2', '\x2', '\x1F', '\xBD', 
		'\x3', '\x2', '\x2', '\x2', '!', '\xBF', '\x3', '\x2', '\x2', '\x2', '#', 
		'\xC5', '\x3', '\x2', '\x2', '\x2', '%', '\xC8', '\x3', '\x2', '\x2', 
		'\x2', '\'', '\xCD', '\x3', '\x2', '\x2', '\x2', ')', '\xDF', '\x3', '\x2', 
		'\x2', '\x2', '+', '\xEB', '\x3', '\x2', '\x2', '\x2', '-', '\xFC', '\x3', 
		'\x2', '\x2', '\x2', '/', '\x108', '\x3', '\x2', '\x2', '\x2', '\x31', 
		'\x10D', '\x3', '\x2', '\x2', '\x2', '\x33', '\x110', '\x3', '\x2', '\x2', 
		'\x2', '\x35', '\x117', '\x3', '\x2', '\x2', '\x2', '\x37', '\x11E', '\x3', 
		'\x2', '\x2', '\x2', '\x39', '\x125', '\x3', '\x2', '\x2', '\x2', ';', 
		'\x129', '\x3', '\x2', '\x2', '\x2', '=', '\x12D', '\x3', '\x2', '\x2', 
		'\x2', '?', '\x13A', '\x3', '\x2', '\x2', '\x2', '\x41', '\x13F', '\x3', 
		'\x2', '\x2', '\x2', '\x43', '\x163', '\x3', '\x2', '\x2', '\x2', '\x45', 
		'\x165', '\x3', '\x2', '\x2', '\x2', 'G', '\x16A', '\x3', '\x2', '\x2', 
		'\x2', 'I', '\x18E', '\x3', '\x2', '\x2', '\x2', 'K', 'L', '\a', '>', 
		'\x2', '\x2', 'L', 'M', '\a', '#', '\x2', '\x2', 'M', 'N', '\a', '/', 
		'\x2', '\x2', 'N', 'O', '\a', '/', '\x2', '\x2', 'O', 'S', '\x3', '\x2', 
		'\x2', '\x2', 'P', 'R', '\v', '\x2', '\x2', '\x2', 'Q', 'P', '\x3', '\x2', 
		'\x2', '\x2', 'R', 'U', '\x3', '\x2', '\x2', '\x2', 'S', 'T', '\x3', '\x2', 
		'\x2', '\x2', 'S', 'Q', '\x3', '\x2', '\x2', '\x2', 'T', 'V', '\x3', '\x2', 
		'\x2', '\x2', 'U', 'S', '\x3', '\x2', '\x2', '\x2', 'V', 'W', '\a', '/', 
		'\x2', '\x2', 'W', 'X', '\a', '/', '\x2', '\x2', 'X', 'Y', '\a', '@', 
		'\x2', '\x2', 'Y', '\n', '\x3', '\x2', '\x2', '\x2', 'Z', '[', '\a', '>', 
		'\x2', '\x2', '[', '\\', '\a', '#', '\x2', '\x2', '\\', '`', '\x3', '\x2', 
		'\x2', '\x2', ']', '_', '\v', '\x2', '\x2', '\x2', '^', ']', '\x3', '\x2', 
		'\x2', '\x2', '_', '\x62', '\x3', '\x2', '\x2', '\x2', '`', '\x61', '\x3', 
		'\x2', '\x2', '\x2', '`', '^', '\x3', '\x2', '\x2', '\x2', '\x61', '\x63', 
		'\x3', '\x2', '\x2', '\x2', '\x62', '`', '\x3', '\x2', '\x2', '\x2', '\x63', 
		'\x64', '\a', '@', '\x2', '\x2', '\x64', '\f', '\x3', '\x2', '\x2', '\x2', 
		'\x65', '\x66', '\a', '>', '\x2', '\x2', '\x66', 'g', '\a', '#', '\x2', 
		'\x2', 'g', 'h', '\a', ']', '\x2', '\x2', 'h', 'i', '\a', '\x45', '\x2', 
		'\x2', 'i', 'j', '\a', '\x46', '\x2', '\x2', 'j', 'k', '\a', '\x43', '\x2', 
		'\x2', 'k', 'l', '\a', 'V', '\x2', '\x2', 'l', 'm', '\a', '\x43', '\x2', 
		'\x2', 'm', 'n', '\a', ']', '\x2', '\x2', 'n', 'r', '\x3', '\x2', '\x2', 
		'\x2', 'o', 'q', '\v', '\x2', '\x2', '\x2', 'p', 'o', '\x3', '\x2', '\x2', 
		'\x2', 'q', 't', '\x3', '\x2', '\x2', '\x2', 'r', 's', '\x3', '\x2', '\x2', 
		'\x2', 'r', 'p', '\x3', '\x2', '\x2', '\x2', 's', 'u', '\x3', '\x2', '\x2', 
		'\x2', 't', 'r', '\x3', '\x2', '\x2', '\x2', 'u', 'v', '\a', '_', '\x2', 
		'\x2', 'v', 'w', '\a', '_', '\x2', '\x2', 'w', 'x', '\a', '@', '\x2', 
		'\x2', 'x', '\xE', '\x3', '\x2', '\x2', '\x2', 'y', 'z', '\a', '>', '\x2', 
		'\x2', 'z', '{', '\x3', '\x2', '\x2', '\x2', '{', '|', '\b', '\x5', '\x2', 
		'\x2', '|', '\x10', '\x3', '\x2', '\x2', '\x2', '}', '~', '\a', '&', '\x2', 
		'\x2', '~', '\x7F', '\a', '}', '\x2', '\x2', '\x7F', '\x83', '\x3', '\x2', 
		'\x2', '\x2', '\x80', '\x82', '\v', '\x2', '\x2', '\x2', '\x81', '\x80', 
		'\x3', '\x2', '\x2', '\x2', '\x82', '\x85', '\x3', '\x2', '\x2', '\x2', 
		'\x83', '\x84', '\x3', '\x2', '\x2', '\x2', '\x83', '\x81', '\x3', '\x2', 
		'\x2', '\x2', '\x84', '\x86', '\x3', '\x2', '\x2', '\x2', '\x85', '\x83', 
		'\x3', '\x2', '\x2', '\x2', '\x86', '\x87', '\a', '\x7F', '\x2', '\x2', 
		'\x87', '\x12', '\x3', '\x2', '\x2', '\x2', '\x88', '\x89', '\t', '\x2', 
		'\x2', '\x2', '\x89', '\x8D', '\n', '\x3', '\x2', '\x2', '\x8A', '\x8C', 
		'\n', '\x2', '\x2', '\x2', '\x8B', '\x8A', '\x3', '\x2', '\x2', '\x2', 
		'\x8C', '\x8F', '\x3', '\x2', '\x2', '\x2', '\x8D', '\x8E', '\x3', '\x2', 
		'\x2', '\x2', '\x8D', '\x8B', '\x3', '\x2', '\x2', '\x2', '\x8E', '\x90', 
		'\x3', '\x2', '\x2', '\x2', '\x8F', '\x8D', '\x3', '\x2', '\x2', '\x2', 
		'\x90', '\xA2', '\a', '\x7F', '\x2', '\x2', '\x91', '\x92', '\t', '\x2', 
		'\x2', '\x2', '\x92', '\x93', '\a', '}', '\x2', '\x2', '\x93', '\x97', 
		'\a', '}', '\x2', '\x2', '\x94', '\x96', '\n', '\x2', '\x2', '\x2', '\x95', 
		'\x94', '\x3', '\x2', '\x2', '\x2', '\x96', '\x99', '\x3', '\x2', '\x2', 
		'\x2', '\x97', '\x98', '\x3', '\x2', '\x2', '\x2', '\x97', '\x95', '\x3', 
		'\x2', '\x2', '\x2', '\x98', '\x9A', '\x3', '\x2', '\x2', '\x2', '\x99', 
		'\x97', '\x3', '\x2', '\x2', '\x2', '\x9A', '\xA2', '\a', '\x7F', '\x2', 
		'\x2', '\x9B', '\xA2', '\t', '\x2', '\x2', '\x2', '\x9C', '\x9E', '\n', 
		'\x4', '\x2', '\x2', '\x9D', '\x9C', '\x3', '\x2', '\x2', '\x2', '\x9E', 
		'\x9F', '\x3', '\x2', '\x2', '\x2', '\x9F', '\x9D', '\x3', '\x2', '\x2', 
		'\x2', '\x9F', '\xA0', '\x3', '\x2', '\x2', '\x2', '\xA0', '\xA2', '\x3', 
		'\x2', '\x2', '\x2', '\xA1', '\x88', '\x3', '\x2', '\x2', '\x2', '\xA1', 
		'\x91', '\x3', '\x2', '\x2', '\x2', '\xA1', '\x9B', '\x3', '\x2', '\x2', 
		'\x2', '\xA1', '\x9D', '\x3', '\x2', '\x2', '\x2', '\xA2', '\x14', '\x3', 
		'\x2', '\x2', '\x2', '\xA3', '\xA4', '\a', '@', '\x2', '\x2', '\xA4', 
		'\xA5', '\x3', '\x2', '\x2', '\x2', '\xA5', '\xA6', '\b', '\b', '\x3', 
		'\x2', '\xA6', '\x16', '\x3', '\x2', '\x2', '\x2', '\xA7', '\xA8', '\a', 
		'\x31', '\x2', '\x2', '\xA8', '\x18', '\x3', '\x2', '\x2', '\x2', '\xA9', 
		'\xAD', '\a', '?', '\x2', '\x2', '\xAA', '\xAC', '\t', '\x5', '\x2', '\x2', 
		'\xAB', '\xAA', '\x3', '\x2', '\x2', '\x2', '\xAC', '\xAF', '\x3', '\x2', 
		'\x2', '\x2', '\xAD', '\xAB', '\x3', '\x2', '\x2', '\x2', '\xAD', '\xAE', 
		'\x3', '\x2', '\x2', '\x2', '\xAE', '\xB0', '\x3', '\x2', '\x2', '\x2', 
		'\xAF', '\xAD', '\x3', '\x2', '\x2', '\x2', '\xB0', '\xB1', '\b', '\n', 
		'\x4', '\x2', '\xB1', '\x1A', '\x3', '\x2', '\x2', '\x2', '\xB2', '\xB6', 
		'\x5', '%', '\x10', '\x2', '\xB3', '\xB5', '\x5', '#', '\xF', '\x2', '\xB4', 
		'\xB3', '\x3', '\x2', '\x2', '\x2', '\xB5', '\xB8', '\x3', '\x2', '\x2', 
		'\x2', '\xB6', '\xB4', '\x3', '\x2', '\x2', '\x2', '\xB6', '\xB7', '\x3', 
		'\x2', '\x2', '\x2', '\xB7', '\x1C', '\x3', '\x2', '\x2', '\x2', '\xB8', 
		'\xB6', '\x3', '\x2', '\x2', '\x2', '\xB9', '\xBA', '\t', '\x5', '\x2', 
		'\x2', '\xBA', '\xBB', '\x3', '\x2', '\x2', '\x2', '\xBB', '\xBC', '\b', 
		'\f', '\x5', '\x2', '\xBC', '\x1E', '\x3', '\x2', '\x2', '\x2', '\xBD', 
		'\xBE', '\t', '\x6', '\x2', '\x2', '\xBE', ' ', '\x3', '\x2', '\x2', '\x2', 
		'\xBF', '\xC0', '\t', '\a', '\x2', '\x2', '\xC0', '\"', '\x3', '\x2', 
		'\x2', '\x2', '\xC1', '\xC6', '\x5', '%', '\x10', '\x2', '\xC2', '\xC6', 
		'\t', '\b', '\x2', '\x2', '\xC3', '\xC6', '\x5', '!', '\xE', '\x2', '\xC4', 
		'\xC6', '\t', '\t', '\x2', '\x2', '\xC5', '\xC1', '\x3', '\x2', '\x2', 
		'\x2', '\xC5', '\xC2', '\x3', '\x2', '\x2', '\x2', '\xC5', '\xC3', '\x3', 
		'\x2', '\x2', '\x2', '\xC5', '\xC4', '\x3', '\x2', '\x2', '\x2', '\xC6', 
		'$', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xC9', '\t', '\n', '\x2', '\x2', 
		'\xC8', '\xC7', '\x3', '\x2', '\x2', '\x2', '\xC9', '&', '\x3', '\x2', 
		'\x2', '\x2', '\xCA', '\xCC', '\v', '\x2', '\x2', '\x2', '\xCB', '\xCA', 
		'\x3', '\x2', '\x2', '\x2', '\xCC', '\xCF', '\x3', '\x2', '\x2', '\x2', 
		'\xCD', '\xCE', '\x3', '\x2', '\x2', '\x2', '\xCD', '\xCB', '\x3', '\x2', 
		'\x2', '\x2', '\xCE', '\xD0', '\x3', '\x2', '\x2', '\x2', '\xCF', '\xCD', 
		'\x3', '\x2', '\x2', '\x2', '\xD0', '\xD1', '\a', '>', '\x2', '\x2', '\xD1', 
		'\xD2', '\a', '\x31', '\x2', '\x2', '\xD2', '\xD3', '\a', 'u', '\x2', 
		'\x2', '\xD3', '\xD4', '\a', '\x65', '\x2', '\x2', '\xD4', '\xD5', '\a', 
		't', '\x2', '\x2', '\xD5', '\xD6', '\a', 'k', '\x2', '\x2', '\xD6', '\xD7', 
		'\a', 'r', '\x2', '\x2', '\xD7', '\xD8', '\a', 'v', '\x2', '\x2', '\xD8', 
		'\xD9', '\a', '@', '\x2', '\x2', '\xD9', '\xDA', '\x3', '\x2', '\x2', 
		'\x2', '\xDA', '\xDB', '\b', '\x11', '\x3', '\x2', '\xDB', '(', '\x3', 
		'\x2', '\x2', '\x2', '\xDC', '\xDE', '\v', '\x2', '\x2', '\x2', '\xDD', 
		'\xDC', '\x3', '\x2', '\x2', '\x2', '\xDE', '\xE1', '\x3', '\x2', '\x2', 
		'\x2', '\xDF', '\xE0', '\x3', '\x2', '\x2', '\x2', '\xDF', '\xDD', '\x3', 
		'\x2', '\x2', '\x2', '\xE0', '\xE2', '\x3', '\x2', '\x2', '\x2', '\xE1', 
		'\xDF', '\x3', '\x2', '\x2', '\x2', '\xE2', '\xE3', '\a', '>', '\x2', 
		'\x2', '\xE3', '\xE4', '\a', '\x31', '\x2', '\x2', '\xE4', '\xE5', '\a', 
		'@', '\x2', '\x2', '\xE5', '\xE6', '\x3', '\x2', '\x2', '\x2', '\xE6', 
		'\xE7', '\b', '\x12', '\x3', '\x2', '\xE7', '*', '\x3', '\x2', '\x2', 
		'\x2', '\xE8', '\xEA', '\v', '\x2', '\x2', '\x2', '\xE9', '\xE8', '\x3', 
		'\x2', '\x2', '\x2', '\xEA', '\xED', '\x3', '\x2', '\x2', '\x2', '\xEB', 
		'\xEC', '\x3', '\x2', '\x2', '\x2', '\xEB', '\xE9', '\x3', '\x2', '\x2', 
		'\x2', '\xEC', '\xEE', '\x3', '\x2', '\x2', '\x2', '\xED', '\xEB', '\x3', 
		'\x2', '\x2', '\x2', '\xEE', '\xEF', '\a', '>', '\x2', '\x2', '\xEF', 
		'\xF0', '\a', '\x31', '\x2', '\x2', '\xF0', '\xF1', '\a', 'u', '\x2', 
		'\x2', '\xF1', '\xF2', '\a', 'v', '\x2', '\x2', '\xF2', '\xF3', '\a', 
		'{', '\x2', '\x2', '\xF3', '\xF4', '\a', 'n', '\x2', '\x2', '\xF4', '\xF5', 
		'\a', 'g', '\x2', '\x2', '\xF5', '\xF6', '\a', '@', '\x2', '\x2', '\xF6', 
		'\xF7', '\x3', '\x2', '\x2', '\x2', '\xF7', '\xF8', '\b', '\x13', '\x3', 
		'\x2', '\xF8', ',', '\x3', '\x2', '\x2', '\x2', '\xF9', '\xFB', '\v', 
		'\x2', '\x2', '\x2', '\xFA', '\xF9', '\x3', '\x2', '\x2', '\x2', '\xFB', 
		'\xFE', '\x3', '\x2', '\x2', '\x2', '\xFC', '\xFD', '\x3', '\x2', '\x2', 
		'\x2', '\xFC', '\xFA', '\x3', '\x2', '\x2', '\x2', '\xFD', '\xFF', '\x3', 
		'\x2', '\x2', '\x2', '\xFE', '\xFC', '\x3', '\x2', '\x2', '\x2', '\xFF', 
		'\x100', '\a', '>', '\x2', '\x2', '\x100', '\x101', '\a', '\x31', '\x2', 
		'\x2', '\x101', '\x102', '\a', '@', '\x2', '\x2', '\x102', '\x103', '\x3', 
		'\x2', '\x2', '\x2', '\x103', '\x104', '\b', '\x14', '\x3', '\x2', '\x104', 
		'.', '\x3', '\x2', '\x2', '\x2', '\x105', '\x109', '\x5', '\x33', '\x17', 
		'\x2', '\x106', '\x109', '\x5', '\x35', '\x18', '\x2', '\x107', '\x109', 
		'\x5', '\x37', '\x19', '\x2', '\x108', '\x105', '\x3', '\x2', '\x2', '\x2', 
		'\x108', '\x106', '\x3', '\x2', '\x2', '\x2', '\x108', '\x107', '\x3', 
		'\x2', '\x2', '\x2', '\x109', '\x10A', '\x3', '\x2', '\x2', '\x2', '\x10A', 
		'\x10B', '\b', '\x15', '\x3', '\x2', '\x10B', '\x30', '\x3', '\x2', '\x2', 
		'\x2', '\x10C', '\x10E', '\t', '\v', '\x2', '\x2', '\x10D', '\x10C', '\x3', 
		'\x2', '\x2', '\x2', '\x10E', '\x32', '\x3', '\x2', '\x2', '\x2', '\x10F', 
		'\x111', '\x5', '\x31', '\x16', '\x2', '\x110', '\x10F', '\x3', '\x2', 
		'\x2', '\x2', '\x111', '\x112', '\x3', '\x2', '\x2', '\x2', '\x112', '\x110', 
		'\x3', '\x2', '\x2', '\x2', '\x112', '\x113', '\x3', '\x2', '\x2', '\x2', 
		'\x113', '\x115', '\x3', '\x2', '\x2', '\x2', '\x114', '\x116', '\a', 
		'\"', '\x2', '\x2', '\x115', '\x114', '\x3', '\x2', '\x2', '\x2', '\x115', 
		'\x116', '\x3', '\x2', '\x2', '\x2', '\x116', '\x34', '\x3', '\x2', '\x2', 
		'\x2', '\x117', '\x119', '\a', '%', '\x2', '\x2', '\x118', '\x11A', '\t', 
		'\x6', '\x2', '\x2', '\x119', '\x118', '\x3', '\x2', '\x2', '\x2', '\x11A', 
		'\x11B', '\x3', '\x2', '\x2', '\x2', '\x11B', '\x119', '\x3', '\x2', '\x2', 
		'\x2', '\x11B', '\x11C', '\x3', '\x2', '\x2', '\x2', '\x11C', '\x36', 
		'\x3', '\x2', '\x2', '\x2', '\x11D', '\x11F', '\t', '\a', '\x2', '\x2', 
		'\x11E', '\x11D', '\x3', '\x2', '\x2', '\x2', '\x11F', '\x120', '\x3', 
		'\x2', '\x2', '\x2', '\x120', '\x11E', '\x3', '\x2', '\x2', '\x2', '\x120', 
		'\x121', '\x3', '\x2', '\x2', '\x2', '\x121', '\x123', '\x3', '\x2', '\x2', 
		'\x2', '\x122', '\x124', '\a', '\'', '\x2', '\x2', '\x123', '\x122', '\x3', 
		'\x2', '\x2', '\x2', '\x123', '\x124', '\x3', '\x2', '\x2', '\x2', '\x124', 
		'\x38', '\x3', '\x2', '\x2', '\x2', '\x125', '\x126', '\a', '$', '\x2', 
		'\x2', '\x126', '\x127', '\x3', '\x2', '\x2', '\x2', '\x127', '\x128', 
		'\b', '\x1A', '\x6', '\x2', '\x128', ':', '\x3', '\x2', '\x2', '\x2', 
		'\x129', '\x12A', '\a', ')', '\x2', '\x2', '\x12A', '\x12B', '\x3', '\x2', 
		'\x2', '\x2', '\x12B', '\x12C', '\b', '\x1B', '\a', '\x2', '\x12C', '<', 
		'\x3', '\x2', '\x2', '\x2', '\x12D', '\x12E', '\a', '&', '\x2', '\x2', 
		'\x12E', '\x12F', '\a', '}', '\x2', '\x2', '\x12F', '\x133', '\x3', '\x2', 
		'\x2', '\x2', '\x130', '\x132', '\n', '\f', '\x2', '\x2', '\x131', '\x130', 
		'\x3', '\x2', '\x2', '\x2', '\x132', '\x135', '\x3', '\x2', '\x2', '\x2', 
		'\x133', '\x131', '\x3', '\x2', '\x2', '\x2', '\x133', '\x134', '\x3', 
		'\x2', '\x2', '\x2', '\x134', '\x136', '\x3', '\x2', '\x2', '\x2', '\x135', 
		'\x133', '\x3', '\x2', '\x2', '\x2', '\x136', '\x137', '\a', '\x7F', '\x2', 
		'\x2', '\x137', '\x138', '\x3', '\x2', '\x2', '\x2', '\x138', '\x139', 
		'\b', '\x1C', '\x3', '\x2', '\x139', '>', '\x3', '\x2', '\x2', '\x2', 
		'\x13A', '\x13B', '\a', '$', '\x2', '\x2', '\x13B', '\x13C', '\x3', '\x2', 
		'\x2', '\x2', '\x13C', '\x13D', '\b', '\x1D', '\x3', '\x2', '\x13D', '\x13E', 
		'\b', '\x1D', '\x3', '\x2', '\x13E', '@', '\x3', '\x2', '\x2', '\x2', 
		'\x13F', '\x140', '\a', '&', '\x2', '\x2', '\x140', '\x141', '\a', '}', 
		'\x2', '\x2', '\x141', '\x145', '\x3', '\x2', '\x2', '\x2', '\x142', '\x144', 
		'\n', '\f', '\x2', '\x2', '\x143', '\x142', '\x3', '\x2', '\x2', '\x2', 
		'\x144', '\x147', '\x3', '\x2', '\x2', '\x2', '\x145', '\x143', '\x3', 
		'\x2', '\x2', '\x2', '\x145', '\x146', '\x3', '\x2', '\x2', '\x2', '\x146', 
		'\x148', '\x3', '\x2', '\x2', '\x2', '\x147', '\x145', '\x3', '\x2', '\x2', 
		'\x2', '\x148', '\x149', '\a', '\x7F', '\x2', '\x2', '\x149', '\x42', 
		'\x3', '\x2', '\x2', '\x2', '\x14A', '\x14B', '\a', '&', '\x2', '\x2', 
		'\x14B', '\x14F', '\n', '\x3', '\x2', '\x2', '\x14C', '\x14E', '\n', '\r', 
		'\x2', '\x2', '\x14D', '\x14C', '\x3', '\x2', '\x2', '\x2', '\x14E', '\x151', 
		'\x3', '\x2', '\x2', '\x2', '\x14F', '\x150', '\x3', '\x2', '\x2', '\x2', 
		'\x14F', '\x14D', '\x3', '\x2', '\x2', '\x2', '\x150', '\x152', '\x3', 
		'\x2', '\x2', '\x2', '\x151', '\x14F', '\x3', '\x2', '\x2', '\x2', '\x152', 
		'\x164', '\a', '\x7F', '\x2', '\x2', '\x153', '\x154', '\a', '&', '\x2', 
		'\x2', '\x154', '\x155', '\a', '}', '\x2', '\x2', '\x155', '\x159', '\a', 
		'}', '\x2', '\x2', '\x156', '\x158', '\n', '\r', '\x2', '\x2', '\x157', 
		'\x156', '\x3', '\x2', '\x2', '\x2', '\x158', '\x15B', '\x3', '\x2', '\x2', 
		'\x2', '\x159', '\x15A', '\x3', '\x2', '\x2', '\x2', '\x159', '\x157', 
		'\x3', '\x2', '\x2', '\x2', '\x15A', '\x15C', '\x3', '\x2', '\x2', '\x2', 
		'\x15B', '\x159', '\x3', '\x2', '\x2', '\x2', '\x15C', '\x164', '\a', 
		'\x7F', '\x2', '\x2', '\x15D', '\x164', '\a', '&', '\x2', '\x2', '\x15E', 
		'\x160', '\n', '\r', '\x2', '\x2', '\x15F', '\x15E', '\x3', '\x2', '\x2', 
		'\x2', '\x160', '\x161', '\x3', '\x2', '\x2', '\x2', '\x161', '\x15F', 
		'\x3', '\x2', '\x2', '\x2', '\x161', '\x162', '\x3', '\x2', '\x2', '\x2', 
		'\x162', '\x164', '\x3', '\x2', '\x2', '\x2', '\x163', '\x14A', '\x3', 
		'\x2', '\x2', '\x2', '\x163', '\x153', '\x3', '\x2', '\x2', '\x2', '\x163', 
		'\x15D', '\x3', '\x2', '\x2', '\x2', '\x163', '\x15F', '\x3', '\x2', '\x2', 
		'\x2', '\x164', '\x44', '\x3', '\x2', '\x2', '\x2', '\x165', '\x166', 
		'\a', ')', '\x2', '\x2', '\x166', '\x167', '\x3', '\x2', '\x2', '\x2', 
		'\x167', '\x168', '\b', ' ', '\x3', '\x2', '\x168', '\x169', '\b', ' ', 
		'\x3', '\x2', '\x169', '\x46', '\x3', '\x2', '\x2', '\x2', '\x16A', '\x16B', 
		'\a', '&', '\x2', '\x2', '\x16B', '\x16C', '\a', '}', '\x2', '\x2', '\x16C', 
		'\x170', '\x3', '\x2', '\x2', '\x2', '\x16D', '\x16F', '\n', '\f', '\x2', 
		'\x2', '\x16E', '\x16D', '\x3', '\x2', '\x2', '\x2', '\x16F', '\x172', 
		'\x3', '\x2', '\x2', '\x2', '\x170', '\x16E', '\x3', '\x2', '\x2', '\x2', 
		'\x170', '\x171', '\x3', '\x2', '\x2', '\x2', '\x171', '\x173', '\x3', 
		'\x2', '\x2', '\x2', '\x172', '\x170', '\x3', '\x2', '\x2', '\x2', '\x173', 
		'\x174', '\a', '\x7F', '\x2', '\x2', '\x174', 'H', '\x3', '\x2', '\x2', 
		'\x2', '\x175', '\x176', '\a', '&', '\x2', '\x2', '\x176', '\x17A', '\n', 
		'\x3', '\x2', '\x2', '\x177', '\x179', '\n', '\xE', '\x2', '\x2', '\x178', 
		'\x177', '\x3', '\x2', '\x2', '\x2', '\x179', '\x17C', '\x3', '\x2', '\x2', 
		'\x2', '\x17A', '\x17B', '\x3', '\x2', '\x2', '\x2', '\x17A', '\x178', 
		'\x3', '\x2', '\x2', '\x2', '\x17B', '\x17D', '\x3', '\x2', '\x2', '\x2', 
		'\x17C', '\x17A', '\x3', '\x2', '\x2', '\x2', '\x17D', '\x18F', '\a', 
		'\x7F', '\x2', '\x2', '\x17E', '\x17F', '\a', '&', '\x2', '\x2', '\x17F', 
		'\x180', '\a', '}', '\x2', '\x2', '\x180', '\x184', '\a', '}', '\x2', 
		'\x2', '\x181', '\x183', '\n', '\xE', '\x2', '\x2', '\x182', '\x181', 
		'\x3', '\x2', '\x2', '\x2', '\x183', '\x186', '\x3', '\x2', '\x2', '\x2', 
		'\x184', '\x185', '\x3', '\x2', '\x2', '\x2', '\x184', '\x182', '\x3', 
		'\x2', '\x2', '\x2', '\x185', '\x187', '\x3', '\x2', '\x2', '\x2', '\x186', 
		'\x184', '\x3', '\x2', '\x2', '\x2', '\x187', '\x18F', '\a', '\x7F', '\x2', 
		'\x2', '\x188', '\x18F', '\a', '&', '\x2', '\x2', '\x189', '\x18B', '\n', 
		'\xE', '\x2', '\x2', '\x18A', '\x189', '\x3', '\x2', '\x2', '\x2', '\x18B', 
		'\x18C', '\x3', '\x2', '\x2', '\x2', '\x18C', '\x18A', '\x3', '\x2', '\x2', 
		'\x2', '\x18C', '\x18D', '\x3', '\x2', '\x2', '\x2', '\x18D', '\x18F', 
		'\x3', '\x2', '\x2', '\x2', '\x18E', '\x175', '\x3', '\x2', '\x2', '\x2', 
		'\x18E', '\x17E', '\x3', '\x2', '\x2', '\x2', '\x18E', '\x188', '\x3', 
		'\x2', '\x2', '\x2', '\x18E', '\x18A', '\x3', '\x2', '\x2', '\x2', '\x18F', 
		'J', '\x3', '\x2', '\x2', '\x2', '+', '\x2', '\x3', '\x4', '\x5', '\x6', 
		'\a', '\b', 'S', '`', 'r', '\x83', '\x8D', '\x97', '\x9F', '\xA1', '\xAD', 
		'\xB6', '\xC5', '\xC8', '\xCD', '\xDF', '\xEB', '\xFC', '\x108', '\x10D', 
		'\x112', '\x115', '\x11B', '\x120', '\x123', '\x133', '\x145', '\x14F', 
		'\x159', '\x161', '\x163', '\x170', '\x17A', '\x184', '\x18C', '\x18E', 
		'\b', '\a', '\x3', '\x2', '\x6', '\x2', '\x2', '\a', '\x6', '\x2', '\b', 
		'\x2', '\x2', '\a', '\a', '\x2', '\a', '\b', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Mindbox.Quokka.Generated
