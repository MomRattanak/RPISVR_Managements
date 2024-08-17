using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RPISVR_Managements.Setting.System_Setting;
using RPISVR_Managements.Student_Informations.Insert_Student_Informations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.AccessControl;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;



namespace RPISVR_Managements
{
    
    public sealed partial class MainWindow : Window
    {
        private AppWindow m_AppWindow;

        public MainWindow()
        {
            this.InitializeComponent();


            m_AppWindow = this.AppWindow;
            //Activated += MainWindow_Activated;
            AppTitleBar.SizeChanged += AppTitleBar_SizeChanged;
            AppTitleBar.Loaded += AppTitleBar_Loaded;

            ExtendsContentIntoTitleBar = true;
            if (ExtendsContentIntoTitleBar == true)
            {
                m_AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
            }
            //Set TitleBar Name as Project
            //TitleBarTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
        }
        // Method to change the theme for the entire MainWindow
        public void SetTheme(ElementTheme theme)
        {
            Application_Controls.RequestedTheme = theme;
            Main_Navigation.RequestedTheme = theme;
            AppTitleBar.RequestedTheme = theme;



        }

        

        private void AppTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (ExtendsContentIntoTitleBar == true)
            {
                // Set the initial interactive regions.
                SetRegionsForCustomTitleBar();
            }
        }

        private void AppTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ExtendsContentIntoTitleBar == true)
            {
                // Update interactive regions if the size of the window changes.
                SetRegionsForCustomTitleBar();
            }
        }
        private void SetRegionsForCustomTitleBar()
        {
            // Specify the interactive regions of the title bar.

            double scaleAdjustment = AppTitleBar.XamlRoot.RasterizationScale;

            RightPaddingColumn.Width = new GridLength(m_AppWindow.TitleBar.RightInset / scaleAdjustment);
            LeftPaddingColumn.Width = new GridLength(m_AppWindow.TitleBar.LeftInset / scaleAdjustment);

            // Get the rectangle around the AutoSuggestBox control.
            GeneralTransform transform = TitleBarSearchBox.TransformToVisual(null);
            Rect bounds = transform.TransformBounds(new Rect(0, 0,
                                                             TitleBarSearchBox.ActualWidth,
                                                             TitleBarSearchBox.ActualHeight));
            Windows.Graphics.RectInt32 SearchBoxRect = GetRect(bounds, scaleAdjustment);

            // Get the rectangle around the PersonPicture control.
            transform = PersonAccountPic.TransformToVisual(null);
            bounds = transform.TransformBounds(new Rect(0, 0,
                                                        PersonAccountPic.ActualWidth,
                                                        PersonAccountPic.ActualHeight));
            Windows.Graphics.RectInt32 PersonPicRect = GetRect(bounds, scaleAdjustment);

            var rectArray = new Windows.Graphics.RectInt32[] { SearchBoxRect, PersonPicRect };

            Microsoft.UI.Input.InputNonClientPointerSource nonClientInputSrc =
                InputNonClientPointerSource.GetForWindowId(this.AppWindow.Id);
            nonClientInputSrc.SetRegionRects(NonClientRegionKind.Passthrough, rectArray);
        }
        private Windows.Graphics.RectInt32 GetRect(Rect bounds, double scale)
        {
            return new Windows.Graphics.RectInt32(
                _X: (int)Math.Round(bounds.X * scale),
                _Y: (int)Math.Round(bounds.Y * scale),
                _Width: (int)Math.Round(bounds.Width * scale),
                _Height: (int)Math.Round(bounds.Height * scale)
            );
        }
        

        //Foreground Title
        //private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        //{
        //    if (args.WindowActivationState == WindowActivationState.Deactivated)
        //    {
        //        TitleBarTextBlock.Foreground =
        //            (SolidColorBrush)App.Current.Resources["WindowCaptionForegroundDisabled"];
        //        TitleBarIcon.Visibility = Visibility.Collapsed;
                
        //    }
        //    else
        //    {
        //        TitleBarTextBlock.Foreground =
        //            (SolidColorBrush)App.Current.Resources["WindowCaptionForeground"];
        //        TitleBarIcon.Visibility = Visibility.Visible;
        //    }
        //}

        
        

        private void MainNV_Load(object sender, RoutedEventArgs e)
        {
            //var tabViewItem = new TabViewItem();
            //tabViewItem.Header = "បញ្ចូលទិន្នន័យសិស្សនិស្សិត";
            //tabViewItem.TabIndex = 1;
            //tabViewItem.IconSource = new SymbolIconSource { Symbol = Symbol.Add };
            //var frame = new Frame();
            //frame.Navigate(typeof(Insert_Student_Info));
            //tabViewItem.Content = frame;

            //TabView.TabItems.Add(tabViewItem);
            //TabView.SelectedIndex = 1;
        }

        private void Main_NV_Items_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if(HomePage.IsSelected)
            {
                // Check if the tab is already open
                foreach (TabViewItem tab in TabView.TabItems)
                {
                    if (tab.Header.ToString() == "ទំព័រដើម")
                    {
                        // Select the existing tab
                        TabView.SelectedItem = tab;
                        return;
                    }
                }
                //// If the tab is not open, create a new one
                //var newTab = new muxc.TabViewItem
                //{
                //    Header = header,
                //    Content = new Frame { Content = Activator.CreateInstance(pageType) }
                //};

                //MyTabView.TabItems.Add(newTab);
                //MyTabView.SelectedItem = newTab;
            }
            else if(Insert_Student_Info.IsSelected)
            {
                // Check if the tab is already open
                foreach (TabViewItem tab in TabView.TabItems)
                {
                    if (tab.Content.ToString() != "បញ្ចូលទិន្នន័យសិស្សនិស្សិត")
                    {
                        
                        var tabViewItem = new TabViewItem();
                        tabViewItem.Header = "បញ្ចូលទិន្នន័យសិស្សនិស្សិត";
                        var frame = new Frame();
                        frame.Navigate(typeof(Insert_Student_Info));
                        tabViewItem.Content = frame;

                        TabView.TabItems.Add(tabViewItem);
                        TabView.SelectedIndex = 0;
                        return;

                    }
                    else if (tab.Content.ToString() == "បញ្ចូលទិន្នន័យសិស្សនិស្សិត")
                    {
                        // Select the existing tab
                        TabView.SelectedItem = tab;
                        return;

                    }
                }
            }
            else if(Setting.IsSelected)
            {
                // Check if the tab is already open
                foreach (TabViewItem tab in TabView.TabItems)
                {
                    if (tab.Header.ToString() == "ការកំណត់")
                    {
                        // Select the existing tab
                        TabView.SelectedItem = tab;
                        return;
                    }
                    else
                    {
                        var tabViewItem = new TabViewItem();
                        tabViewItem.Header = "ការកំណត់";
                        var frame = new Frame();
                        frame.Navigate(typeof(System_Settings));
                        tabViewItem.Content = frame;

                        TabView.TabItems.Add(tabViewItem);
                        TabView.SelectedIndex = 2;
                        return;
                    }
                }
            }
        }

        private void TabView_TabItemsChanged(TabView sender, IVectorChangedEventArgs args)
        {

        }

        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

       
    }

}
