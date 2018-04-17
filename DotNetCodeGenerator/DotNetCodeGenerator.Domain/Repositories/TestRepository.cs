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
        public string ConnectionString = "Server=174.128.194.106;Database=polbot2;Uid=emin;Pwd=145145145Aa;Port=3306"; //ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;


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

    }





}
