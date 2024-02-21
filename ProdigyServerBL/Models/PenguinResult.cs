using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyServerBL.Models
{
    public class PenguinResult
    {
        public string Publisher { get; set; }
        public string AuthorKey { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }

        public PenguinResult(string publisher, string authorKey, string authorName, string title)
        {
            Publisher = publisher;
            AuthorKey = authorKey;
            AuthorName = authorName;
            Title = title;
        }
    }
}
