using System;
namespace Youni
{
    public class Faculty
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public Faculty()
        {
        }

        public Faculty(string name, string path) : this()
        {
            this.Name = name;
            this.Path = path;
        }
    }
}
