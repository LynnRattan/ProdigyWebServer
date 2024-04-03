using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyServerBL.Models
{
    public  class PenguinResult
    {
        public string Publisher { get; set; }
        public string AuthorKey { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public bool IsStarred { get; set; } = false;

        public PenguinResult(string publisher, string authorKey, string authorName, string title, string iSBN)
        {
            Publisher = publisher;
            AuthorKey = authorKey;
            AuthorName = authorName;
            Title = title;
            ISBN = iSBN;
        }
        public PenguinResult(PenguinResult book)
        {
            Publisher = book.Publisher;
            AuthorKey = book.AuthorKey;
            AuthorName = book.AuthorName;
            Title = book.Title;
            ISBN = book.ISBN;
        }

        public PenguinResult()
        {

        }
    }
}
