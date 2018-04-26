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
        public void ParseSqlStatement()
        {
            string txt = @"CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [int] NULL,
	[ProductCategoryId] [int] NOT NULL,
	[BrandId] [int] NULL,
	[RetailerId] [int] NULL,
	[ProductCode] [nvarchar](50) NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [nvarchar](50) NULL,
	[MainPage] [bit] NULL,
	[State] [bit] NULL,
	[Ordering] [int] NULL,
	[CreatedDate] [datetime2](7) NOT NULL CONSTRAINT [DF_Products_CreatedDate]  DEFAULT (getdate()),
	[ImageState] [bit] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL CONSTRAINT [DF_Products_UpdatedDate]  DEFAULT (getdate()),
	[Price] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[UnitsInStock] [int] NULL,
	[TotalRating] [int] NULL,
	[VideoUrl] [nvarchar](1500) NULL,
 CONSTRAINT [PK_Products_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
";


            String mySql = @"CREATE TABLE `trade_history_record` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `buy_vol` double DEFAULT NULL,
  `currency_pair` varchar(255) DEFAULT NULL,
  `sell_vol` double DEFAULT NULL,
  `start` bigint(20) DEFAULT NULL,
  `close` decimal(19,8) DEFAULT NULL,
  `high` decimal(19,8) DEFAULT NULL,
  `low` decimal(19,8) DEFAULT NULL,
  `open` decimal(19,8) DEFAULT NULL,
  `buyqvol` double DEFAULT NULL,
  `sellqvol` double DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
";

            SqlParserHelper.ParseSqlCreateStatement(txt);

        }
        [TestMethod]
        public void TestItem()
        {
            var pp = new TestRepository();
            Console.Write(pp.SaveOrUpdateNwmTest(new NwmTest() { Id = 5, Name = "NwmTest444" }));
        }
        [TestMethod]
        public void NwmHaberlerTest()
        {
            var item = new NwmHaberler();
            var pp = new TestRepository();
            item.id = 99;
            item.sira = 1;
            item.tarih = DateTime.Now;
            item.durum = 1;
            item.seo = "Maritime reporter New SPeeedd";
            item.link = "link 2";
            item.baslik_tr = "baslik_tr 2";
            item.keywords_tr = "keywords_tr 2";
            item.ozet_tr = "ozet_tr";
            item.detay_tr = "detay_tr";
            item.baslik_en = "baslik_en";
            item.keywords_en = "keywords_en";
            item.ozet_en = "ozet_en";
            item.detay_en = "detay_en";
            item.haberTarihi = DateTime.Now;
            item.baslik_de = "baslik_de";
            item.keywords_de = "keywords_de";
            item.ozet_de = "ozet_de";
            item.detay_de = "detay_de";
            item.tip = 1;
            item.baslik_ar = "baslik_ar";
            item.keywords_ar = "keywords_ar";
            item.ozet_ar = "ozet_ar";
            item.detay_ar = "detay_ar";
            item.yorum_tr = "yorum_tr";
            item.yorum_en = "yorum_en";
            var result1 = pp.SaveOrUpdateNwmHaberler(item);
            Console.WriteLine(result1);
        }

        [TestMethod]
        public void Index222()
        {
            var pp = new TestRepository();
            for (int i = 0; i < 10000; i++)
            {
                pp.SaveOrUpdateNwmTest(new NwmTest() { Name = (i+100) + Guid.NewGuid().ToStr() });
            }
            var item = new NwmAyarlar();

            item.id = 1;
            item.siteBasligi = "siteBasligi";
            item.anahtarKelimeler = "anahtarKelimeler";
            item.google = "google 22";
            item.siteAciklamasi = "siteAciklamasi";
            item.firmaAdi = "firmaAdi";
            item.telefon = "telefon";
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
           // var result1= pp.SaveOrUpdateNwmAyarlar(item);
           // var ayarlarItem = pp.GetNwmAyarlar(result1);

           // Console.WriteLine(ayarlarItem.google);
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

         
            string cmdText = "call sp_test(@n, @f);";
            //var mySqlParameters = new List<MySqlParameter>();
            //mySqlParameters.Add(new MySqlParameter("@n", "steve"));
            //mySqlParameters.Add(new MySqlParameter("@f", @"d:\temp\test.txt"));
            //MySqlHelper.ExecuteNonQuery(connectionString, cmdText, mySqlParameters.ToArray());

            Console.WriteLine("connectionString:" + ConnectionString);
            //cmdText = "select 1;";
            //DataSet dataSetResult = MySqlHelper.ExecuteDataset(connectionString, cmdText, null);
            //Console.WriteLine("total" + dataSetResult.Tables.Count);
            var databaseMetaData = tableRepository.GetAllMySqlTables(ConnectionString);
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
