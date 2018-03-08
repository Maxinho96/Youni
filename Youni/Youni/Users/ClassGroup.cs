using System;
using System.Collections.ObjectModel;

namespace Youni
{
    public class ClassGroup : ObservableCollection<Class>
    {
        public String Key { get; set; } // FlowListView wants this to be called "Key"

        public ClassGroup()
        {
        }

        public ClassGroup(String name) : this()
        {
            this.Key = name;
        }
    }
}
