using System;

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
                tList.PrintTransactionList();
            }
            else
            {
                Console.WriteLine("Need file path in first argument.");
            }
        }
    }
}
