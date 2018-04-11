using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Entities
{
    public class TableMetaData
    {
        public string TableCatalog { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }
        public string TableSchema { get; set; }

        public string DatabaseTableName
        {
            get
            {
                return String.Format("{0}.{1}", TableSchema, TableName);
            }
        }

        public List<TableRowMetaData> TableRowMetaDataList { get; set; }


    }
}
