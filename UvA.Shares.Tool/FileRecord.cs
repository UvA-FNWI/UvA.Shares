using MongoDB.Bson;
using System;

namespace UvA.Shares.Tool
{
    internal class FileRecord
    {
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
        public string Folder { get; set; }
        public string Part { get; set; }
        public int Length { get; set; }
        public string Part1 { get; set; }
        public long Size { get; set; }
        public double SizeGB { get; set; }
        public DateTime? LastModified { get; set; }
        public bool TooLong { get; set; }
        public string DeptName { get; set; }

        public FileRecord(string fileName, string folder, string part, int length, string part1, long size, double sizeGB, DateTime? lastModified, bool tooLong, string deptName)
        {
            FileName = fileName;
            Folder = folder;
            Part = part;
            Length = length;
            Part1 = part1;
            Size = size;
            SizeGB = sizeGB;
            LastModified = lastModified;
            TooLong = tooLong;
            DeptName = deptName;
        }
    }
}
