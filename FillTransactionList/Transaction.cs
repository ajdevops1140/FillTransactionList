


namespace FillTransactionList
{
    public class Transaction 
    {
        public int id { get; set; }
        public string? _Date { get; set; }
        public string? _Description { get; set; }
        public string? _Debit { get; set; }  //decimal
        public string? _Credit { get; set; }  //decimal
        public string? _Category { get; set; }

        public override string ToString()
        {
            return $"ID: {id}  DATE:{_Date}  DESCRIPTION: {_Description}  DEBIT: {_Debit}  CREDIT: {_Credit}  CATEGORY: {_Category}";
        }
    }
}