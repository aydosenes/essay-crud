using Essay.Entities;
using Essay.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace EssayData.Services.Abstract
{
    public interface IEssayMongoService : IBaseCrudRepository<EssayCollection>
    {
    }
}
