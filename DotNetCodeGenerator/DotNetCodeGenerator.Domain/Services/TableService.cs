﻿using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Entities.Enums;
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
using System.Threading;
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
        public DatabaseMetadata GetAllMySqlTables(String connectionString)
        {
            return TableRepository.GetAllMySqlTables(connectionString);
        }

        public DatabaseMetadata GetAllTables(String connectionString)
        {
            return TableRepository.GetAllTables(connectionString);
        }
        public DataSet GetDataSet(string sqlCommand, string connectionString)
        {
            return TableRepository.GetDataSet(sqlCommand, connectionString);
        }
        public async Task FillGridView(CodeGeneratorResult codeGeneratorResult)
        {
            var task = Task.Factory.StartNew(() =>
            {
                var databaseMetaData = new DatabaseMetadata();
                if (!String.IsNullOrEmpty(codeGeneratorResult.ConnectionString))
                {
                    databaseMetaData = this.GetAllTablesFromCache(codeGeneratorResult.ConnectionString);
                    TableRepository.GetSelectedTableMetaData(databaseMetaData, codeGeneratorResult.SelectedTable);
                }
                else if (!String.IsNullOrEmpty(codeGeneratorResult.MySqlConnectionString))
                {
                    databaseMetaData = this.GetAllMySqlTables(codeGeneratorResult.MySqlConnectionString);
                    TableRepository.GetSelectedMysqlTableMetaData(databaseMetaData, codeGeneratorResult.SelectedTable);
                }
                codeGeneratorResult.DatabaseMetadata = databaseMetaData;
            });
            codeGeneratorResult.UserMessage = codeGeneratorResult.SelectedTable + " table metadata is populated to GridView. You are so close, Do not give up until you make it, dude :)";
            codeGeneratorResult.UserMessageState = UserMessageState.Success;
            await task;

        }
        public async Task GenerateCode(CodeGeneratorResult codeGeneratorResult)
        {
            DatabaseMetadata databaseMetaData = new DatabaseMetadata();

            databaseMetaData = await GetDatabaseMetaDataAsync(codeGeneratorResult, databaseMetaData);

            CodeProducerHelper.CodeGeneratorResult = codeGeneratorResult;
            CodeProducerHelper.DatabaseMetadata = databaseMetaData;


            var tasks = new List<Task>();
            // Database related code.
            if (databaseMetaData.DatabaseType == DatabaseType.MsSql || databaseMetaData.DatabaseType == DatabaseType.UnKnown)
            {
                tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateSaveOrUpdateStoredProcedure(); }));
                tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateSqlRepository(); }));
                tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateStoredProcExecutionCode(); }));
            }
            else if (databaseMetaData.DatabaseType == DatabaseType.MySql)
            {
                tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateMySqlSaveOrUpdateStoredProcedure(); }));
                tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenereateMySqlRepository(); }));
            }

            // c# code for both database.
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateWebApiController(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateTableServices(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateTableItem(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateNewInstance(); }));
            tasks.Add(Task.Factory.StartNew(() => { CodeProducerHelper.GenerateAspMvcControllerClass(); }));




            await Task.WhenAll(tasks);

            var tableName = codeGeneratorResult.ModifiedTableName.ToStr(codeGeneratorResult.SelectedTable);
            codeGeneratorResult = CodeProducerHelper.CodeGeneratorResult;
            codeGeneratorResult.DatabaseMetadata = databaseMetaData;
            codeGeneratorResult.UserMessage = tableName + " table codes are created. You made it dude, Congratulation :)";
            codeGeneratorResult.UserMessageState = UserMessageState.Success;
        }
        private async Task<DatabaseMetadata> GetDatabaseMetaDataAsync(CodeGeneratorResult codeGeneratorResult, DatabaseMetadata databaseMetaData)
        {
            var t = Task<DatabaseMetadata>.Factory.StartNew(() =>
           {
               if (!String.IsNullOrEmpty(codeGeneratorResult.ConnectionString))
               {
                   databaseMetaData = this.GetAllTablesFromCache(codeGeneratorResult.ConnectionString);
                   TableRepository.GetSelectedTableMetaData(databaseMetaData, codeGeneratorResult.SelectedTable);
               }
               else if (!String.IsNullOrEmpty(codeGeneratorResult.MySqlConnectionString))
               {
                   databaseMetaData = this.GetAllMySqlTables(codeGeneratorResult.MySqlConnectionString);
                   TableRepository.GetSelectedMysqlTableMetaData(databaseMetaData, codeGeneratorResult.SelectedTable);
               }
               else if (!String.IsNullOrEmpty(codeGeneratorResult.SqlCreateTableStatement))
               {
                   databaseMetaData = SqlParserHelper.ParseSqlCreateStatement(codeGeneratorResult.SqlCreateTableStatement);
               }

               return databaseMetaData;

           });
            await t;
            return t.Result;
        }
    }
}
