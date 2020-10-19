using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using UvA.Utilities;

namespace UvA.Shares.Tool
{
    class ReportGenerator
    {
        public void GetLongPathNames()
        {
            var client = new MongoClient();
            var coll = client.GetDatabase("files").GetCollection<FileRecord>("files");
            coll.Find(f => f.TooLong && f.DeptName == "IIS").ToEnumerable().SaveAsTabSeparated(@"C:\temp\toolong.tsv");
        }
    }
}
