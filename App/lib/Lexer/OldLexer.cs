﻿// using System.Text.RegularExpressions;

// namespace hulk;

// public class Lexer
// {

//     private string code;
//     public Errors diagnostics = new();

//     private readonly List<Tuple<Regex, TokenType>> regexToTokenType = new()
//     {
//         Tuple.Create(new Regex(@"\("), TokenType.LParen),
//         Tuple.Create(new Regex(@"\)"), TokenType.RParen),
//         Tuple.Create(new Regex(@"\["), TokenType.LBracket),
//         Tuple.Create(new Regex(@"\]"), TokenType.RBracket),
//         Tuple.Create(new Regex(@"(?:\d+(?:\.\d*)?|\.\d+)"), TokenType.Number),
//         Tuple.Create(new Regex("true|false"), TokenType.Boolean),
//         Tuple.Create(new Regex("!="), TokenType.Diferent),
//         Tuple.Create(new Regex(">="), TokenType.BiggerOrEqual),
//         Tuple.Create(new Regex("<="), TokenType.LessThan),
//         Tuple.Create(new Regex("<"), TokenType.Minor),
//         Tuple.Create(new Regex(">"), TokenType.Bigger),
//         Tuple.Create(new Regex("=="), TokenType.Comparation),
//         Tuple.Create(new Regex("="), TokenType.Asignation),
//         Tuple.Create(new Regex("&"), TokenType.And),
//         Tuple.Create(new Regex(@"\|"), TokenType.Or),
//         Tuple.Create(new Regex(@"\+"), TokenType.Plus),
//         Tuple.Create(new Regex(@"\-"), TokenType.Minus),
//         Tuple.Create(new Regex(@"\*"), TokenType.Mult),
//         Tuple.Create(new Regex(@"\/"), TokenType.Div),
//         Tuple.Create(new Regex(@"\%"), TokenType.Mod),
//         Tuple.Create(new Regex(@"\^"), TokenType.Pow),
//         Tuple.Create(new Regex(@"\@"), TokenType.Concat),
//         Tuple.Create(new Regex(".next|.current|.size|PI|sin|cos|log|range|exp|rand|sqrt|E"), TokenType.MathFunctions),
//         Tuple.Create(new Regex(@"\bprint"), TokenType.Print),
//         Tuple.Create(new Regex(@"\bin\b"), TokenType.Keyword),
//         Tuple.Create(new Regex(@"\blet\b"), TokenType.Keyword),
//         Tuple.Create(new Regex(@"\bif"), TokenType.Keyword),
//         Tuple.Create(new Regex(@"\bfunction"), TokenType.Keyword),
//         Tuple.Create(new Regex(@"\bfor"), TokenType.Keyword),
//         Tuple.Create(new Regex(@"\bwhile"), TokenType.Keyword),
//         Tuple.Create(new Regex(@"\belse\b"), TokenType.Else),
//         Tuple.Create(new Regex(@"\belif\b"), TokenType.Elif),
//         Tuple.Create(new Regex(@"\b[a-zA-Z_]\w*\b"), TokenType.Identificator),
//         Tuple.Create(new Regex("\"([^\"\\\\]|\\\\.)*\""), TokenType.String),
//         Tuple.Create(new Regex(","), TokenType.Comma),
//         Tuple.Create(new Regex(" "), TokenType.WhiteSpace),
//         Tuple.Create(new Regex("!"), TokenType.Negation),
//         Tuple.Create(new Regex(";"), TokenType.EOL)
//     };

//     public Lexer(string code)
//     {
//         this.code = code;
//     }

//     public List<Token> Tokenize()
//     {
//         List<Token> tokens = new();
//         int currentIndex = 0;

//         while (currentIndex < code.Length)
//         {
//             bool matchFound = false;
//             int len = 0;

//             foreach (var regexToken in regexToTokenType)
//             {
//                 var match = regexToken.Item1.Match(code, currentIndex);
//                 if (match.Success && match.Index == currentIndex)
//                 {
//                     if (!double.TryParse(match.Value, out double value) && regexToken.Item2 == TokenType.Number)
//                     {
//                         diagnostics.AddError($"! LEXICAL ERROR: The number '{match.Value}' isn't valid");
//                     }

//                     if (regexToken.Item2 == TokenType.MathFunctions)
//                     {
//                         var random = new Random();
//                         double randomNum = random.NextDouble();
//                         switch (match.Value)
//                         {
//                             case "PI":
//                                 tokens.Add(new Token(TokenType.Number, match.Index, "PI", Math.PI));
//                                 break;
//                             case "E":
//                                 tokens.Add(new Token(TokenType.Number, match.Index, "E", Math.E));
//                                 break;
//                             case "rand":
//                                 tokens.Add(new Token(TokenType.Number, match.Index, "rand", randomNum));
//                                 break;
//                             default:
//                                 tokens.Add(new Token(regexToken.Item2, match.Index, match.Value, match.Value));
//                                 break;
//                         }
//                     }
//                     else if (regexToken.Item2 == TokenType.Boolean)
//                     {
//                         bool val;
//                         if (match.Value=="true")
//                         {
//                             val = true;
//                             tokens.Add(new Token(regexToken.Item2, match.Index, code.Substring(match.Index, match.Length), val));
//                         }
//                         else {
//                             val = false;
//                             tokens.Add(new Token(regexToken.Item2, match.Index, code.Substring(match.Index, match.Length), val));
//                         }                        
//                     }
//                     else if (!(regexToken.Item2 == TokenType.WhiteSpace))
//                     {
//                         tokens.Add(new Token(regexToken.Item2, match.Index, match.Value, value));
//                     }
//                     len = match.Length;
//                     currentIndex += len;
//                     matchFound = true;
//                     break;
//                 }
//                 len = match.Length;
//             }

//             if (currentIndex < code.Length && code[currentIndex] == ' ')
//             {
//                 currentIndex++;
//             }

//             if (!matchFound)
//             {
//                 diagnostics.AddError($"! LEXICAL ERROR: {code.Substring(currentIndex, len)} invalid token");

//                 tokens.Add(new Token(TokenType.Error, currentIndex++, null, null));
//             }
//         }

//         if (tokens[tokens.Count - 1].Type != TokenType.EOL)
//         {
//             tokens.Add(new Token(TokenType.Error, currentIndex, null, null));
//         }

//         return tokens;
//     }
// }