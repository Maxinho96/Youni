using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Youni
{
    public class Document : BindableObject
    {
        public string DocumentTitle { get; set; }
        public Image DocumentImage { get; set; }
        public int DocumentUpvote { get; set; }

        public Document()
        {
        }

        public Document(string documentTitle, Image documentImage, int documentUpvote) : this()
        {
            this.DocumentTitle = documentTitle;
            this.DocumentImage = documentImage;
            this.DocumentUpvote = documentUpvote;
        }
    }
}
