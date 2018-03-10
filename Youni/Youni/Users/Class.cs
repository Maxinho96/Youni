﻿using System;
using Xamarin.Forms;

namespace Youni
{
    public class Class : BindableObject
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        private string buttonColor;
        public string ButtonColor {
            get
            {
                return this.buttonColor;
            }
            set
            {
                this.buttonColor = value;
                OnPropertyChanged("ButtonColor");
            }
        }

        public Class()
        {
        }

        public Class(string name, string shortName) : this()
        {
            this.Name = name;
            this.ShortName = shortName;
            this.ButtonColor = "#174668";
        }

        public void ChangeButtonColor()
        {
            if(this.ButtonColor == "#174668")
            {
                this.ButtonColor = "#45BFEE";
            }
            else
            {
                this.ButtonColor = "#174668";
            }
        }
    }
}
