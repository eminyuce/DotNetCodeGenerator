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
