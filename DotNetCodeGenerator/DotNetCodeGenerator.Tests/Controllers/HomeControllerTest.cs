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

        public string MySqlConnectionString
        {

            get
            {
                return ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString.ToStr();
            }
        }

        public string ConnectionString
        {

            get
            {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToStr();
            }
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            //var kernel = NinjectWebCommon.CreatePublicKernel();
            //_sut = kernel.Resolve<HomeController>();
        }
        [TestMethod]
        public void ParseSqlStatement()
        {
            string txt = @"

USE [TestEY]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 4/28/2018 12:53:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
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
	[CreatedDate] [datetime2](7) NOT NULL,
	[ImageState] [bit] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
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

ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO



";


            String mySql = @"
CREATE TABLE `urunler` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `baslik_tr` varchar(255) DEFAULT NULL,
  `keywords_tr` varchar(255) DEFAULT NULL,
  `katID` varchar(11) DEFAULT '0',
  `ozet_tr` text,
  `detay_tr` text,
  `sira` int(11) DEFAULT '1000',
  `tarih` datetime DEFAULT NULL,
  `durum` tinyint(1) DEFAULT NULL,
  `baslik_en` varchar(255) DEFAULT NULL,
  `keywords_en` varchar(255) DEFAULT NULL,
  `ozet_en` text,
  `detay_en` text,
  `seo` varchar(255) DEFAULT NULL,
  `tip` int(4) DEFAULT '1' COMMENT '1: motor, 2: yelken',
  `link` varchar(255) DEFAULT NULL,
  `vitrin` int(1) DEFAULT '0',
  `image` varchar(255) DEFAULT NULL,
  `youtube` varchar(255) DEFAULT NULL,
  `fiyat` double DEFAULT NULL,
  `tamam` varchar(255) CHARACTER SET utf8 COLLATE utf8_turkish_ci DEFAULT NULL,
  `baslik_de` varchar(255) DEFAULT NULL,
  `keywords_de` varchar(255) DEFAULT NULL,
  `ozet_de` text,
  `detay_de` text,
  `adres_de` text,
  `online` tinyint(1) DEFAULT NULL,
  `ColorID` int(11) DEFAULT NULL,
  `RegionID` int(11) DEFAULT NULL,
  `GrapeID` int(11) DEFAULT NULL,
  `yemek_tercihi_tr` text,
  `haftanin` tinyint(1) DEFAULT NULL,
  `yemek_tercihi_en` text,
  `stok` varchar(255) DEFAULT NULL,
  `yeni` tinyint(1) DEFAULT NULL,
  `yil` varchar(255) DEFAULT NULL,
  `miktar` varchar(255) DEFAULT NULL,
  `eski_fiyat` varchar(255) DEFAULT NULL,
  `alkol_orani` varchar(255) DEFAULT NULL,
  `kdv` varchar(255) DEFAULT NULL,
  `encok` tinyint(1) DEFAULT NULL,
  `harita` text CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `baslik_ar` varchar(255) DEFAULT NULL,
  `keywords_ar` varchar(255) DEFAULT NULL,
  `ozet_ar` text,
  `detay_ar` text,
  `baslik_ru` varchar(255) DEFAULT NULL,
  `keywords_ru` varchar(255) DEFAULT NULL,
  `ozet_ru` text,
  `detay_ru` text,
  `teknik` text,
  `renk` varchar(255) DEFAULT NULL,
  `kilit` varchar(255) DEFAULT NULL,
  `kaplama` varchar(255) DEFAULT NULL,
  `aksesuar` varchar(255) DEFAULT NULL,
  `aksesuarr` varchar(255) DEFAULT NULL,
  `markaID` int(11) DEFAULT NULL,
  `sektorID` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

";

            var metadata = SqlParserHelper.ParseSqlCreateStatement(mySql);
            var r = new CodeGeneratorResult();
            r.ModifiedTableName = "NwmProducts";
            CodeProducerHelper CodeProducerHelper = new CodeProducerHelper();
            CodeProducerHelper.DatabaseMetadata = metadata;
            CodeProducerHelper.CodeGeneratorResult = r;
            CodeProducerHelper.GenerateTableItem();
            Console.WriteLine(r.TableClassItem);
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
                pp.SaveOrUpdateNwmTest(new NwmTest() { Name = (i + 100) + Guid.NewGuid().ToStr() });
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
            //var databaseMetaData = tableRepository.GetAllTables(ConnectionString);
            //tableRepository.GetSelectedTableMetaData(databaseMetaData, "TestEY_2.dbo.Products");

            DatabaseMetadata databaseMetaData = tableRepository.GetAllMySqlTables(MySqlConnectionString);
            tableRepository.GetSelectedMysqlTableMetaData(databaseMetaData, "def.db_kodyazan.urunler");
            Console.WriteLine(XmlParserHelper.ToXml(databaseMetaData));

            //CodeGeneratorResult codeGeneratorResult = new CodeGeneratorResult();
            //codeGeneratorResult.MySqlConnectionString = MySqlConnectionString;

            //codeGeneratorResult.ModifiedTableName = "NwmProducts";
            //CodeProducerHelper.CodeGeneratorResult = codeGeneratorResult;
            //CodeProducerHelper.DatabaseMetadata = databaseMetaData;
            //      CodeProducerHelper.GenerateMySqlSaveOrUpdateStoredProcedure();

            //  Console.WriteLine(codeGeneratorResult.MySqlSaveOrUpdateStoredProc);

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

            var id = testRepo.SaveOrUpdateNwmCurrencyConfig(item);
            Console.WriteLine("id:" + id);
        }
    }
}
