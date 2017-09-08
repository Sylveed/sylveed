using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.Lang.Tokens;
using Sylveed.Lang.Syntactics;

namespace Sylveed.Lang
{
	public class SyntacticBlockAnalyzer
	{
		readonly Token[] source;

		public SyntacticBlockAnalyzer(IEnumerable<Token> source)
		{
			this.source = source.ToArray();
		}
		
		public SourceBlock Analyze()
		{
			var innerExpressionBuffer = new List<Expression>();
			var innerExpressionBufferStack = new Stack<List<Expression>>();
			var openingBracketStack = new Stack<BracketToken>();
			
			foreach (var token in source)
			{
				var bracketToken = token as BracketToken;

				if (bracketToken == null)
				{
					innerExpressionBuffer.Add(token);
				}
				else
				{
					if (bracketToken.IsOpeningBracket)
					{
						innerExpressionBufferStack.Push(innerExpressionBuffer);
						innerExpressionBuffer = new List<Expression>();

						openingBracketStack.Push(bracketToken);
					}
					else
					{
						if (openingBracketStack.Count == 0)
							throw new InvalidOperationException();

						var openingBracket = openingBracketStack.Pop();

						if (openingBracket.BracketType != bracketToken.BracketType)
							throw new InvalidOperationException();

						var parentInnerExpressionBuffer = innerExpressionBufferStack.Pop();

						var block = new BracketBlock(openingBracket.BracketType, innerExpressionBuffer);

						parentInnerExpressionBuffer.Add(block);

						innerExpressionBuffer = parentInnerExpressionBuffer;
					}
				}
			}

			if (openingBracketStack.Count > 0)
				throw new InvalidProgramException();

			if (innerExpressionBufferStack.Count > 0)
				throw new InvalidProgramException();

			return new SourceBlock(innerExpressionBuffer);
		}
	}
}
