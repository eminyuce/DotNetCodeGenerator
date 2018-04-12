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
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
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
