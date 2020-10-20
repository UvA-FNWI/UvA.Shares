using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UvA.Shares.Tool
{
    class FileCrawler
    {
        string DeptName;

        IEnumerable<FileRecord> ProcessFolder(string folder)
        {
            Console.WriteLine($"Retrieving files in {folder}");
            var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
            int count = 0;
            foreach (var f in files)
            {
                if ((count++ % 1000) == 0)
                    Console.WriteLine($"{folder}: {count}/{files.Length}");
                var dir = Path.GetDirectoryName(f);
                long length = 0;
                DateTime? lastModified = null;
                try
                {
                    var info = new FileInfo(f);
                    length = info.Length;
                    lastModified = info.LastWriteTime;
                }
                catch { }
                yield return new FileRecord(
                    Path.GetFileName(f),
                    dir,
                    dir.Substring(folder.Length),
                    f.Length,
                    folder,
                    length,
                    (double)(length / 1024) / 1024.0 / 1024.0,
                    lastModified,
                    f.Length > 250,
                    DeptName
                );
            };
        }


        public void GetFileNames(params string[] folders)
        {
            var client = new MongoClient();
            var coll = client.GetDatabase("files").GetCollection<FileRecord>("files");

            var list = new List<FileRecord>();
            void save()
            {
                coll.InsertMany(list);
                list.Clear();
            }

            long count = 0;
            foreach (var folder in folders)
                foreach (var record in ProcessFolder(folder))
                {
                    if ((++count % 10000) == 0)
                        save();
                    list.Add(record);
                }

            save();
        }
    }
}
