using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.Lang.Tokens
{
	public abstract class Token : Expression
	{
		public abstract string Value { get; }
	}

	public class UserDefineToken : Token
	{
		readonly string value;

		public override string Value { get { return value; } }

		public UserDefineToken(string value)
		{
			this.value = value;
		}
	}

	public static class PrimitiveTokenDefine
	{
		readonly static Dictionary<string, Func<PrimitiveToken>> GeneratorMap = new Dictionary<string, Func<PrimitiveToken>>();
		readonly static Dictionary<string, Func<SymbolToken>> SymbolGeneratorMap = new Dictionary<string, Func<SymbolToken>>();

		static PrimitiveTokenDefine()
		{
			GeneratorMap.Add(new ClassToken().Value, () => new ClassToken());
			GeneratorMap.Add(new StaticToken().Value, () => new StaticToken());
			GeneratorMap.Add(new PublicToken().Value, () => new PublicToken());
			GeneratorMap.Add(new PrivateToken().Value, () => new PrivateToken());
			GeneratorMap.Add(new IfToken().Value, () => new IfToken());
			GeneratorMap.Add(new WhileToken().Value, () => new WhileToken());
			GeneratorMap.Add(new ForToken().Value, () => new ForToken());
			GeneratorMap.Add(new NewToken().Value, () => new NewToken());
			GeneratorMap.Add(new OpeningCurlyBracketToken().Value, () => new OpeningCurlyBracketToken());
			GeneratorMap.Add(new ClosingCurlyBracketToken().Value, () => new ClosingCurlyBracketToken());
			GeneratorMap.Add(new OpeningParenthesisToken().Value, () => new OpeningParenthesisToken());
			GeneratorMap.Add(new ClosingParenthesisToken().Value, () => new ClosingParenthesisToken());

			GeneratorMap.Add(new SubstitutionToken().Value, () => new SubstitutionToken());
			GeneratorMap.Add(new EqualsToken().Value, () => new EqualsToken());
			GeneratorMap.Add(new ColonToken().Value, () => new ColonToken());
			GeneratorMap.Add(new SemicolonToken().Value, () => new SemicolonToken());
			GeneratorMap.Add(new CommaToken().Value, () => new CommaToken());
			GeneratorMap.Add(new DotToken().Value, () => new DotToken());
			GeneratorMap.Add(new PlusToken().Value, () => new PlusToken());

			SymbolGeneratorMap.Add(new OpeningCurlyBracketToken().Value, () => new OpeningCurlyBracketToken());
			SymbolGeneratorMap.Add(new ClosingCurlyBracketToken().Value, () => new ClosingCurlyBracketToken());
			SymbolGeneratorMap.Add(new OpeningParenthesisToken().Value, () => new OpeningParenthesisToken());
			SymbolGeneratorMap.Add(new ClosingParenthesisToken().Value, () => new ClosingParenthesisToken());

			SymbolGeneratorMap.Add(new SubstitutionToken().Value, () => new SubstitutionToken());
			SymbolGeneratorMap.Add(new EqualsToken().Value, () => new EqualsToken());
			SymbolGeneratorMap.Add(new ColonToken().Value, () => new ColonToken());
			SymbolGeneratorMap.Add(new SemicolonToken().Value, () => new SemicolonToken());
			SymbolGeneratorMap.Add(new CommaToken().Value, () => new CommaToken());
			SymbolGeneratorMap.Add(new DotToken().Value, () => new DotToken());
			SymbolGeneratorMap.Add(new PlusToken().Value, () => new PlusToken());
		}

		public static PrimitiveToken GetValueOrNull(string key)
		{
			Func<PrimitiveToken> value;
			GeneratorMap.TryGetValue(key, out value);

			if (value == null)
				return null;

			return value();
		}

		public static SymbolToken GetSymbolValueOrNull(string key)
		{
			Func<SymbolToken> value;
			SymbolGeneratorMap.TryGetValue(key, out value);

			if (value == null)
				return null;

			return value();
		}
	}

	public abstract class PrimitiveToken : Token
	{
	}

	public abstract class PrimitiveKeywordToken : PrimitiveToken
	{
	}

	public abstract class SymbolToken : PrimitiveToken
	{
	}

	public class ClassToken : PrimitiveKeywordToken
	{
		const string value = "class";

		public override string Value { get { return value; } }
	}

	public class StaticToken : PrimitiveKeywordToken
	{
		const string value = "static";

		public override string Value { get { return value; } }
	}

	public class PublicToken : PrimitiveKeywordToken
	{
		const string value = "public";

		public override string Value { get { return value; } }
	}

	public class PrivateToken : PrimitiveKeywordToken
	{
		const string value = "private";

		public override string Value { get { return value; } }
	}

	public class IfToken : PrimitiveKeywordToken
	{
		const string value = "if";

		public override string Value { get { return value; } }
	}

	public class WhileToken : PrimitiveKeywordToken
	{
		const string value = "while";

		public override string Value { get { return value; } }
	}

	public class ForToken : PrimitiveKeywordToken
	{
		const string value = "for";

		public override string Value { get { return value; } }
	}

	public class NewToken : PrimitiveKeywordToken
	{
		const string value = "new";

		public override string Value { get { return value; } }
	}

	public enum BracketType
	{
		CurlyBracket,
		Parenthesis,
	}

	public abstract class BracketToken : SymbolToken
	{
		public abstract bool IsOpeningBracket { get; }

		public abstract BracketType BracketType { get; }
	}

	public abstract class OpeningBracketToken : BracketToken
	{
		public override bool IsOpeningBracket { get { return true; } }
	}

	public abstract class ClosingBracketToken : BracketToken
	{
		public override bool IsOpeningBracket { get { return false; } }
	}

	public class OpeningCurlyBracketToken  : OpeningBracketToken
	{
		const string value = "{";

		public override string Value { get { return value; } }

		public override BracketType BracketType { get { return BracketType.CurlyBracket; } }
	}

	public class ClosingCurlyBracketToken : ClosingBracketToken
	{
		const string value = "}";

		public override string Value { get { return value; } }

		public override BracketType BracketType { get { return BracketType.CurlyBracket; } }
	}

	public class OpeningParenthesisToken : OpeningBracketToken
	{
		const string value = "(";

		public override string Value { get { return value; } }

		public override BracketType BracketType { get { return BracketType.Parenthesis; } }
	}

	public class ClosingParenthesisToken : ClosingBracketToken
	{
		const string value = ")";

		public override string Value { get { return value; } }

		public override BracketType BracketType { get { return BracketType.Parenthesis; } }
	}

	public class SubstitutionToken : SymbolToken
	{
		const string value = "=";

		public override string Value { get { return value; } }
	}

	public class EqualsToken : SymbolToken
	{
		const string value = "==";

		public override string Value { get { return value; } }
	}

	public class CommaToken : SymbolToken
	{
		const string value = ",";

		public override string Value { get { return value; } }
	}

	public class DotToken : SymbolToken
	{
		const string value = ".";

		public override string Value { get { return value; } }
	}

	public class SemicolonToken : SymbolToken
	{
		const string value = ";";

		public override string Value { get { return value; } }
	}

	public class ColonToken : SymbolToken
	{
		const string value = ":";

		public override string Value { get { return value; } }
	}

	public class PlusToken : SymbolToken
	{
		const string value = "+";

		public override string Value { get { return value; } }
	}

	public class TokenSeparation
	{
		static readonly HashSet<char> EmptyCharSet;
		static readonly HashSet<char> SymbolCharSet;

		static TokenSeparation()
		{
			EmptyCharSet = new HashSet<char>(new[]
			{
				' ', '\t', '\n', '\r'
			});

			SymbolCharSet = new HashSet<char>(new[]
			{
				'<', '>',
				'(', ')',
				'{', '}',
				'[', ']',
				'.', ',', ':', ';',
				'!', '|', '&', '%', '/', '*', '+', '-', '~', '^', '?','='
			});
		}

		public static bool IsEmpty(char c)
		{
			return EmptyCharSet.Contains(c);
		}

		public static bool IsSymbol(char c)
		{
			return SymbolCharSet.Contains(c);
		}
	}
}
