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
        public  void GenerateCode(CodeGeneratorResult codeGeneratorResult)
        {
            var databaseMetaData = TableRepository.GetAllTables(codeGeneratorResult.ConnectionString);
            TableRepository.GetSelectedTableMetaData(databaseMetaData, codeGeneratorResult.SelectedTable);

            CodeProducerHelper.CodeGeneratorResult = codeGeneratorResult;
            CodeProducerHelper.DatabaseMetadata = databaseMetaData;
            CodeProducerHelper.GenerateSPModel();
            CodeProducerHelper.GenereateSaveOrUpdateDatabaseUtility();
            //var tasks = new List<Task>();

            //Task task = new Task(() => {  });
            //task.Start();


            //tasks.Add(task);
            //await Task.WhenAll(tasks);

            codeGeneratorResult = CodeProducerHelper.CodeGeneratorResult;
        }
    }
}
