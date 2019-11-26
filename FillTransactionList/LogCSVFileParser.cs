using System;
using System.IO;
using System.Text;

namespace FillTransactionList
{
    public class LogCSVFileParser
    {
        public string FilePath;
        private FileStream fs;
        private UTF8Encoding utf;        

        public LogCSVFileParser(string _FilePath)
        {
            this.FilePath = _FilePath;
        }

        public void OpenNewLogFile()
        {
            // Delete the file if it exists.
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            try
            {
                fs = File.OpenWrite(FilePath); 
            } 
            catch(IOException e)
            {
                Console.WriteLine(e);
            }

            utf = new UTF8Encoding();   
            
            if(fs.CanWrite)
            {
                Console.WriteLine("LogFile has been opened.");
            }else
            {
                Console.WriteLine("LogFile has not been opened.");
            }
        }

        public void WriteToLogFile(string input)
        {
            
            if(fs.CanWrite)
            {
                byte[] encoded = utf.GetBytes(input);
                fs.Write(encoded, 0 , utf.GetByteCount(input));                
            }
            else
            {
                Console.WriteLine($"Could not write {input}");
            }
        }

        public void CloseStream()
        {
            try{
                fs.Dispose();
            }
            catch(IOException e)
            {
                Console.WriteLine("FileStream could not be disposed.");
            }
        }
    }
}