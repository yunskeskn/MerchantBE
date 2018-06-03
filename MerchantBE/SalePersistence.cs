using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MerchantBE.Request;
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

        public long insertTransaction(SaleRequest saleRequest)
        {
            string sqlString = "Insert into merchant_transaction (merchant_no, terminal_no, amount, transaction_status)values('" + saleRequest.merchant_no + "','" + saleRequest.terminal_no + "'," + saleRequest.amount + ", 'P')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long guid = cmd.LastInsertedId;
            return guid;
        }

    }
}
