using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Entities.Enums;
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
            var paranthesesRegex = @"\((.*?)\)";
            var mysqlQuoteRegex = @"\`(.*?)\`";

            var tableMetaDataList = new List<TableRowMetaData>();
            int i = 0;
            using (StringReader reader = new StringReader(txt))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string lineLower = line.ToLower();
                    var p = new TableRowMetaData();
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
                                Logger.Error(ex, ex.Message, txt);
                            }



                        }
                        else if (line.ToLower().Contains("null"))
                        {

                            try
                            {
                                var isNotNull = line.ToLower().Contains("not null");
                                p.IsNull = isNotNull.ToStr();
                                string tbl = isNotNull ? "not null" : "null";
                                var f = lineLower.IndexOf(tbl);
                                string s1 = line.Substring(0, f).ToStr();

                                if (line.StartsWith("["))
                                {
                                    Regex r = new Regex(bracketsRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                                    var matches = r.Matches(line);

                                    r = new Regex(paranthesesRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                                    var matches3 = r.Matches(line);
                                    p.ColumnName = RemoveBrackets(matches[0].Groups[0].ToStr());
                                    p.MaxChar = matches3[0].Groups[0].ToStr();
                                    p.PrimaryKey = false;
                                    p.ID = i++;
                                    p.Order = i++;
                                    p.DataType = RemoveBrackets(matches[1].Groups[0].ToStr());
                                    p.DataTypeMaxChar = p.DataType + matches3[0].Groups[0].ToStr();
                                    p.DatabaseType = DatabaseType.MsSql;
                                }
                                else if (line[0]== 32)
                                {
                                    Regex r = new Regex(mysqlQuoteRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                                    var matches = r.Matches(line);
                                    p.ColumnName =  matches[0].Groups[0].ToStr();
                                    p.PrimaryKey = false;
                                    p.ID = i++;
                                    p.Order = i++;
                                    p.DatabaseType = DatabaseType.MsSql;
                                }
                             



                                tableMetaDataList.Add(p);
                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex, ex.Message, txt);
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
            foreach (var tableMeta in tableMetaDataList)
            {
                Console.WriteLine("tableMeta:" + tableMeta);
            }
            Console.WriteLine("tableName:" + tableName);
            Console.WriteLine("primaryKey:" + primaryKey);
        }
        public static bool IsSqlDataType(string line)
        {
            var sqlDataType = new List<String>();
            sqlDataType.Add("varbinary");
            sqlDataType.Add("binary");
            //   sqlDataType.Add("varbinary(1), binary(1)");
            sqlDataType.Add("image");
            sqlDataType.Add("varchar");
            sqlDataType.Add("char");
            //   sqlDataType.Add("nvarchar(1), nchar(1)");
            sqlDataType.Add("nvarchar");
            sqlDataType.Add("nchar");
            sqlDataType.Add("text");
            sqlDataType.Add("ntext");
            sqlDataType.Add("uniqueidentifier");
            sqlDataType.Add("rowversion");
            sqlDataType.Add("bit");
            sqlDataType.Add("tinyint");
            sqlDataType.Add("smallint");
            sqlDataType.Add("int");
            sqlDataType.Add("bigint");
            sqlDataType.Add("smallmoney");
            sqlDataType.Add("money");
            sqlDataType.Add("numeric");
            sqlDataType.Add("decimal");
            sqlDataType.Add("real");
            sqlDataType.Add("float");
            sqlDataType.Add("smalldatetime");
            sqlDataType.Add("datetime");
            sqlDataType.Add("sql_variant");
            sqlDataType.Add("table");
            sqlDataType.Add("cursor");
            sqlDataType.Add("timestamp");
            sqlDataType.Add("xml");

            foreach (var item in sqlDataType)
            {
                if (line.IndexOf(item) > -1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
