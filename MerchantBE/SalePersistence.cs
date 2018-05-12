using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MerchantBE.Models;
using MySql.Data;

namespace MerchantBE
{
    public class SalePersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public SalePersistence()
        {
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=Yunus;pwd=av7vgqggx;database=merchant";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                throw;
            }
        }

        public long insertTransaction(SaleInfo saleInfo)
        {
            string sqlString = "Insert into transaction (merchant_no, terminal_no, amount, transaction_status)values('" + saleInfo.merchant_no + "','" + saleInfo.terminal_no + "'," + saleInfo.amount + ", 'P')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long guid = cmd.LastInsertedId;
            return guid;
        }

    }
}
