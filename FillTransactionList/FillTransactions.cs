using System;
using System.Collections.Generic;
using System.IO;




namespace FillTransactionList
{
    public class FillTransactionList
    {
        private string path;
        private List<Transaction> transactions;

        public FillTransactionList(string filename = "")
        {
            path = filename;
            transactions = null;
        }

        public OpenAndReadTransactionsList()
        {
            try
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    
                }
            }
            catch(IOException e)
            {

            }
        }
    }
}