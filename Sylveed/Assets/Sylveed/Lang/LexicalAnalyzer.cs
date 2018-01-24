using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.Lang.Tokens;

namespace Assets.Sylveed.Lang
{
	public class LexicalAnalyzer
	{
		readonly string source;

		public LexicalAnalyzer(string source)
		{
			this.source = source;
		}

		public IEnumerable<Token> Analyze()
		{
			var buffer = new StringBuilder();

			foreach (var c in source)
			{
				if (TokenSeparation.IsEmpty(c))
				{
					if (buffer.Length > 0)
					{
						var token = ParseWordToken(buffer.ToString());

						buffer.Clear();

						yield return token;
					}
				}
				else if (TokenSeparation.IsSymbol(c))
				{
					if (buffer.Length > 0)
					{
						var token = ParseWordToken(buffer.ToString());

						buffer.Clear();

						yield return token;
					}

					yield return ParseSymbolToken(new string(c, 1));
				}
				else
				{
					buffer.Append(c);
				}
			}
		}

		Token ParseSymbolToken(string value)
		{
			var token = PrimitiveTokenDefine.GetSymbolValueOrNull(value);

			if (token == null)
				throw new InvalidOperationException();

			return token;
		}

		Token ParseWordToken(string value)
		{
			Token token = PrimitiveTokenDefine.GetValueOrNull(value);

			if (token == null)
			{
				token = new UserDefineToken(value);
			}

			return token;
		}
	}
}
