using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Youni
{
    public class Document : BindableObject
    {
        public string DocumentTitle { get; set; }

        private int totViews;
        public int TotViews
        {
            get
            {
                return this.totViews;
            }
            set
            {
                this.totViews = value;
                OnPropertyChanged("TotViews");
            }
        }

        public Document()
        {
        }

        public Document(string documentTitle, int totViews) : this()
        {
            this.DocumentTitle = documentTitle;
            this.TotViews = totViews; 
        }
    }
}
