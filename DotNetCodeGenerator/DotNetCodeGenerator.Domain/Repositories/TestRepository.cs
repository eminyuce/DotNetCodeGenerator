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

        public static string ConnectionStringKey = "";
        public string ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;


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
            String commandText = @"call def.polbot2.currency_SaveOrUpdateNwmCurrencyConfig";
            var parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@currencyconfigid", item.currency_config_id));
            parameterList.Add(new MySqlParameter("@buyatprice", item.buy_at_price));
            parameterList.Add(new MySqlParameter("@buyonpercent", item.buy_on_percent));
            parameterList.Add(new MySqlParameter("@ordertimeoutinhour", item.order_timeout_in_hour));
            parameterList.Add(new MySqlParameter("@buyable", item.buyable));
            parameterList.Add(new MySqlParameter("@currencypair", item.currency_pair.ToStr()));
            parameterList.Add(new MySqlParameter("@sellatprice", item.sell_at_price));
            parameterList.Add(new MySqlParameter("@sellonpercent", item.sell_on_percent));
            parameterList.Add(new MySqlParameter("@sellable", item.sellable));
            parameterList.Add(new MySqlParameter("@usablebalancepercent", item.usable_balance_percent));
            parameterList.Add(new MySqlParameter("@botuser", item.bot_user));
            int id = MySqlHelper.ExecuteScalar(ConnectionString, commandText, parameterList.ToArray()).ToInt();
            return id;
        }

    }





}
