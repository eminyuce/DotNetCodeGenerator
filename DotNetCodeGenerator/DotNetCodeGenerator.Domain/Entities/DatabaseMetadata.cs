﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Entities
{
    public class DatabaseMetadata
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public List<TableMetaData> Tables { get; set; }
        public TableMetaData SelectedTable { get; set; }
        public string MySqlConnectionString { get; set; }
    }
}
