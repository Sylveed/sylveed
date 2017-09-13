/*
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Sylveed.Lang.Syntactics;
using Sylveed.Lang.Tokens;

namespace Sylveed.Lang
{
	[TestClass()]
	public class CompilerTest
	{
		[TestMethod()]
		public void Test()
		{
			var source = TestSource.Value;

			var tokens = new LexicalAnalyzer(source).Analyze();

			var rootBlock = new SyntacticBlockAnalyzer(tokens).Analyze();
		}
	}
}
*/