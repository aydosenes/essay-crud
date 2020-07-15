using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EssayBusiness.Models
{
    public class EssayModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        public string Language { get; set; }
        public IFormFile TextFile { get; set; }

    }
}
