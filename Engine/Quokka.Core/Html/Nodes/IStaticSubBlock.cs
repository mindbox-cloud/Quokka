namespace Quokka.Html
{
	internal interface IStaticSubBlock : ITemplateNode
	{
		/// <summary>
		/// Zero-based offset inside the static node
		/// </summary>
		int Offset { get; }

		/// <summary>
		/// Length (in characters) of the subnode
		/// </summary>
		int Length { get; }
	}
}