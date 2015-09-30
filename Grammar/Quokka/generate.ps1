function GenerateGrammarFiles ($grammarFile)
{
	
	& java `
		"-jar" `
		"..\..\Tools\antlr-4.5.1-complete.jar" `
		"$grammarFile" `
		"-lib" "..\..\Engine\Quokka.Core\Generated" `
		"-encoding" "UTF-8" `
		"-visitor" `
		"-listener" `
		"-package" "Quokka.Generated" `
		"-Dlanguage=CSharp" `
		"-Werror" `
		"-o" "..\..\Engine\Quokka.Core\Generated"
}

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

GenerateGrammarFiles (Join-Path $scriptPath "QuokkaLex.g4")
GenerateGrammarFiles (Join-Path $scriptPath "Quokka.g4")