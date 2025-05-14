using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Tools
{
    public class LogManager
    {
        private static readonly string PATH = "Log";
        public static string getCurrentDir()
        {
            return Directory.GetCurrentDirectory();
        }
        public static string getCurrentFile()
        {
            return $@"{getCurrentDir()}\Log\{DateTime.Now.Month}\{DateTime.Now.Day}.txt";
        }
        public static void writeToLog(String projectName, String funcName, String message)
        {
            try
            {
                String directoryPath = $@"{getCurrentDir()}\Log\{DateTime.Now.Month}";
                String filePath = getCurrentFile();

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                using (StreamWriter writeLog = new StreamWriter(filePath, true))
                {
                    writeLog.WriteLine($"{DateTime.Now}\t{projectName}.{funcName}:\t{message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void clearLog()
        {
            try
            {
                DirectoryInfo[] directories = Directory.CreateDirectory($@"{getCurrentDir()}\Log").GetDirectories();
                foreach (var dir in directories)
                {
                    if (dir.Name != DateTime.Now.Month.ToString() && dir.Name != DateTime.Now.AddMonths(-1).Month.ToString())
                        dir.Delete();
                }
            }
            catch (Exception e)
            {
                LogManager.writeToLog(MethodBase.GetCurrentMethod()?.DeclaringType?.FullName!, MethodBase.GetCurrentMethod()?.Name!, e.Message);
                Console.WriteLine(e.Message);
            }

        }
    }
}
