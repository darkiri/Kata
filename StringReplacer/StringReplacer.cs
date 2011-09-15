using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace StringReplacer {
    [TestFixture]
    public class StringReplacerTests {
        [TestCaseSource("_replacerTestCases")]
        public void ReplaceTestCase(Dictionary<string, string> dictionary, string input, string output) {
            var replacer = new StringReplacer(dictionary);
            Assert.That(replacer.Replace(input), Is.EqualTo(output));
        }

        private static object[] _replacerTestCases = {
            new object[] {new Dictionary<string, string>(), "", ""},
            new object[] {new Dictionary<string, string> {{"bla", "bla"}}, "str", "str"},
            new object[] {
                new Dictionary<string, string> {{"Name", "Welt"}},
                "Hallo, $Name$!", "Hallo, Welt!"
            },
            new object[] {
                new Dictionary<string, string> {{"Name1", "Welt"}, {"Name2", "Hölle"}},
                "Hallo, $Name1$ und $Name2$!", "Hallo, Welt und Hölle!"
            },
            new object[] {
                new Dictionary<string, string> {{"Name1", "Welt"},},
                "Hallo, $Name1$ und $Name2$!", "Hallo, Welt und !"
            },
            new object[] {
                new Dictionary<string, string>(),
                "Hallo, $Name1$ und $Name2$!", "Hallo,  und !"
            },
            new object[] {
                new Dictionary<string, string> {{"Name1", "Welt"},},
                "That was 20$", "That was 20$"
            },
            new object[] {
                new Dictionary<string, string> {{"Name1", "Welt"},},
                "That was 20$ and 30$", "That was 20$ and 30$"
            },
            new object[] {
                new Dictionary<string, string> {
                    {"Name1", "Welt"},
                    {"Name2", "Hölle"},
                    {"Header", "Hallo, $Name1$!"},
                    {"Footer", "Regards, $Name2$"},
                },
                "$Header$ From $Name1$ to $Name2$. $Footer$", "Hallo, Welt! From Welt to Hölle. Regards, Hölle"
            },
        };
    }

    public class StringReplacer {
        private readonly Dictionary<string, string> _dictionary;

        public StringReplacer(Dictionary<string, string> dictionary) {
            _dictionary = dictionary;
        }

        public string Replace(string input) {
            return RemoveUnusedPlaceholders(ReplaceUsedPlaceholders(input));
        }

        private string ReplaceUsedPlaceholders(string input) {
            return ContainsExistingPlaceholders(input)
                       ? ReplaceUsedPlaceholders(DoSinglePhaseReplace(input))
                       : input;
        }

        private bool ContainsExistingPlaceholders(string input) {
            return _dictionary.Keys.Select(Key2Placeholder).Any(input.Contains);
        }

        private string DoSinglePhaseReplace(string input) {
            return _dictionary
                .Keys
                .Aggregate(input, (i, key) => i.Replace(Key2Placeholder(key), _dictionary[key]));
        }

        private static string Key2Placeholder(string p) {
            return "$" + p + "$";
        }

        public static string RemoveUnusedPlaceholders(string input) {
            return _placeholderDefinition.IsMatch(input) ? _placeholderDefinition.Replace(input, "") : input;
        }

        private static readonly Regex _placeholderDefinition = new Regex(@"\$[a-zA-Z0-9]+\$");
    }
}