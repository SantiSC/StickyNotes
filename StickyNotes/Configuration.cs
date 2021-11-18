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

using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace StickyNotes
{
    public class Configuration : INotifyPropertyChanged
    {
        // Property Change Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public Configuration()
        {
            // Get all available themes ordered by name and add them to the themes collection
            Themes = new ObservableCollection<Theme>(ThemeManager.Current.Themes.OrderBy(x => x.Name));
        }

        // Fields
        private bool showInTaskBar = true;  // Set to true by default
        private bool showinAltTab = true;  // Set to true by default
        private ObservableCollection<Theme> themes;
        private Theme selectedTheme;

        // Properties
        [JsonPropertyName("ShowInTaskBar")]
        public bool ShowInTaskBar
        {
            get => showInTaskBar;
            set
            {
                showInTaskBar = value;
                OnPropertyChanged("ShowInTaskBar");
            }
        }

        [JsonPropertyName("ShowInAltTab")]
        public bool ShowInAltTab
        {
            get => showinAltTab;
            set
            {
                showinAltTab = value;
                OnPropertyChanged("ShowInAltTab");
            }
        }

        [JsonIgnore]
        public ObservableCollection<Theme> Themes
        {
            get => themes;
            set
            {
                themes = value;
                OnPropertyChanged("Themes");
            }
        }

        //[JsonPropertyName("SelectedTheme")]
        [JsonIgnore]
        public Theme SelectedTheme
        {
            get => selectedTheme;
            set
            {
                selectedTheme = value;
                OnPropertyChanged("SelectedTheme");

                // If the value selected isn't null. Just to prevent possible bugs
                if (value != null)
                {
                    ThemeManager.Current.ChangeTheme(Application.Current, value.Name);
                }
                // Default theme
                else
                {
                    ThemeManager.Current.ChangeTheme(Application.Current, "Light.Blue");
                }
            }
        }

        [JsonPropertyName("SelectedThemeName")]
        private string selectedThemeName;
        public string SelectedThemeName
        {
            get => SelectedTheme != null ? SelectedTheme.Name : "Light.Blue";
            set 
            { 
                selectedThemeName = value;
                ThemeManager.Current.ChangeTheme(Application.Current, selectedThemeName);
            }
        }

        public void SaveConfig()
        {  
            string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText("Config.json", jsonString);
        }

        public static Configuration ReadConfig()
        {
            if (File.Exists("Config.json"))
            {
                string jsonContent = File.ReadAllText("Config.json");

                // If the json isn't empty
                if (jsonContent != string.Empty)
                {
                    Configuration configBuffer = JsonSerializer.Deserialize<Configuration>(jsonContent);
                    return configBuffer;
                }
                else
                {
                    return new Configuration();
                }
            }
            else
            {
                return new Configuration();
            }
        }
    }
}
