using DotNetCodeGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Helpers
{
    public class TableRowMetaDataHelper
    {
        public static string GetCSharpDataType(TableRowMetaData ki)
        {
            var item = ki;
            String result = "";
            if (item.DataType.IndexOf("varchar") > -1 || item.DataType.IndexOf("text") > -1 || item.DataType.IndexOf("xml") > -1)
            {
                result = "String";
            }
            else if (item.DataType.IndexOf("int") > -1)
            {
                result = "int";
            }
            else if (item.DataType.IndexOf("date") > -1)
            {
                result = "DateTime ";
            }
            else if (item.DataType.IndexOf("bit") > -1)
            {
                result = "Boolean ";
            }
            else if (item.DataType.IndexOf("float") > -1)
            {
                result = "float ";
            }
            else if (item.DataType.IndexOf("char") > -1)
            {
                result = "char ";
            }

            return result.Trim();
        }
        public static string GetPrimaryKeys(List<TableRowMetaData> tableRowMetaDataList)
        {
            foreach (var item in tableRowMetaDataList)
            {
                if (item.PrimaryKey)
                {
                    return item.ColumnName;
                }
            }
            var firstOrDefault = tableRowMetaDataList.FirstOrDefault();
            if (firstOrDefault != null)
                return firstOrDefault.ColumnName;
            else
                return "";
        }
    }
}
