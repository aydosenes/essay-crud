using Essay.MongoDb;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Essay.Entities
{
    public class EssayCollection : BaseModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        public string Language { get; set; }
        public string Path { get; set; }

        public DateTime Record { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
