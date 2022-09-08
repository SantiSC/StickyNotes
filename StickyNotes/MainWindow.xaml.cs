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

using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StickyNotes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set data context
            DataContext = new MainViewModel();
        }

        public MainWindow(string noteContent)
        {
            InitializeComponent();

            // Set data context
            DataContext = new MainViewModel();

            // Set editor content
            avalonEditor.Text = noteContent;
        }

        #region Hide Window from TAB
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EX_STYLE = -20;
        private const int WS_EX_APPWINDOW = 0x00040000;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        private void HideFromAltTab()
        {
            IntPtr wHandle = new WindowInteropHelper(this).Handle;
            SetWindowLong(wHandle, GWL_EX_STYLE, (GetWindowLong(wHandle, GWL_EX_STYLE) | WS_EX_TOOLWINDOW) & ~WS_EX_APPWINDOW);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!((App)System.Windows.Application.Current).Config.ShowInAltTab)
            {
                HideFromAltTab();
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newNote = new MainWindow();
            newNote.Show();
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            Topmost = (bool)PinButton.IsChecked;
        }

        private void ConfigButton_Click(object sender, RoutedEventArgs e)
        {
            // ((App)System.Windows.Application.Current).Config.SaveConfig();

            // If there's already a config window open, activate it
            if (ConfigWindow.Instance != null)
            {
                ConfigWindow.Instance.Activate();
            }
            // Otherwise, create a new one, assign it to the instance var and open it.
            else
            {
                ConfigWindow configWindow = new();
                ConfigWindow.Instance = configWindow;
                configWindow.Show();
            }
        }

        private void avalonEditor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
            {
                return;
            }

            e.Handled = true;
            if (e.Key == Key.Add)
            {
                avalonEditor.FontSize++;
            }
            else if (e.Key == Key.Subtract)
            {
                if (avalonEditor.FontSize > 1)
                {
                    avalonEditor.FontSize--;
                }
            }
        }
    }
}
