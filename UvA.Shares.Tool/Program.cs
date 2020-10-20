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
            new FileCrawler().GetFileNames(@"P:\test");
        }
    }
}
