using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace StoreCreator
{
    public class LoggingUserActions
    {

        public static void CreateUserDirectory(int UserId)
        {
           
            FileInfo fi = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("LoggingUserActions\\MemberId=" + UserId.ToString() + ".txt"));

            if (!fi.Exists)
            {
                FileStream fs = fi.Create();
                fs.Close();
            }
          
        }

        [Serializable()]
        private class LogEntry
        {
            private string message;
            private DateTime date;
            private int UserId;

            public string Message
            {
                get {return message;}
                set {message = value;}
            }
            public DateTime Date
            {
                get {return date;}
                set {date = value;}
            }

            public int userid
            {
                get {return UserId;}
                set {UserId = value;}
            }
            public LogEntry(string m, DateTime d, int UserId)
            {
                this.date = d;
                this.message = m;
                this.userid = UserId;
            }
        }

        public static void LogUserProductToCart(int UserId, int ProductId, int Quantity) // works
        {
            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("FrontEndUserActions\\UserProductActions\\CartActions.txt"), FileMode.Open);

            try
            {
                using (fs)
                {
                    using (StreamWriter wr = new StreamWriter(fs))
                    {
                        wr.WriteLine("UserId: " + UserId.ToString());
                        wr.WriteLine("ProductId: " + ProductId.ToString());
                        wr.WriteLine("Quantity: " + Quantity.ToString());
                        wr.WriteLine(DateTime.Now);
                        wr.WriteLine(" ");

                        wr.Flush();
                        wr.Close();

                    }
                }
            }
            finally
            {
                fs.Close();
            }
        }

        public static void WriteProductSearchCriteria(string[] SearchTerms, int UserId)// works
        {
            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("FrontEndUserActions\\UserProductActions\\SearchTerms.txt"), FileMode.Open);

            using (fs)
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {

                    for (int i = 0; i < SearchTerms.Length; i++)
                    {
                        writer.WriteLine(SearchTerms[i]);
                    }

                    writer.WriteLine(DateTime.Now.ToShortDateString());
                    writer.Flush();
                    writer.Close();
                }
            }
        }


        public static void ReadStringToBytes(int UserId, string FileName)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(FileName, FileMode.Open);
                byte[] data = new byte[fs.Length];

                for (int i = 0; i < fs.Length; i++)
                {
                    data[i] = (byte)fs.ReadByte();

                }

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

        }


        private static void ReadFiles(List<string> FileNames, int UserId, string[] LineCommands)
        {
            foreach (string FileName in FileNames)
            {
                FileStream fs = new FileStream(FileName, FileMode.Open);

                using (fs)
                {
                    try
                    {
                        byte[] data = new byte[fs.Length];

                        for (int i = 0; i < fs.Length; i++)
                        {
                            data[i] = (byte)fs.ReadByte();
                        }

                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
        }

      


        public static void WriteLogToFile(int UserId, string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Open);
            StreamWriter writer = new StreamWriter(fs);

            writer.Write("Button One Has Been Click by User <br />");
            writer.WriteLine(UserId);

            writer.Flush();
            writer.Close();

        }

        public static void ReadLog(int UserId, string FileName, out StringBuilder s)
        {
            StreamReader r = File.OpenText(FileName);
            string inputString = r.ReadToEnd();
            StringBuilder str = new StringBuilder(inputString);

            
            s = str;
        }
        public static void WriteBinaryStringFile(int UserId, string StringToWrite, string FileName)
        {

            FileInfo fi = new FileInfo(FileName);

            if (!fi.Exists)
            {
                fi.Create();

            }
            else
            {
                FileStream stream = new FileStream(FileName, FileMode.Open);

                BinaryWriter binary = new BinaryWriter(stream);
                binary.Write(StringToWrite);

                binary.Flush();
                binary.Close();

                stream.Close();
           
            }         
        }

        public static void WriteBinaryStringFiles(int UserId, string StringToWrite, string FileName)
        {
            FileInfo fi = new FileInfo(FileName);

            if (!fi.Exists)
            {
                fi.Create();
            }
            else
            {
                FileStream fs = new FileStream(FileName, FileMode.Open);

                using (fs)
                {
                    using (BinaryWriter wr = new BinaryWriter(fs))
                    {
                        wr.Write(StringToWrite);

                        wr.Flush();
                        wr.Close();

                        fs.Close();
                    }
                }

            }
        }

        public static void WritingToMultipleBinaryFiles(int UserId, string[] Strings, string[] FileNames)
        {
            for (int i = 0; i < Strings.Length; i++)
            {
                FileInfo fi = new FileInfo(FileNames[i]);

                if (!fi.Exists)
                {
                    fi.Create();

                }
                else
                {
                    using (FileStream fs = new FileStream(FileNames[i], FileMode.Open))
                    {
                        using (BinaryWriter wr = new BinaryWriter(fs))
                        {
                            wr.Write(Strings[i]);

                            wr.Flush();
                            wr.Close();
                        }
                    }


                }
            }
        }

        public static void LogMessage(int UserId, string Message,string FileName, bool LogAlreadyCreated)
        {
            FileMode mode;

            if(LogAlreadyCreated == false)
            {
                mode = FileMode.Create;
            }
            else
            {
                mode = FileMode.Append;
            }

            using(FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                StreamWriter wr = new StreamWriter(fs);

                using(wr)
                {
                    wr.Write(fs);

                    wr.Flush();
                    wr.Close();

                    fs.Close();
                }
            }
        }

        public static void LogMessageTwo(int UserId, string Message, string FileName, bool AlreadyLogged)
        {
            FileMode mode = new FileMode();

            if (AlreadyLogged)
            {
                mode = FileMode.Create;
            }
            else
            {
                mode = FileMode.Append;
            }

            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                using (StreamWriter wr = new StreamWriter(fs))
                {
                    wr.Write(Message);
                    wr.Close();
                    wr.Dispose();

                    fs.Close();
                }
            }

        }
        public static void ReadLogMessage(int UserId, string FileName, string Message)
        {
            FileInfo fi = new FileInfo(FileName);

            if (fi.Exists)
            {
                StringBuilder s = new StringBuilder();

                using (FileStream fs = new FileStream(fi.Name, FileMode.Open))
                {
                    using (StreamWriter wr = new StreamWriter(fs))
                    {
                        try
                        {
                            wr.Write(Message);
                        

                        }
                        catch (IOException err)
                        {

                        }
                        finally
                        {
                            wr.Close();
                            fs.Close();
                        }
                    }
                }
            }

        }

        public static void ReadLogMessage(int UserId, FileInfo fi)
        {
            if(fi.Exists)
            {
               StringBuilder str = new StringBuilder();

                using(FileStream fs = new FileStream(fi.Name, FileMode.Open))
                {
                   using(StreamReader rdr = new StreamReader(fs))
                   {
                       string line;

                       do
                       {
                           line = rdr.ReadLine();
                           str.Append(line + "<br />");

                       }while(line != null);

                   }

                }
            }
            else
            {
                throw new FileNotFoundException();
            }



        }






        public static void LogMultipleMessageToOneLogLineByLine(string[] Messages, string FileName, int UserId)
        {

            for(int i = 0; i < Messages.Length; i++)
            {
                StringBuilder str = new StringBuilder();

                using(FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    using(StreamWriter wr = new StreamWriter(fs))
                    {

                        wr.WriteLine(Messages[i]);
                        wr.WriteLine(DateTime.Now);

                        wr.Flush();
                        wr.Close();
                    }
                }         
            }
        }


        public static string ReadBinaryData(int UserId, string FileName)
        {

            BinaryReader r = new BinaryReader(File.OpenRead(FileName));
            string str;

            str = r.ReadString();

            return str;
        }




        public static void LogMessage(int UserId, string Message, bool LogAlreadyCreated, string FileName)
        {
            FileMode mode;

            if (LogAlreadyCreated == false)
            {
                mode = FileMode.Create;
            }
            else
            {
                mode = FileMode.Append;
            }

            using (FileStream fs = new FileStream(FileName, mode))
            {
                StreamWriter w = new StreamWriter(fs);
                w.WriteLine(DateTime.Now);
                w.WriteLine(Message);
                w.WriteLine();
                w.Close();

            }
        }

        public static string ReadLogMessage(FileInfo FileName, string UserId)
        {
            StringBuilder log = new StringBuilder();

            if (FileName.Exists)
            {

                using (FileStream fs = new FileStream(FileName.FullName, FileMode.Open))
                {
                    StreamReader r = new StreamReader(fs);

                    string line;

                    do
                    {
                        line = r.ReadLine();

                        if (line != null)
                        {
                            log.Append(line + "<br />");
                        }

                    } while (line != null);

                    r.Close();
                }
              
            }
            else
            {
                log.Append("There is no log file");
            }

            return log.ToString();
        }

        public static void WriteCompressionStream(string FileName, string UserId)
        {
            FileStream fs = new FileStream(FileName, FileMode.Create);

            GZipStream compFs = new GZipStream(fs, CompressionMode.Compress);

            StreamWriter w = new StreamWriter(compFs);
            w.Write("yyyyyy");

            w.Flush();
            fs.Close();


        }

        public static string ReadCompressionStream(string FileName, string UserId) // not working
        {
            FileStream fileStream = new FileStream(FileName, FileMode.Open);
                       
                GZipStream decompressedStream = new GZipStream(fileStream, CompressionMode.Decompress);
                StreamReader r = new StreamReader(decompressedStream);
                string  response = r.ReadToEnd();

                fileStream.Close();

                return response;
        }

        public static void WriteCompressionStream(string FileName, int UserId, string StringToWrite)
        {
            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                using(GZipStream gZip = new GZipStream(fs, CompressionMode.Compress))
                {
                   StreamWriter w = new StreamWriter(fs);

                    w.Write(StringToWrite);

                    w.Flush();
                    w.Close();
                    fs.Close();
                }
            }
        }




        public static void WriteToLogs(bool IsBinary, bool isCompressed, string FileName, string WhatToLog)
        {
             
            FileStream fs = new FileStream(FileName, FileMode.Open);

            if(isCompressed)
            {
               
                using(fs)
                {
                    using(GZipStream comp = new GZipStream(fs, CompressionMode.Compress))
                    {
                        
                        StreamWriter w = new StreamWriter(comp);

                        w.WriteLine(WhatToLog);
         
                        w.Flush();
                        w.Close();
                    }
                }               
            }
        }



        public static void AppendUserAction(int UserId, string Message, string FileName)
        {
            try
            {
                FileStream fs = new FileStream(FileName, FileMode.Append);

                using (fs)
                {
                    StreamWriter w = new StreamWriter(fs);
                    w.WriteLine(Message);

                    w.Flush();
                    w.Close();

                }
            }
            catch(Exception e)
            {
                if(e != null)
                {
                    LogExceptionToUserFile(e, FileName);
                }
            }

        }

        public static void AppendUserActions(int UserId, string[] Messages, string FileName)
        {


        }

        public static bool LogExceptionToUserFile(Exception e, string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open);
            bool IsLogged = false;

            try
            {

                using (fs)
                {
                    StreamWriter sw = new StreamWriter(fs);

                    StreamWriter w = new StreamWriter(fs);
                    w.WriteLine(DateTime.Now);
                    w.WriteLine(e.Message);
                    w.WriteLine(e.Source);
                    w.WriteLine(e.StackTrace);
                    w.WriteLine(e.TargetSite);
                    w.WriteLine(e.InnerException);
                    w.WriteLine(e.GetType().ToString());
                    w.WriteLine(e.InnerException);

                    w.Flush();
                    w.Close();

                    IsLogged = true;
                }
            }          
            finally
            {
                fs.Close();
            }

            return IsLogged;
        }

        public enum ProductAction
        {
            CreatedProduct,
            CreatedDiscount,
            CreatedPromotion,
            CreatedPage

        }

        private enum StoreAction
        {
            SearchStore,
            VisitStore
        }

    


        public static void LogUserActionForProducts(int UserId,string FileName, ProductAction Action)
        {
            FileStream fs = new FileStream(FileName, FileMode.Open);

            using (fs)
            {
               using (StreamWriter wr = new StreamWriter(fs))
               {
                   wr.WriteLine("CreatedId=" + UserId.ToString());
                   wr.WriteLine(DateTime.Now.ToString());
                  
                   switch(Action)
                   {
                       case ProductAction.CreatedProduct:
                           wr.WriteLine("CreatedProduct");

                           break;
                       case ProductAction.CreatedDiscount:
                           wr.WriteLine("CreatedDiscount");


                           break;
                       case ProductAction.CreatedPromotion:
                           wr.WriteLine("CreatedPromotion");


                           break;
                       case ProductAction.CreatedPage:
                           wr.WriteLine("CreatedPage");


                           break;
                       default:


                           break;
                   }

                   wr.Dispose();
               }              
            }

        }

        public enum TypeOfStream
        {
            StreamBinary,
            StreamFile,
            StreamMemory

        }
        public static void LogUserRegisterAndLogin(int UserId, bool IsLoggedIn, string FilePath, TypeOfStream StreamType)
        {
            if(StreamType == TypeOfStream.StreamBinary)
            {
                
                FileStream fs = new FileStream(FilePath, FileMode.Open);
                BinaryWriter bw = new BinaryWriter(fs);

                if (IsLoggedIn)
                {
                    bw.Write("User " + UserId.ToString() + " Was Logged In At " + DateTime.Now.ToString());

                }
                else
                {
                    bw.Write("User " + UserId.ToString() + " Was Registered At " + DateTime.Now.ToString());
                }
                               
            }
            else if (StreamType == TypeOfStream.StreamFile)
            {


            }
            else 
            {
                

            }
        }

        public static void DownLoadMemoryStreamToFile(Stream stream, string Location)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (stream is MemoryStream && stream.GetType() == typeof(MemoryStream))
            {
                MemoryStream memoryStream = (MemoryStream)stream;
                TextWriter textWriter = new StreamWriter(memoryStream);


            }
            else if (stream is FileStream && stream.GetType() == typeof(FileStream))
            {


            }

        }

    }
}