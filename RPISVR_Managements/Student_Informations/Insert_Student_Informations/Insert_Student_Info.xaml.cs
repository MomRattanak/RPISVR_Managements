using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RPISVR_Managements.Student_Informations.Insert_Student_Informations
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Insert_Student_Info : Page
    {
        public Insert_Student_Info()
        {
            this.InitializeComponent();
        }

        private void ISI_Click_Add_Student_Info(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hi");
            Debug.WriteLine("Click Add Student");

        }
    }
}
