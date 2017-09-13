using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.Lang.Helpers;

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
		readonly static Dictionary<RuntimeTypeHandle, Func<Token>> ConstructorPool =
			new Dictionary<RuntimeTypeHandle, Func<Token>>();

		readonly static Dictionary<string, Func<PrimitiveToken>> GeneratorMap = new Dictionary<string, Func<PrimitiveToken>>();
		readonly static Dictionary<string, Func<SymbolToken>> SymbolGeneratorMap = new Dictionary<string, Func<SymbolToken>>();

		static PrimitiveTokenDefine()
		{
			foreach (var type in GetTokenTypes())
				AddGeneratorMap(type);
		}

		static IEnumerable<Type> GetTokenTypes()
		{
			yield return typeof(ClassToken);
			yield return typeof(StaticToken);
			yield return typeof(PublicToken);
			yield return typeof(PrivateToken);
			yield return typeof(IfToken);
			yield return typeof(WhileToken);
			yield return typeof(ForToken);
			yield return typeof(NewToken);

			yield return typeof(OpeningCurlyBracketToken);
			yield return typeof(ClosingCurlyBracketToken);
			yield return typeof(OpeningParenthesisToken);
			yield return typeof(ClosingParenthesisToken);

			yield return typeof(EqualToken);
			yield return typeof(ColonToken);
			yield return typeof(SemicolonToken);
			yield return typeof(CommaToken);
			yield return typeof(DotToken);
			yield return typeof(PlusToken);
		}
		
		static void AddGeneratorMap(Type type)
		{
			var ctor = GetTokenConstructor(type);

			var key = ctor().Value;

			GeneratorMap.Add(key, () => (PrimitiveToken)ctor());

			if (typeof(SymbolToken).IsAssignableFrom(type))
			{
				SymbolGeneratorMap.Add(key, () => (SymbolToken)ctor());
			}
		}

		static Func<Token> GetTokenConstructor(Type type)
		{
			Func<Token> ctor;
			if (ConstructorPool.TryGetValue(type.TypeHandle, out ctor))
				return ctor;

			var sourceCtor = ConstructorCreator.Create(type);
			ctor = () => (Token)sourceCtor();

			ConstructorPool.Add(type.TypeHandle, ctor);

			return ctor;
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

	public interface IModifierToken
	{

	}

	public class ClassToken : PrimitiveKeywordToken
	{
		const string value = "class";

		public override string Value { get { return value; } }
	}

	public class StaticToken : PrimitiveKeywordToken, IModifierToken
	{
		const string value = "static";

		public override string Value { get { return value; } }
	}

	public class PublicToken : PrimitiveKeywordToken, IModifierToken
	{
		const string value = "public";

		public override string Value { get { return value; } }
	}

	public class PrivateToken : PrimitiveKeywordToken, IModifierToken
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

	public class NewToken : PrimitiveKeywordToken, IModifierToken
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

	public class EqualToken : SymbolToken
	{
		const string value = "=";

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
