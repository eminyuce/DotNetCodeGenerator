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

            //spring.datasource.url = jdbc:mysql://localhost:3306/polbot2?useSSL=false
            //spring.datasource.username = polbot
            //spring.datasource.password = 145145145 

            //CALL `polbot2`.`sp_test2`();
            string conn = "Server=174.128.194.106;Database=polbot2;Uid=polbot;Pwd=145145145;Port=3306";
            string cmdText = "call sp_test(@n, @f);";
           // MySqlParameter[] parameters = new MySqlParameter[2];
           // parameters[0] = new MySqlParameter("@n", "steve");
           // parameters[1] = new MySqlParameter("@f", @"d:\temp\test.txt");
           //// MySql.Data.MySqlClient.MySqlHelper.ExecuteNonQuery(conn, cmdText, parameters);


            cmdText = "call sp_test2();";
            DataSet dataSetResult = MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(conn, cmdText, null);
            Console.WriteLine("total" + dataSetResult.Tables.Count);

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
