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
            //new FileCrawler().ToExcel();
            //new FileCrawler().GetByDept("HIMS");

            //var client = new MongoClient();
            //var coll = client.GetDatabase("files").GetCollection<FileRecord>("files");
            //coll.UpdateMany(f => true, Builders<FileRecord>.Update.Unset("Part3").Unset("Part4").Unset("Part5").Unset("Part6"));

            //new FileCrawler().GetByDept("IIS");
            new ReportGenerator().GetLongPathNames();
        }
    }
}
