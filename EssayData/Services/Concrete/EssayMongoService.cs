using Essay.Entities;
using Essay.MongoDb;
using EssayData.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EssayData.Services.Concrete
{
    public class EssayMongoService : BaseCrudRepository<EssayCollection>, IEssayMongoService
    {
        public EssayMongoService(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName, collectionName)
        {

        }
    }
}
