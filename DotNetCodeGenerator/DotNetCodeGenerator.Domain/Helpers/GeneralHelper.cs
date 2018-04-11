using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Helpers
{
    public class GeneralHelper
    {
     
        public static string ToTitleCase(string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static string GetCleanEntityName(string m)
        {
            if (!String.IsNullOrEmpty(m))
            {
                var parts = m.Split(new string[] { "_" }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    m = "Nwm" + UppercaseFirst(parts[0]) + "" + UppercaseFirst(parts[1].TrimEnd('s'));
                }
                else
                {
                    m = parts[0];
                }
            }
            return m;
        }
        public static string GetEntityPrefixName(string m)
        {
            String k = "";
            if (!String.IsNullOrEmpty(m))
            {
                var parts = m.Split(new string[] { "_" }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    k = parts[0].Trim();
                }
            }
            return k;
        }
    }
}
