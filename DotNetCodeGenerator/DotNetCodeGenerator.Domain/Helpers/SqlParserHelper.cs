using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Helpers
{
    public class SqlParserHelper
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static string RemoveBrackets(String m)
        {
            return m.Replace("[", "").Replace("]", "");
        }
        public static void ParseSqlCreateStatement(string txt = "")
        {

            string tableName = "";
            string primaryKey = "";
            var bracketsRegex = @"\[(.*?)\]";
            using (StringReader reader = new StringReader(txt))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string lineLower = line.ToLower();
                    try
                    {
                        // Do something with the line
                        if (line.ToLower().Contains("create table"))
                        {

                            try
                            {
                                string tbl = "table";
                                var f = lineLower.IndexOf(tbl) + tbl.Length;
                                string s1 = RemoveBrackets(line.Substring(f, line.Length - f - 1).ToStr());
                                tableName = s1;
                            }
                            catch (Exception ex)
                            {

                            }
                       


                        }
                        else if (line.ToLower().Contains("null"))
                        {

                            string tbl = line.ToLower().Contains("not null") ? "not null" : "null";
                            var f = lineLower.IndexOf(tbl);
                            string s1 = line.Substring(0, f).ToStr();
                            Regex r = new Regex(bracketsRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            var matches = r.Matches(line);
                            foreach (Match m in matches)
                            {
                                Console.WriteLine(m.Groups[1]);
                            }

                        }
                        else if (line.ToLower().Contains("emin"))
                        {
                            string tbl = "primary key";
                            if (line.ToLower().Contains("constraint"))
                            {
                                Regex r = new Regex(bracketsRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                                Match m = r.Match(line);
                                if (m.Success)
                                {
                                    primaryKey = RemoveBrackets(m.Groups[1].ToStr());
                                }

                            }
                            else
                            {

                            }


                        }
                    }
                    catch (Exception ex)
                    {

                       
                    }
                   
                }
            }

            Console.WriteLine(tableName);
            Console.WriteLine(primaryKey);
        }
    }
}
