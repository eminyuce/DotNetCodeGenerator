using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Helpers;
using DotNetCodeGenerator.Domain.Repositories;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Services
{
    public class TableService
    {
        [Inject]
        public TableRepository TableRepository { get; set; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        [Inject]
        public CodeProducerHelper CodeProducerHelper { get; set; }

        public DatabaseMetadata GetAllTables(String connectionString)
        {
            return TableRepository.GetAllTables(connectionString);
        }
        public DataSet GetDataSet(string sqlCommand, string connectionString)
        {
            return TableRepository.GetDataSet(sqlCommand, connectionString);
        }
        public async Task GenerateCode(CodeGeneratorResult codeGeneratorResult)
        {
            var databaseMetaData = TableRepository.GetAllTables(codeGeneratorResult.ConnectionString);
            TableRepository.GetSelectedTableMetaData(databaseMetaData, codeGeneratorResult.SelectedTable);
            CodeProducerHelper.CodeGeneratorResult = codeGeneratorResult;
            CodeProducerHelper.DatabaseMetadata = databaseMetaData;

            var tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateSPModel(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenereateSaveOrUpdateDatabaseUtility(); }));
            await Task.WhenAll(tasks);

            codeGeneratorResult = CodeProducerHelper.CodeGeneratorResult;
        }
    }
}
