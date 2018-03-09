using System;
using System.Collections.ObjectModel;

namespace Youni
{
    public class ClassGroup : ObservableCollection<Class>
    {
        public String Key { get; set; } // FlowListView wants this to be called "Key"
        public int Year { get; set; }

        public ClassGroup()
        {
        }

        public ClassGroup(String name, int year) : this()
        {
            this.Key = name;
            this.Year = year;
        }
    }
}
