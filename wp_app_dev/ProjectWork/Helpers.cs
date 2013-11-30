using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace ProjectWork
{
    public static class Helpers
    {
        public static IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        private static readonly Regex QueryStringRegex = new Regex(@"[\?&#](?<name>[^&=]+)=(?<value>[^&=]+)");

        // from SO
        public static IEnumerable<KeyValuePair<string, string>> ParseQueryString(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentException("uri");

            var matches = QueryStringRegex.Matches(uri.OriginalString);
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                yield return new KeyValuePair<string, string>(match.Groups["name"].Value, match.Groups["value"].Value);
            }
        }

        public static bool strCmp(string a, string b)
        {
            if (a.Length < b.Length)
                return false;
            bool equal = false;
            for (int i = 0; i < b.Length; i++)
            {

                if (a[i] == b[i])
                    equal = true;
                else
                {
                    equal = false;
                    break;
                }

            }

            return equal;
        }
    }
}
