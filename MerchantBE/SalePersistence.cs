﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MerchantBE.Request;
using MerchantBE.Response;
using MySql.Data;

namespace MerchantBE
{
    public class SalePersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public SalePersistence()
        {
            string myConnectionString;
            myConnectionString = "server=198.71.227.92;Port=3306;uid=yunskeskn;pwd=Canik_6168;database=poc_be;SslMode=none";
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
            string sqlString = "Insert into MERCHANT_TRANSACTION (merchant_no, terminal_no, amount, transaction_status)values('" + saleRequest.merchant_no + "','" + saleRequest.terminal_no + "'," + saleRequest.amount + ", 'P')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long guid = cmd.LastInsertedId;
            return guid;

        }

        public int updateTransactionfromBank(long guid,SaleResponse saleResponse)
        {
            string sqlString = "Update MERCHANT_TRANSACTION set bank_transaction_guid =" + saleResponse.bank_transaction_guid+", token_data = '"+saleResponse.token_data+"' where guid ="+guid;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            int result = cmd.ExecuteNonQuery();
            
            return result;
        }

        public int updateTransactionStatus(CompleteTransactionRequest completeTransactionRequest)
        {
            string sqlString = "Update MERCHANT_TRANSACTION set transaction_status ='" + completeTransactionRequest.status + "' where guid = " + completeTransactionRequest.merchant_guid;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            int result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}
