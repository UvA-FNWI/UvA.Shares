using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UvA.Shares.Tool
{
    class ReportGenerator
    {
        /// <summary>
        /// Creates an out.json file containing summary data for the specified folder
        /// </summary>
        /// <param name="folder"></param>
        public void Generate(string folder)
        {
            var client = new MongoClient(new MongoClientSettings
            {
                SocketTimeout = TimeSpan.FromHours(1),
                WaitQueueTimeout = TimeSpan.FromHours(1)
            });
            var coll = client.GetDatabase("files").GetCollection<BsonDocument>("files");

            var steps = BsonDocument.Parse(@"{ steps: [
{$addFields: {
  components: {
    $split: [
      '$Folder',
      '\\'
    ]
  }
}}, {$match: {
  ""components.2"": '<TargetFolder>'
}},{$group: {
  _id: {
    p1: '$Part1',
    p2: {
      $arrayElemAt: [
        '$components',
        2
      ]
    },
    p3: {
      $arrayElemAt: [
        '$components',
        3
      ]
    },
    p4: {
      $arrayElemAt: [
        '$components',
        4
      ]
    },
    p5: {
      $arrayElemAt: [
        '$components',
        5
      ]
    }
  },
  totalSize: {
    $sum: '$SizeGB'
  },
  fileCount: {
    $sum: 1
  },
  lastChange: {
    $max: '$LastModified'
  },
  longPathNames: {
    $sum: {
      $cond: [
        {
          $eq: [
            '$TooLong',
            true
          ]
        },
        1,
        0
      ]
    }
  }
}}]}".Replace("<TargetFolder>", folder))["steps"].AsBsonArray;
            PipelineDefinition<BsonDocument, BsonDocument> pipeline = steps.Cast<BsonDocument>().ToArray();

            var resp = new BsonArray(coll.Aggregate(pipeline).ToEnumerable());
            File.WriteAllText("out.json", resp.ToJson());
        }
    }
}
