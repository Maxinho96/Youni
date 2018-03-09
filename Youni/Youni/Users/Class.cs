using System;
using Xamarin.Forms;

namespace Youni
{
    public class Class : BindableObject
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Year { get; set; }

        public Class()
        {
        }

        public Class(string name, string shortName, int year) : this()
        {
            this.Name = name;
            this.ShortName = shortName;
            this.Year = year;
        }
    }
}
