using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Entities
{
    public class NwmCurrencyConfig
    {
        public int currency_config_id { get; set; }
        public float buy_at_price { get; set; }
        public float buy_on_percent { get; set; }
        public float order_timeout_in_hour { get; set; }
        public Boolean buyable { get; set; }
        public string currency_pair { get; set; }
        public float sell_at_price { get; set; }
        public float sell_on_percent { get; set; }
        public Boolean sellable { get; set; }
        public float usable_balance_percent { get; set; }
        public int bot_user { get; set; }
    }
}
