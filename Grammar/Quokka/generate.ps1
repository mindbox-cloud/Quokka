function GenerateGrammarFiles ($grammarFile, $directory)
{	
	# Tool options reference: https://github.com/antlr/antlr4/blob/master/doc/tool-options.md

	& java `
		"-jar" `
		"..\..\Tools\antlr-4.7-complete.jar" `
		"$grammarFile" `
		"-lib" $directory `
		"-encoding" "UTF-8" `
		"-visitor" `
		"-no-listener" `
		"-package" "Mindbox.Quokka.Generated" `
		"-Dlanguage=CSharp" `
		"-Werror" `
		"-o" $directory
}

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$generatedDirectory = "..\..\Engine\Quokka.Core\Generated"
$generatedHtmlDirectory = "..\..\Engine\Quokka.Core\Html\Generated"

GenerateGrammarFiles (Join-Path $scriptPath "QuokkaLex.g4") $generatedDirectory
GenerateGrammarFiles (Join-Path $scriptPath "Quokka.g4") $generatedDirectory
GenerateGrammarFiles (Join-Path $scriptPath "QuokkaHtmlLex.g4") $generatedHtmlDirectory
GenerateGrammarFiles (Join-Path $scriptPath "QuokkaHtml.g4") $generatedHtmlDirectory

# Modifying generated .cs files:
# - making types internal instead of public
# - removing machine-specific absolute path from "Generated from..." header

$PathToRepositoryRoot = (Get-Item $PSScriptRoot).parent.parent.FullName + '\';

Get-ChildItem $generatedDirectory,$generatedHtmlDirectory -Filter *.cs | `
	Foreach-Object{
		$fileName = $_.FullName
		(Get-Content $fileName) | Foreach-Object {	$_ -replace 'public partial class Quokka', 'internal partial class Quokka' `
													   -replace 'public interface IQuokka', 'internal interface IQuokka' `
													   -replace [regex]::Escape($PathToRepositoryRoot), '' } | Set-Content $fileName
	}
	
# Removing .tokens files from the output directory (they are necessary only during generation)	

Get-ChildItem $generatedDirectory,$generatedHtmlDirectory -Filter *.tokens | foreach ($_) {remove-item $_.fullname}