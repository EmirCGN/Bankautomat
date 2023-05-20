using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Automat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string jsonFilePath = "karteninhaber.json";
            BankAutomat bankAutomat = new BankAutomat(jsonFilePath);
            bankAutomat.Start();
        }
    }
}