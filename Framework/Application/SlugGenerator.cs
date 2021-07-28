﻿using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Application
{
    public static class SlugGenerator
    {
        public static string Slugify(this string phrase)
            {
            var s = phrase.RemoveDiacratics().ToLower();
            s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u0200C\u0200Fa-z0-9\s-]", "");
            s = Regex.Replace(s, @"\s+", " ").Trim();
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim();
            s = Regex.Replace(s, @"\s", "-");
            s = Regex.Replace(s, @"", "-");
            return s.ToLower();
            }
        public static string RemoveDiacratics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();
            foreach(var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark )
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
