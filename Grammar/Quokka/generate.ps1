function GenerateGrammarFiles ($grammarFile, $generatedDirectory)
{	
	& java `
		"-jar" `
		"..\..\Tools\antlr-4.5.1-complete.jar" `
		"$grammarFile" `
		"-lib" $generatedDirectory `
		"-encoding" "UTF-8" `
		"-visitor" `
		"-no-listener" `
		"-package" "Quokka.Generated" `
		"-Dlanguage=CSharp" `
		"-Werror" `
		"-o" $generatedDirectory
}

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$generatedDirectory = "..\..\Engine\Quokka.Core\Generated"

GenerateGrammarFiles (Join-Path $scriptPath "QuokkaLex.g4") $generatedDirectory
GenerateGrammarFiles (Join-Path $scriptPath "Quokka.g4") $generatedDirectory
GenerateGrammarFiles (Join-Path $scriptPath "QuokkaHtmlLex.g4") $generatedDirectory
GenerateGrammarFiles (Join-Path $scriptPath "QuokkaHtml.g4") $generatedDirectory

Get-ChildItem $generatedDirectory -Filter *.cs | `
		Foreach-Object{
			$fileName = $_.FullName
			(Get-Content $fileName) | Foreach-Object {	$_ -replace 'public partial class Quokka', 'internal partial class Quokka' `
														   -replace 'public interface IQuokka', 'internal interface IQuokka' } | Set-Content $fileName
		}