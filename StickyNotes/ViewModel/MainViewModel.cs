/*
 MIT License

Copyright (c) 2021 SantiSC

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.ComponentModel;
using System.Windows;

namespace StickyNotes
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Property Change Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public MainViewModel()
        {
            //AppConfiguration = new Configuration();
            APP = Application.Current;
        }

        //public Configuration AppConfiguration { get; set; }

        // Helper "pointer" to easily access app config and bind properties
        private Application appPointer;
        public Application APP
        {
            get => appPointer;
            set
            {
                appPointer = value;
                OnPropertyChanged("APP");
            }
        }

        // Set to true by default
        private bool wordWrap = true;
        public bool WordWrap
        {
            get => wordWrap;
            set
            {
                wordWrap = value;
                OnPropertyChanged("WordWrap");
            }
        }

        // Set to true by default
        private bool showLineNumbers = true;
        public bool ShowLineNumbers
        {
            get => showLineNumbers;
            set
            {
                showLineNumbers = value;
                OnPropertyChanged("ShowLineNumbers");
            }
        }
    }
}