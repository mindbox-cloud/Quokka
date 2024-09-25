// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System.Text.Json;
using Mindbox.Quokka;

namespace Mindbox.Quokka.Script
{
  public class Program
  {
    static void Main(string[] args)
    {
      var htmlString = File.ReadAllText("/Users/nikitin/Documents/Mindbox/Quokka/Engine/ConsoleApp1/template.html");
      var templateFactory = new DefaultTemplateFactory();
      var htmlTemplate = templateFactory.CreateHtmlTemplate(htmlString);
      var basicTemplate = templateFactory.CreateTemplate(htmlString);

      JsonSerializerOptions options = new()
      {
        MaxDepth = 500

      };

      File.WriteAllText("/Users/nikitin/Documents/Mindbox/Quokka/Engine/ConsoleApp1/tree-basic.json", JsonSerializer.Serialize(basicTemplate.GetTestDTO(), options));
      // File.WriteAllText("/Users/nikitin/Documents/Mindbox/Quokka/Engine/ConsoleApp1/tree.json", JsonSerializer.Serialize(htmlTemplate.GetTestDTO()));
      // File.WriteAllText("/Users/nikitin/Documents/Mindbox/Quokka/Engine/ConsoleApp1/tree-model-definition.json", ObjectDumper.Dump(htmlTemplate.GetModelDefinition()));
      System.Console.WriteLine("done");
    }
  }
}


// var template = new HtmlTemplate("<a href=\"${ \"https\" }://example.com/${ 4 + 13 }\">");