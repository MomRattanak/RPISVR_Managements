using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RPISVR_Managements.Setting.System_Setting
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class System_Settings : Page
    {
        public System_Settings()
        {
            this.InitializeComponent();
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTheme = (sender as ComboBox)?.SelectedItem as ComboBoxItem;

            if (selectedTheme != null)
            {
                string theme = selectedTheme.Content.ToString();

                if (theme == "Light")
                {
                    App.m_window.SetTheme(ElementTheme.Light);

                }
                else if (theme == "Dark")
                {
                    App.m_window.SetTheme(ElementTheme.Dark);
                }
            }
        }
    }
}
