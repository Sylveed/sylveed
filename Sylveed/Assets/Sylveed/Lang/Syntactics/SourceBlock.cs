﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.Lang.Syntactics
{
	public class SourceBlock : SyntacticBlock
	{
		readonly Expression[] inners;

		public IEnumerable<Expression> InnerExpressions { get { return inners; } }

		public SourceBlock(IEnumerable<Expression> inners)
		{
			this.inners = inners.ToArray();
		}
	}
}
