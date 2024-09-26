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
using RPISVR_Managements.ViewModel;
using Windows.Storage.Pickers;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using System.Threading.Tasks;
using WinRT.Interop;
using System.ComponentModel;
using Microsoft.UI.Text;
using Microsoft.UI;



namespace RPISVR_Managements.Student_Informations.Insert_Student_Informations
{
    public sealed partial class Insert_Student_Info : Page, INotifyPropertyChanged
    {
        //ViewModel for Check validation result
        public StudentViewModel ViewModel { get; set; }

        
        public Insert_Student_Info()
        {
            this.InitializeComponent();

            //Load Focus
            //Loaded += StudentPage_Loaded;
            // Create or get an instance of StudentViewModel
            ViewModel = new StudentViewModel();
            this.DataContext = new StudentViewModel();  // Bind ViewModel to View  

            // Subscribe to ErrorMessage changes from the ViewModel
            var viewModel = (StudentViewModel)this.DataContext;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;

            
        }
        //Load Focus Stu_ID
        private void StudentPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Use DispatcherQueue to delay setting focus
            DispatcherQueue.TryEnqueue(() =>
            {
                Stu_FirstName_KH.Focus(FocusState.Programmatic);
            });
        }



        private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var viewModel = (StudentViewModel)sender;

            // Check if the changed property is ErrorMessage
            if (e.PropertyName == nameof(viewModel.ErrorMessage) && !string.IsNullOrEmpty(viewModel.ErrorMessage))
            {

                var dialog = new ContentDialog
                {
                    Title = new TextBlock
                    {
                        Text = "ការជូនដំណឹង",
                        FontSize = 18,
                        FontFamily = new FontFamily("Khmer OS Battambang"),
                        FontWeight = FontWeights.Bold,
                    },
                    CloseButtonText = "យល់ព្រម",
                    XamlRoot = this.XamlRoot,
                    RequestedTheme = ElementTheme.Default
                };
                //
                // Create the Image control and bind its Source property
                Image errorImage = new Image
                {
                    Width = 30,
                    Height = 30
                };

                // Bind the Image.Source to the ErrorImageSource in the ViewModel
                Binding imageBinding = new Binding
                {
                    Path = new PropertyPath("ErrorImageSource"),
                    Source = viewModel, // The ViewModel is the source of the binding
                    Mode = BindingMode.TwoWay
                };
                errorImage.SetBinding(Image.SourceProperty, imageBinding);

                // Create the TextBlock control and bind its Text property
                TextBlock messageTextBlock = new TextBlock
                {
                    FontSize = 12,
                    FontFamily = new FontFamily("Khmer OS Battambang"),
                    TextWrapping = TextWrapping.Wrap
                };

                // Bind the TextBlock.Text to the ErrorMessage in the ViewModel
                Binding textBinding = new Binding
                {
                    Path = new PropertyPath("ErrorMessage"),
                    Source = viewModel, // The ViewModel is the source of the binding
                    Mode = BindingMode.TwoWay
                };
                messageTextBlock.SetBinding(TextBlock.TextProperty, textBinding);

                // Create the StackPanel to hold the Image and TextBlock
                StackPanel contentStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Spacing = 10,
                    Children =
                    {
                        errorImage,   // Add the Image control to the StackPanel
                        messageTextBlock // Add the TextBlock control to the StackPanel
                    }
                };

                // Set the ContentDialog's content to the StackPanel
                dialog.Content = contentStackPanel;

                dialog.CloseButtonStyle = (Style)Application.Current.Resources["CustomDialogButtonStyle"];
                await dialog.ShowAsync();
                Debug.WriteLine(viewModel.ErrorImageSource);
            }
        }

        private void ISI_Click_Add_Student_Info(object sender, RoutedEventArgs e)
        {         

        }

        private void Stu_PhoneNumber_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            // If you want to process the value or validate it
            double value = sender.Value;

            // For example, ensure the value is an integer (phone numbers shouldn't have decimals)
            if (value != Math.Floor(value))
            {
                // Correct the value to an integer (truncate the decimal part)
                sender.Value = Math.Floor(value);
            }

            // Optionally, you can log the value for debugging purposes
            Debug.WriteLine($"Phone number entered: {sender.Value}");
        }

        // List of available job suggestions
        private List<string> _jobSuggestions = new List<string> { "ទន្លៀង", "វាលល្ងើត", "វិស្វករ", "គ្រូបង្រៀន", "ពេទ្យ" };

        
        

        
      
        public class NullToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                throw new NotImplementedException();
            }
        }

        // Helper method to get the Window handle (HWND) from a Page
        private IntPtr GetWindowHandle()
        {
            // Get the window from the App class
            Window window = App.MainAppWindow;  // Access the window stored in App.xaml.cs
            return WindowNative.GetWindowHandle(window);  // Get the window handle (HWND)
        }
        //Choose Image
        private async Task<(BitmapImage, byte[])> Choose_Image_Stu()
        {
            FileOpenPicker openPicker = new FileOpenPicker();

            // Get the window handle from the Page's parent window
            var hwnd = GetWindowHandle();  // Use the helper method to get the window handle

            // Initialize the picker with the window handle
            InitializeWithWindow.Initialize(openPicker, hwnd);

            // Set the file type filters (e.g., .jpg, .png)
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // Pick a file
            StorageFile selectedFile = await openPicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                // Open the file and return the BitmapImage
                using (var stream = await selectedFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(stream);

                    //Convert Image to byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.AsStreamForRead().CopyToAsync(memoryStream);
                        byte[] imageBytes = memoryStream.ToArray();

                        // Return both the BitmapImage and the byte array
                        return (bitmapImage, imageBytes);
                    }
                    //return bitmapImage;
                }
            }

            // Return null if no image is selected
            return (null, null);
        }
        //btn_chooseimage_stu
        private async void btn_chooseimage_stu(object sender, RoutedEventArgs e)
        {
            var (image, imageBytes) = await Choose_Image_Stu();
            if (image != null && imageBytes != null)
            {
                // Update the ViewModel with the selected image and byte array
                var viewModel = this.DataContext as StudentViewModel;
                if (viewModel != null)
                {
                    viewModel.Stu_Image_Source = image;       // Set the image for UI display
                    viewModel.ProfileImageBytes = imageBytes; // Set the byte array for storing
                }

                Stu_ShowImage.Source = image;  // Optionally, display it directly in the UI
            }

        }

        //btn_chooseimage_NationID
        private async void btn_chooseimage_nationID(object sender, RoutedEventArgs e)
        {
            var (image, imageBytes) = await Choose_Image_Stu();
            if (image != null && imageBytes != null)
            {
                // Update the ViewModel with the selected image and byte array
                var viewModel = this.DataContext as StudentViewModel;
                if (viewModel != null)
                {
                    viewModel.Stu_ImageIDNation_Source = image;       // Set the image for UI display
                    viewModel.Stu_ImageIDNation_Bytes = imageBytes; // Set the byte array for storing
                }

                Stu_ShowImageIDNation.Source = image; //Display it directly in the UI
            }
        }

        private void Test_Delete(object sender, RoutedEventArgs e)
        {
            
            
        }

        private async void btn_chooseimage_Degree(object sender, RoutedEventArgs e)
        {
            var (image, imageBytes) = await Choose_Image_Stu();
            if (image != null && imageBytes != null)
            {
                // Update the ViewModel with the selected image and byte array
                var viewModel = this.DataContext as StudentViewModel;
                if (viewModel != null)
                {
                    viewModel.Stu_Image_Degree_Source = image;       // Set the image for UI display
                    viewModel.Stu_Image_Degree_Bytes = imageBytes; // Set the byte array for storing
                   
                }

                Stu_ShowImageDegree.Source = image; //Display it directly in the UI
            }
            
                

        }

        private async void btn_chooseImage_Birth_certificate(object sender, RoutedEventArgs e)
        {
            var (image, imageBytes) = await Choose_Image_Stu();
            if (image != null && imageBytes != null)
            {
                // Update the ViewModel with the selected image and byte array
                var viewModel = this.DataContext as StudentViewModel;
                if (viewModel != null)
                {
                    viewModel.Stu_ImageBirth_Cert_Source = image;       // Set the image for UI display
                    viewModel.Stu_ImageBirth_Cert_Bytes = imageBytes; // Set the byte array for storing
                }

                Stu_ShowImage_Birth_Certifacate.Source = image; //Display it directly in the UI
            }

            
            
        }

        private async void btn_chooseImage_Poor_card(object sender, RoutedEventArgs e)
        {
            var (image, imageBytes) = await Choose_Image_Stu();
            if (image != null && imageBytes != null)
            {
                // Update the ViewModel with the selected image and byte array
                var viewModel = this.DataContext as StudentViewModel;
                if (viewModel != null)
                {
                    viewModel.Stu_ImagePoor_Card_Source = image;       // Set the image for UI display
                    viewModel.Stu_Image_Poor_Card_Bytes = imageBytes; // Set the byte array for storing
                }

                Stu_ShowImage_Poor_card.Source = image; //Display it directly in the UI
            }

           
            
        }

        private void Test_DialogFont(object sender, RoutedEventArgs e)
        {
           
        }


        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
