using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.Lang.Tokens;

namespace Assets.Sylveed.Lang.Syntactics
{
	public class BracketBlock : SyntacticBlock
	{
		readonly Expression[] inners;

		public BracketType BracketType { get; private set; }

		public BracketBlock(BracketType bracketType, IEnumerable<Expression> inners)
		{
			BracketType = bracketType;

			this.inners = inners.ToArray();
		}
	}
}
