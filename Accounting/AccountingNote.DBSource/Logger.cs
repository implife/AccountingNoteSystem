using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AccountingNote.DBSource
{
    class Logger
    {
        public static void WriteLog(Exception ex)
        {
            string msg = $@"{DateTime.Now.ToString("yyyyy-MM-dd HH:mm:ss")}
                {ex.ToString()}
            ";

            string logPath = "C:\\Logs\\Log.log";
            string folderPath = Path.GetDirectoryName(logPath);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (File.Exists(logPath))
                File.Create(logPath);

            File.AppendAllText("C:\\Logs\\Log.log", msg);

            throw ex;
        }
    }
}
