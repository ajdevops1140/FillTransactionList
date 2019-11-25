


namespace FillTransactionList
{
    public class Transaction 
    {
        public int id;
        public string? _Date;
        public string? _Description;
        public string? _Debit;   //decimal
        public string? _Credit;  //decimal
        public string? _Category;

        public override string ToString()
        {
            return $"ID: {id}  DATE:{_Date}  DESCRIPTION: {_Description}  DEBIT: {_Debit}  CREDIT: {_Credit}  CATEGORY: {_Category}";
        }
    }
}