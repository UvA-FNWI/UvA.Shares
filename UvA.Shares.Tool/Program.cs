using MongoDB.Driver;
using OfficeOpenXml;
using System;

namespace UvA.Shares.Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (args.Length < 2)
            {
                Console.WriteLine("Specify an option and target folder");
                return;
            }

            switch (args[0])
            {
                case "crawl":
                    new FileCrawler().GetFileNames(args[1]);
                    break;
                case "report":
                    new ReportGenerator().Generate(args[1]);
                    break;
            }
        }
    }
}
