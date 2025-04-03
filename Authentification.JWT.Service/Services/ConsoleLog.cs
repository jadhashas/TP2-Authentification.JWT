using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentification.JWT.Service.Services.Interfaces;

namespace Authentification.JWT.Service.Services
{
    public class ConsoleLog : ILog
    {
        private readonly string logFile = "app.log"; // Fichier dans dossier de l’app

        public void Info(string message)
        {
            var fullMessage = $"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(fullMessage);
            Console.ResetColor();
            File.AppendAllText(logFile, fullMessage + Environment.NewLine);
        }

        public void Error(string message, Exception? ex = null)
        {
            var fullMessage = $"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}" +
                (ex != null ? $"\nException: {ex.Message}" : "");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(fullMessage);
            Console.ResetColor();
            File.AppendAllText(logFile, fullMessage + Environment.NewLine);
        }
    }
}
