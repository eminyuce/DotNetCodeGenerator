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

        public string MySqlConnectionString = ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString;
        public string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        [TestInitialize]
        public void MyTestInitialize()
        {
            //var kernel = NinjectWebCommon.CreatePublicKernel();
            //_sut = kernel.Resolve<HomeController>();
        }
        [TestMethod]
        public void Index222()
        {
            var pp = new TestRepository();
            int id=pp.SaveOrUpdateNwmTest(new NwmTest { Id=100,Name="Test" });


            var item = new NwmAyarlar();

            item.id = 1;
            item.siteBasligi = "siteBasligi";
            item.anahtarKelimeler = "";
            item.google = "";
            item.siteAciklamasi = "";
            item.firmaAdi = "";
            item.telefon = "";
            item.telefon2 = "";
            item.faks = "";
            item.eposta = "";
            item.adres = "";
            item.yetkili = "";
            item.username = "";
            item.password = "";
            item.logKayit = 1;
            item.durum = 10;
            item.siteUrl = "";
            item.panelUrl = "";
            item.smtpSunucu = "";
            item.smtpPort = "";
            item.smtpKullanici = "";
            item.smtpSifre = "";
            item.smtpAd = "";
            item.smtpMetod = "";
            item.smtpDurum = "";
            item.facebook = "";
            item.twitter = "";
            item.gplus = "";
            item.foursquare = "";
            item.map = "";
            var result1= pp.SaveOrUpdateNwmAyarlar(item);

            Console.WriteLine(result1.id);
        }
        [TestMethod]
        public void Index()
        {

            var tableRepository = new TableRepository();
            var tableService = new TableService();
            tableService.TableRepository = tableRepository;
            CodeProducerHelper CodeProducerHelper = new CodeProducerHelper();
            //  var databaseMetaData = tableRepository.GetAllTables(ConnectionString);
            //  tableRepository.GetSelectedTableMetaData(databaseMetaData, "TestEY_2.dbo.Products");
            DatabaseMetadata databaseMetaData = tableRepository.GetAllMySqlTables(MySqlConnectionString);
            tableRepository.GetSelectedMysqlTableMetaData(databaseMetaData, "def.polbot2.currency_config2");

            CodeGeneratorResult codeGeneratorResult = new CodeGeneratorResult();
            codeGeneratorResult.MySqlConnectionString = MySqlConnectionString;

            codeGeneratorResult.ModifiedTableName = "NwmCurrencyConfig";
            CodeProducerHelper.CodeGeneratorResult = codeGeneratorResult;
            CodeProducerHelper.DatabaseMetadata = databaseMetaData;
            CodeProducerHelper.GenerateMySqlSaveOrUpdateStoredProcedure();

            Console.WriteLine(codeGeneratorResult.MySqlSaveOrUpdateStoredProc);

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
            var testRepo = new TestRepository();
            //var nwmFirmalars = testRepo.GetNwmFirmalars();
            //foreach (var nwmFirmalar in nwmFirmalars)
            //{
            //    Console.WriteLine("id:" + nwmFirmalar.id);
            //}

            var item = new NwmCurrencyConfig();
            item.currency_config_id = 99;
            item.buy_at_price = 1;
            item.buy_on_percent = 1;
            item.order_timeout_in_hour = 1;
            item.buyable = false;
            item.currency_pair = "";
            item.sell_at_price = 1;
            item.sell_on_percent = 1;
            item.sellable = false;
            item.usable_balance_percent = 1;
            item.bot_user = 3;

            var id =  testRepo.SaveOrUpdateNwmCurrencyConfig(item);
            Console.WriteLine("id:" + id);
        }
    }
}
