using DotNetCodeGenerator.Domain.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCodeGenerator.Domain.Helpers;

namespace DotNetCodeGenerator.Domain.Repositories
{

    public class TestRepository
    {

        public static string ConnectionStringKey = "MysqlConnectionString";
        public string ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;



        public List<NwmHaberler> GetNwmHaberlers()
        {
            var list = new List<NwmHaberler>();
            String commandText = @"SELECT * FROM db_kodyazan.haberler ORDER BY id DESC";
            var parameterList = new List<MySqlParameter>();
            DataSet dataSet = MySqlHelper.ExecuteDataset(ConnectionString, commandText, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                list = dataSet.Tables[0].ToList<NwmHaberler>();
            }
            return list;
        }
        public void DeleteNwmHaberler(int id)
        {
            String commandText = @"DELETE FROM db_kodyazan.haberler WHERE id=@id";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@id", id));
            MySqlHelper.ExecuteNonQuery(ConnectionString, commandText, parameterList.ToArray());
        }

        public NwmHaberler GetNwmHaberler(int id)
        {
            var resultEntity = new NwmHaberler();
            String commandText = @"SELECT * FROM db_kodyazan.haberler WHERE id=@id";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@id", id));
            DataSet dataSet = MySqlHelper.ExecuteDataset(ConnectionString, commandText, parameterList.ToArray());
            resultEntity = dataSet.Tables[0].ToList<NwmHaberler>().FirstOrDefault();
            return resultEntity;
        }
        public int SaveOrUpdateNwmHaberler(NwmHaberler item)
        {
            String commandText = @"CALL SaveOrUpdateNwmHaberler(@id_IN,@sira_IN,@tarih_IN,@durum_IN,@seo_IN,@link_IN,@baslik_tr_IN,@keywords_tr_IN,@ozet_tr_IN,@detay_tr_IN,@baslik_en_IN,@keywords_en_IN,@ozet_en_IN,@detay_en_IN,@haberTarihi_IN,@baslik_de_IN,@keywords_de_IN,@ozet_de_IN,@detay_de_IN,@tip_IN,@baslik_ar_IN,@keywords_ar_IN,@ozet_ar_IN,@detay_ar_IN,@yorum_tr_IN,@yorum_en_IN)";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@id_IN", item.id));
            parameterList.Add(new MySqlParameter("@sira_IN", item.sira));
            parameterList.Add(new MySqlParameter("@tarih_IN", item.tarih));
            parameterList.Add(new MySqlParameter("@durum_IN", item.durum));
            parameterList.Add(new MySqlParameter("@seo_IN", item.seo.ToStr()));
            parameterList.Add(new MySqlParameter("@link_IN", item.link.ToStr()));
            parameterList.Add(new MySqlParameter("@baslik_tr_IN", item.baslik_tr.ToStr()));
            parameterList.Add(new MySqlParameter("@keywords_tr_IN", item.keywords_tr.ToStr()));
            parameterList.Add(new MySqlParameter("@ozet_tr_IN", item.ozet_tr.ToStr()));
            parameterList.Add(new MySqlParameter("@detay_tr_IN", item.detay_tr.ToStr()));
            parameterList.Add(new MySqlParameter("@baslik_en_IN", item.baslik_en.ToStr()));
            parameterList.Add(new MySqlParameter("@keywords_en_IN", item.keywords_en.ToStr()));
            parameterList.Add(new MySqlParameter("@ozet_en_IN", item.ozet_en.ToStr()));
            parameterList.Add(new MySqlParameter("@detay_en_IN", item.detay_en.ToStr()));
            parameterList.Add(new MySqlParameter("@haberTarihi_IN", item.haberTarihi));
            parameterList.Add(new MySqlParameter("@baslik_de_IN", item.baslik_de.ToStr()));
            parameterList.Add(new MySqlParameter("@keywords_de_IN", item.keywords_de.ToStr()));
            parameterList.Add(new MySqlParameter("@ozet_de_IN", item.ozet_de.ToStr()));
            parameterList.Add(new MySqlParameter("@detay_de_IN", item.detay_de.ToStr()));
            parameterList.Add(new MySqlParameter("@tip_IN", item.tip));
            parameterList.Add(new MySqlParameter("@baslik_ar_IN", item.baslik_ar.ToStr()));
            parameterList.Add(new MySqlParameter("@keywords_ar_IN", item.keywords_ar.ToStr()));
            parameterList.Add(new MySqlParameter("@ozet_ar_IN", item.ozet_ar.ToStr()));
            parameterList.Add(new MySqlParameter("@detay_ar_IN", item.detay_ar.ToStr()));
            parameterList.Add(new MySqlParameter("@yorum_tr_IN", item.yorum_tr.ToStr()));
            parameterList.Add(new MySqlParameter("@yorum_en_IN", item.yorum_en.ToStr()));
            int id = MySqlHelper.ExecuteScalar(ConnectionString, commandText, parameterList.ToArray()).ToInt();
            return id;
        }



        public int SaveOrUpdateNwmTest(NwmTest item)
        {
            String commandText = @"CALL SaveOrUpdateNwmTest(@Id,@Name)";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@Id", item.Id));
            parameterList.Add(new MySqlParameter("@Name", item.Name.ToStr()));
            int id = MySqlHelper.ExecuteScalar(ConnectionString, commandText, parameterList.ToArray()).ToInt();
            return id;
        }


        public List<NwmCurrencyConfig> GetNwmCurrencyConfigs()
        {
            var list = new List<NwmCurrencyConfig>();
            String commandText = @"SELECT * FROM polbot2.currency_config ORDER BY currency_config_id DESC";
            var parameterList = new List<MySqlParameter>();
            DataSet dataSet = MySqlHelper.ExecuteDataset(ConnectionString, commandText, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                list = dataSet.Tables[0].ToList<NwmCurrencyConfig>();
            }
            return list;
        }
        public void DeleteNwmCurrencyConfig(int currency_config_id)
        {
            String commandText = @"DELETE FROM polbot2.currency_config WHERE currency_config_id=@currency_config_id";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@currency_config_id", currency_config_id));
            MySqlHelper.ExecuteNonQuery(ConnectionString, commandText, parameterList.ToArray());
        }

        public NwmCurrencyConfig GetNwmCurrencyConfig(int currency_config_id)
        {
            var resultEntity = new NwmCurrencyConfig();
            String commandText = @"SELECT * FROM polbot2.currency_config WHERE currency_config_id=@currency_config_id";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@currency_config_id", currency_config_id));
            DataSet dataSet = MySqlHelper.ExecuteDataset(ConnectionString, commandText, parameterList.ToArray());
            resultEntity = dataSet.Tables[0].ToList<NwmCurrencyConfig>().FirstOrDefault();
            return resultEntity;
        }
        public int SaveOrUpdateNwmCurrencyConfig(NwmCurrencyConfig item)
        {

            // CALL(<{ currency_config_id INT}>,
            // <{ buy_at_price float}>,
            // <{ buy_on_percent float}>,
            // <{ order_timeout_in_hour float}>,
            // <{ buyable bit(1)}>,
            // <{ currency_pair varchar(255)}>, 
            // <{ sell_at_price float}>,
            // <{ sell_on_percent float}>, 
            // <{ sellable bit(1)}>
            //, <{ usable_balance_percent float}>,
            // <{ bot_user int(11)}>);
            // CALL `polbot2`.`sp_SaveOrUpdateCurrency_config`(<{currency_config_id INT}>, <{buy_at_price float}>, <{buy_on_percent float}>, <{order_timeout_in_hour float}>, <{buyable bit(1)}>, <{currency_pair varchar(255)}>, <{sell_at_price float}>, <{sell_on_percent float}>, <{sellable bit(1)}>, <{usable_balance_percent float}>, <{bot_user int(11)}>);
            String commandText = @"CALL `polbot2`.`sp_SaveOrUpdateCurrency_config`(
@currency_config_id,
@buy_at_price,
@buy_on_percent,
@order_timeout_in_hour,
@buyable,
@currency_pair,
@sell_at_price,
@sell_on_percent,
@sellable,
@usable_balance_percent,
@bot_user);";
            commandText = commandText.Replace(Environment.NewLine, "");
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@currency_config_id", item.currency_config_id));
            parameterList.Add(new MySqlParameter("@buy_at_price", item.buy_at_price));
            parameterList.Add(new MySqlParameter("@buy_on_percent", item.buy_on_percent));
            parameterList.Add(new MySqlParameter("@order_timeout_in_hour", item.order_timeout_in_hour));
            parameterList.Add(new MySqlParameter("@buyable", item.buyable));
            parameterList.Add(new MySqlParameter("@currency_pair", item.currency_pair.ToStr()));
            parameterList.Add(new MySqlParameter("@sell_at_price", item.sell_at_price));
            parameterList.Add(new MySqlParameter("@sell_on_percent", item.sell_on_percent));
            parameterList.Add(new MySqlParameter("@sellable", item.sellable));
            parameterList.Add(new MySqlParameter("@usable_balance_percent", item.usable_balance_percent));
            parameterList.Add(new MySqlParameter("@bot_user", item.bot_user));
            int id = MySqlHelper.ExecuteScalar(ConnectionString, commandText, parameterList.ToArray()).ToInt();
            return id;
        }



        public List<NwmAyarlar> GetNwmAyarlars()
        {
            var list = new List<NwmAyarlar>();
            String commandText = @"SELECT * FROM db_kodyazan.ayarlar ORDER BY id DESC";
            var parameterList = new List<MySqlParameter>();
            DataSet dataSet = MySqlHelper.ExecuteDataset(ConnectionString, commandText, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                list = dataSet.Tables[0].ToList<NwmAyarlar>();
            }
            return list;
        }
        public void DeleteNwmAyarlar(int id)
        {
            String commandText = @"DELETE FROM db_kodyazan.ayarlar WHERE id=@id";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@id", id));
            MySqlHelper.ExecuteNonQuery(ConnectionString, commandText, parameterList.ToArray());
        }

        public NwmAyarlar GetNwmAyarlar(int id)
        {
            var resultEntity = new NwmAyarlar();
            String commandText = @"SELECT * FROM db_kodyazan.ayarlar WHERE id=@id";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@id", id));
            DataSet dataSet = MySqlHelper.ExecuteDataset(ConnectionString, commandText, parameterList.ToArray());
            resultEntity = dataSet.Tables[0].ToList<NwmAyarlar>().FirstOrDefault();
            return resultEntity;
        }
        public int SaveOrUpdateNwmAyarlar(NwmAyarlar item)
        {
            String commandText = @"CALL SaveOrUpdateNwmAyarlar(@id,
@siteBasligi,
@anahtarKelimeler,
@google,
@siteAciklamasi,
@firmaAdi,@telefon,@telefon2,
@faks,@eposta,@adres,@yetkili,
@username,@password,@logKayit,
@durum,@siteUrl,@panelUrl,@smtpSunucu,
@smtpPort,@smtpKullanici,@smtpSifre,
@smtpAd,@smtpMetod,@smtpDurum,
@facebook,@twitter,@gplus,@foursquare,@map)";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@id", item.id));
            parameterList.Add(new MySqlParameter("@siteBasligi", item.siteBasligi.ToStr()));
            parameterList.Add(new MySqlParameter("@anahtarKelimeler", item.anahtarKelimeler.ToStr()));
            parameterList.Add(new MySqlParameter("@google", item.google.ToStr()));
            parameterList.Add(new MySqlParameter("@siteAciklamasi", item.siteAciklamasi.ToStr()));
            parameterList.Add(new MySqlParameter("@firmaAdi", item.firmaAdi.ToStr()));
            parameterList.Add(new MySqlParameter("@telefon", item.telefon.ToStr()));
            parameterList.Add(new MySqlParameter("@telefon2", item.telefon2.ToStr()));
            parameterList.Add(new MySqlParameter("@faks", item.faks.ToStr()));
            parameterList.Add(new MySqlParameter("@eposta", item.eposta.ToStr()));
            parameterList.Add(new MySqlParameter("@adres", item.adres.ToStr()));
            parameterList.Add(new MySqlParameter("@yetkili", item.yetkili.ToStr()));
            parameterList.Add(new MySqlParameter("@username", item.username.ToStr()));
            parameterList.Add(new MySqlParameter("@password", item.password.ToStr()));
            parameterList.Add(new MySqlParameter("@logKayit", item.logKayit));
            parameterList.Add(new MySqlParameter("@durum", item.durum));
            parameterList.Add(new MySqlParameter("@siteUrl", item.siteUrl.ToStr()));
            parameterList.Add(new MySqlParameter("@panelUrl", item.panelUrl.ToStr()));
            parameterList.Add(new MySqlParameter("@smtpSunucu", item.smtpSunucu.ToStr()));
            parameterList.Add(new MySqlParameter("@smtpPort", item.smtpPort.ToStr()));
            parameterList.Add(new MySqlParameter("@smtpKullanici", item.smtpKullanici.ToStr()));
            parameterList.Add(new MySqlParameter("@smtpSifre", item.smtpSifre.ToStr()));
            parameterList.Add(new MySqlParameter("@smtpAd", item.smtpAd.ToStr()));
            parameterList.Add(new MySqlParameter("@smtpMetod", item.smtpMetod.ToStr()));
            parameterList.Add(new MySqlParameter("@smtpDurum", item.smtpDurum.ToStr()));
            parameterList.Add(new MySqlParameter("@facebook", item.facebook.ToStr()));
            parameterList.Add(new MySqlParameter("@twitter", item.twitter.ToStr()));
            parameterList.Add(new MySqlParameter("@gplus", item.gplus.ToStr()));
            parameterList.Add(new MySqlParameter("@foursquare", item.foursquare.ToStr()));
            parameterList.Add(new MySqlParameter("@map", item.map.ToStr()));
            int id = MySqlHelper.ExecuteScalar(ConnectionString, commandText, parameterList.ToArray()).ToInt();
            return id;
        }


    }





}
