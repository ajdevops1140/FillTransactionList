using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FillTransactionList
{
    public class FillTransactionList
    {
        private string path;
        public List<Transaction> transactions;
        private LogCSVFileParser log;

        public FillTransactionList(string filepath = "")
        {
            path = filepath;
            transactions = null;
        }

        public void OpenAndReadTransactionsList()
        {
            int totalQuoteCount = 0;
            int quoteCount = 0;
            char ch = '\0';
            string logPath;

            StringBuilder data = new StringBuilder();
            Transaction temp = null;
            
            if(path != "")
            {
                try
                {
                    using(StreamReader sr = new StreamReader(path))
                    {
                        //Get Directory log path
                        string dir = Directory.GetCurrentDirectory();
                        logPath = $"{dir}\\FailedTransactionLog.txt";
                        log = new LogCSVFileParser(logPath);
                        log.OpenNewLogFile();

                        transactions = new List<Transaction>();       
                        while(sr.Peek() > -1)
                        {                        
                            temp = new Transaction();
                            //Read file line by line
                            while(sr.Peek() > -1 && sr.Peek() != '\n')
                            {                            
                                //Check for first quote
                                if(sr.Peek() == '"')
                                {
                                    sr.Read(); //Read and throw away the quote
                                    quoteCount++;
                                    totalQuoteCount++;
                                    //Console.WriteLine($"QuoteCount: {quoteCount}");  //For checking whether quotes are being read

                                    while(sr.Peek() > -1 && sr.Peek() != '"')
                                    {
                                        ch = (char)sr.Read();
                                        data.Append(ch);
                                    }

                                    //Add data to transaction here
                                    SetTransactionData(quoteCount, ref temp, ref data);

                                    //Read second quote and throw away
                                    //Throw away comma if available
                                    if(sr.Peek() == '"')
                                    {
                                        sr.Read();
                                        if(sr.Peek() == ',')
                                        {
                                            sr.Read();
                                        }
                                    }                                
                                }//End of   

                                
                                data.Clear(); 
                            }//End of Read Line or End of File
                            
                            //----------------Clean up operations
                            quoteCount = 0;

                            if(sr.Peek() == '\n')
                            {
                                sr.Read(); //Advance the reader
                            }

                            //Add the transaction data to the list
                            if(CheckTransaction(ref temp))  //Set Transaction data if data is true
                            {
                                transactions.Add(temp);
                            }
                            else
                            {   
                                string t = temp.ToString();
                                log.WriteToLogFile($"{t}\n"); //Log the failed transaction
                            }                       
                                                       
                        }  
                        log.CloseStream();                      
                    }
                }
                catch(IOException e)
                {
                    //Console.WriteLine($"Path : {path} was not able to be opened.");
                }
            }
            else
            {
                //Console.WriteLine("Path is empty.");
            }            
        }

        private bool CheckTransaction(ref Transaction temp)
        {
            return !(String.IsNullOrEmpty(temp._Date) || String.IsNullOrEmpty(temp._Description) || !(String.IsNullOrEmpty(temp._Credit) ^ String.IsNullOrEmpty(temp._Debit)));
        }

        private void SetTransactionData(int quoteCount, ref Transaction temp, ref StringBuilder sb)
        {
            /*
            public int id;
            public string? _Date;
            public string? _Description;
            public string? _Debit;   //decimal
            public string? _Credit;  //decimal
            public string? _Category;
            */
            switch(quoteCount)
            {
                case 1:
                    int id = 0;
                    string s = sb.ToString();
                    if(Int32.TryParse(s,out id))
                    {
                        temp.id = id;
                    }
                    else
                    {
                        //Console.WriteLine($"Temp ID: {s} could not be set.");
                    }
                break;
                case 2:
                    temp._Date = sb.ToString();
                break;
                case 3:
                    temp._Description = sb.ToString();
                break;
                case 4:
                    temp._Debit = sb.ToString();
                break;
                case 5:
                    temp._Credit = sb.ToString();
                break;
                case 6:
                    temp._Category = sb.ToString();
                break;                
                default:
                    //Console.WriteLine($"Quote Count is not a valid value {quoteCount}");
                break;
            }
        } 

        public void PrintTransactionList()
        {
            if(transactions != null)
            {
                foreach(Transaction t in transactions)
                {
                    Console.WriteLine(t.ToString());
                }
                Console.WriteLine($"Total Transactions: {transactions.Count}");
            }
        }
    }
}