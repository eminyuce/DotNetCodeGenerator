﻿using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Helpers;
using DotNetCodeGenerator.Domain.Repositories;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
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

        public DatabaseMetadata GetAllTablesFromCache(String connectionString)
        {
            var items = (DatabaseMetadata)MemoryCache.Default.Get(connectionString);
            if (items == null)
            {
                items = GetAllTables(connectionString);
                CacheItemPolicy policy = null;
                policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.Default;
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(Settings.CacheMediumSeconds);
                MemoryCache.Default.Set(connectionString, items, policy);
            }
            return items;
        }

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
            var databaseMetaData = this.GetAllTablesFromCache(codeGeneratorResult.ConnectionString);
            TableRepository.GetSelectedTableMetaData(databaseMetaData, codeGeneratorResult.SelectedTable);
            CodeProducerHelper.CodeGeneratorResult = codeGeneratorResult;
            CodeProducerHelper.DatabaseMetadata = databaseMetaData;

            var tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateSPModel(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateTableRepository(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateTableItem(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateNewInstance(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenereateSqlDatabaseOperation(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateAspMvcControllerClass(); }));

            await Task.WhenAll(tasks);

            codeGeneratorResult = CodeProducerHelper.CodeGeneratorResult;
        }
    }
}
