using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Bankomaten
{
    internal static class Utils
    {
        public static string filePath = "C:\\Users\\nasse\\Desktop\\code\\System24\\NET1\\föreläsning-3\\Bankomaten\\data\\Users.json";
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public static double InsertAmount()
        {
            double amount;
            Console.WriteLine("Ange en summa: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("mata in korrekt siffra i korrekt format");
                return -1;
            }
            return amount;

        }

        public static void ExtractUser(User userIn, out User userOut)
        {
            userOut = userIn;
        }

    }
}
