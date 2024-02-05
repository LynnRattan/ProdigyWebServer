using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyServerBL.Models
{
    public class PenguinResult
    {
        public string Publisher;
        public string AuthorKey;
        public string AuthorName;
        public string Title;

        public PenguinResult(string publisher, string authorKey, string authorName, string title)
        {
            Publisher = publisher;
            AuthorKey = authorKey;
            AuthorName = authorName;
            Title = title;
        }
    }
}
