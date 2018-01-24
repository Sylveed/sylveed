using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.Lang.Tokens;
using Assets.Sylveed.Lang.Syntactics;
using Assets.Sylveed.Lang.Semantics;

namespace Assets.Sylveed.Lang
{
	public static class DefinitionAnalyzer
	{
		public static IEnumerable<Expression> AnalyzeModifier(IEnumerable<Expression> expressions)
		{
			var modifierBuffer = new List<Token>();

			foreach (var expression in expressions)
			{
				if (expression is IModifierToken)
				{
					modifierBuffer.Add((Token)expression);
				}
				else
				{
					if (modifierBuffer.Count > 0)
					{
						yield return new ModifierSet(modifierBuffer);

						modifierBuffer.Clear();
					}

					yield return expression;
				}
			}
		}

		public static IEnumerable<Expression> AnalyzeClassDefinition(IEnumerable<Expression> expressions)
		{
			ModifierSet currentModifiers = null;
			ClassToken currentClassToken = null;

			foreach (var expression in AnalyzeModifier(expressions))
			{
				var block = expression as BracketBlock;

				if (block != null && block.BracketType == BracketType.CurlyBracket)
				{
					if (currentClassToken != null)
					{
						yield return new ClassDefinitionBlock(
							currentModifiers ?? new ModifierSet(Enumerable.Empty<Token>()),
							currentClassToken,
							block);
					}

					currentClassToken = null;
					currentModifiers = null;
				}
				else
				{
					var modifiers = expression as ModifierSet;
					var classToken = expressions as ClassToken;

					if (classToken == null)
					{
						currentModifiers = null;
					}
					else
					{
						currentModifiers = modifiers;
					}

					currentClassToken = classToken;
				}
			}
		}
	}
}
