using System;
using System.Collections.Generic;
using System.Text.Json;

namespace FillTransactionList
{
    class Program
    {
        static void Main(string[] args)
        {
            FillTransactionList tList;
            if(args.Length > 0)
            {
                tList = new FillTransactionList(args[0]);
                tList.OpenAndReadTransactionsList();
                //tList.PrintTransactionList();
                var jsonString = JsonSerializer.Serialize(tList.transactions);
                Console.WriteLine(jsonString);                
            }
            else
            {
                Console.WriteLine("Need file path in first argument.");
            }
        
        }
    }
}
