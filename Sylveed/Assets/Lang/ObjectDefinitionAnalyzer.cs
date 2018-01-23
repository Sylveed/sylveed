using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.Lang.Tokens;
using Sylveed.Lang.Syntactics;
using Sylveed.Lang.Semantics;

namespace Sylveed.Lang
{
	public class ObjectDefinitionAnalyzer
	{
		readonly SourceBlock source;

		public ObjectDefinitionAnalyzer(SourceBlock source)
		{
			this.source = source;
		}
		
		public SourceBlock Analyze()
		{
			return new SourceBlock(AnalyzeCore(source.InnerExpressions));
		}

		IEnumerable<Expression> AnalyzeCore(IEnumerable<Expression> expressions)
		{
			Stack<Token> modifierBuffer = new Stack<Token>();

			ClassToken previousObjectToken = null;

			foreach (var expression in expressions)
			{
				if (previousObjectToken != null)
				{
					var block = expression as BracketBlock;

					if (block == null || block.BracketType != BracketType.CurlyBracket)
						throw new InvalidOperationException();

					previousObjectToken = null;
				}
				else
				{
					if (expression is ClassToken)
					{
						previousObjectToken = expression as ClassToken;
					}
					else
					{
						yield return expression;
					}
				}
			}
		}
	}
}
