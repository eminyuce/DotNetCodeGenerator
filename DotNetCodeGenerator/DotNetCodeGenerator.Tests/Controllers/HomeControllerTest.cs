using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetCodeGenerator;
using DotNetCodeGenerator.Controllers;
using System.Configuration;
using DotNetCodeGenerator.Domain.Helpers;
using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Repositories;
using DotNetCodeGenerator.Domain.Services;
using MySql.Data.MySqlClient;
using System.Data;

namespace DotNetCodeGenerator.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var tableRepository = new TableRepository();
            var tableService = new TableService();
            tableService.TableRepository = tableRepository;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var databaseMetaData = tableRepository.GetAllTables(connectionString);
            tableRepository.GetSelectedTableMetaData(databaseMetaData, "TestEY_2.dbo.Products");

            //var selectedTable = databaseMetaData.Tables.Where(r => r.DatabaseTableName.Equals("dbo.Products"));
            //var t = new CodeProducerHelper();
            //t.TableService = tableService;
            //var codeGeneratorResult = new CodeGeneratorResult();
            //codeGeneratorResult.StoredProcExec = "dbo.test_SP @take=2 -NwmProduct";
            //t.GenerateSPModel(codeGeneratorResult,databaseMetaData);
            //Console.WriteLine(codeGeneratorResult.StoredProcExec);
            //Console.WriteLine(codeGeneratorResult.StoredProcExecModel);
            //Console.WriteLine(codeGeneratorResult.StoredProcExecModelDataReader);
        }

        [TestMethod]
        public void About()
        {
            var tableRepository = new TableRepository();
            //spring.datasource.url = jdbc:mysql://localhost:3306/polbot2?useSSL=false
            //spring.datasource.username = polbot
            //spring.datasource.password = 145145145 

            //CALL `polbot2`.`sp_test2`();


            //IP / Port	: 37.230.108.236 / 3306

            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "mysql06.trwww.com";
            conn_string.UserID = "otokoduret";
            conn_string.Password = "7mX^0XPW";
            conn_string.Database = "test";
            conn_string.Port = 3306;

            string connectionString = "Server=174.128.194.106;Database=polbot2;Uid=emin;Pwd=145145145Aa;Port=3306";
            connectionString = conn_string.ToStr();
            string cmdText = "call sp_test(@n, @f);";
            //var mySqlParameters = new List<MySqlParameter>();
            //mySqlParameters.Add(new MySqlParameter("@n", "steve"));
            //mySqlParameters.Add(new MySqlParameter("@f", @"d:\temp\test.txt"));
            //MySqlHelper.ExecuteNonQuery(connectionString, cmdText, mySqlParameters.ToArray());

            Console.WriteLine("connectionString:" + connectionString);
            //cmdText = "select 1;";
            //DataSet dataSetResult = MySqlHelper.ExecuteDataset(connectionString, cmdText, null);
            //Console.WriteLine("total" + dataSetResult.Tables.Count);
            var databaseMetaData = tableRepository.GetAllMySqlTables("server=mysql06.trwww.com;user id=otokoduret;password=7mX^0XPW;database=test;port=3306");
            Console.WriteLine("total" + databaseMetaData.Tables.Count);
            tableRepository.GetSelectedMysqlTableMetaData(databaseMetaData, "def.test.urunler");


       

        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
