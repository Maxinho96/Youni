using System;
using System.Collections.ObjectModel;

namespace Youni
{
    public class Year : ObservableCollection<Class>
    {
        public int Code { get; set; }
        public String Key { get; set; } // FlowListView wants this to be called "Key", but it is the description

        public Year()
        {
        }

        public Year(int code, String description) : this()
        {
            this.Code = code;
            this.Key = description;
        }
    }
}
