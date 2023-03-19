//using MongoDB.Bson;
//using MongoDB.Driver;
//using System;
//using System.Linq;

//var connectionString = Environment.GetEnvironmentVariable("mongodb://Sanish:<password>@ac-tzh0cbl-shard-00-00.akei8ec.mongodb.net:27017,ac-tzh0cbl-shard-00-01.akei8ec.mongodb.net:27017,ac-tzh0cbl-shard-00-02.akei8ec.mongodb.net:27017/?ssl=true&replicaSet=atlas-9ga8vt-shard-0&authSource=admin&retryWrites=true&w=majority");
//if (connectionString == null)
//{
//    Console.WriteLine("You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable");
//    Environment.Exit(0);
//}
//var client = new MongoClient(connectionString);
//var collection = client.GetDatabase("sample_mflix").GetCollection<BsonDocument>("movies");
//var filter = Builders<BsonDocument>.Filter.Eq("title", "Back to the Future");
//var document = collection.Find(filter).First();
//Console.WriteLine(document);