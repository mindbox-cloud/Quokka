using System;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class VisitingContext
	{
		private readonly Func<VisitingContext, IQuokkaVisitor<StaticBlock>> staticBlockFactoryMethod;

		public SyntaxErrorListener ErrorListener { get; }

		public VisitingContext(
			SyntaxErrorListener errorListener,
			Func<VisitingContext, IQuokkaVisitor<StaticBlock>> staticBlockFactoryMethod)
		{
			this.staticBlockFactoryMethod = staticBlockFactoryMethod;
			ErrorListener = errorListener;
		}

		public IQuokkaVisitor<StaticBlock> CreateStaticBlockVisitor()
		{
			return staticBlockFactoryMethod(this);
		}
	}
}
