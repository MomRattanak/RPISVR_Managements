using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;
using Windows.Storage;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using ABI.Microsoft.UI.Xaml;


namespace RPISVR_Managements.ViewModel
{
    public class StudentViewModel:INotifyPropertyChanged 
    {
        
        public StudentViewModel()
        {
          
            SubmitCommand = new RelayCommand(async () => await SubmitAsync());
 

            // Populate Days and Years
            for (int i = 1; i <= 31; i++) Days.Add(i); // Days 1-31
            for (int i = DateTime.Now.Year - 100; i <= DateTime.Now.Year + 50; i++) Years.Add(i); // Years from 100 years ago to 50 years in the future

            // Default to today's date
            SelectedDay = DateTime.Now.Day;
            SelectedYear = DateTime.Now.Year;
            SelectedKhmerMonth = KhmerMonths[DateTime.Now.Month - 1];
        }
        // Properties bound to the form fields

        //Color Border Error in Input Box.
        private SolidColorBrush _ErrorBorderBrush = new SolidColorBrush(Colors.Transparent); // Default transparent border

        //Real-time validation method Stu_ID
        public SolidColorBrush StuIDBorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(StuIDBorderBrush));
            }
        } 
        private void ValidateStuID()
        {
            if (string.IsNullOrEmpty(Stu_ID))
            {
                StuIDBorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                StuIDBorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        } 

        //Validation Stu_FirstName_KH
        public SolidColorBrush Stu_FirstName_KH_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_FirstName_KH_BorderBrush));
            }
        }
        private void ValidateStu_FirstName_KH()
        {
            if (string.IsNullOrEmpty(Stu_FirstName_KH))
            {
                Stu_FirstName_KH_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_FirstName_KH_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_LastName_KH
        public SolidColorBrush Stu_LastName_KH_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_LastName_KH_BorderBrush));
            }
        }
        private void ValidateStu_LastName_KH()
        {
            if (string.IsNullOrEmpty(Stu_LastName_KH))
            {
                Stu_LastName_KH_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_LastName_KH_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_FirstName_EN
        public SolidColorBrush Stu_FirstName_EN_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_FirstName_EN_BorderBrush));
            }
        }
        private void ValidateStu_FirstName_EN()
        {
            if (string.IsNullOrEmpty(Stu_FirstName_EN))
            {
                Stu_FirstName_EN_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_FirstName_EN_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_FirstName_EN
        public SolidColorBrush Stu_LastName_EN_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_LastName_EN_BorderBrush));
            }
        }
        private void ValidateStu_LastName_EN()
        {
            if (string.IsNullOrEmpty(Stu_LastName_EN))
            {
                Stu_LastName_EN_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_LastName_EN_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Levels
        public SolidColorBrush Stu_EducationLevels_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_EducationLevels_BorderBrush));
            }
        }
        private void ValidateStu_EducationLevels()
        {
            if (string.IsNullOrEmpty(Stu_EducationLevels))
            {
                Stu_EducationLevels_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_EducationLevels_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Subject
        public SolidColorBrush Stu_EducationSubjects_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_EducationSubjects_BorderBrush));
            }
        }
        private void ValidateStu_EducationSubjects()
        {
            if (string.IsNullOrEmpty(Stu_EducationSubjects))
            {
                Stu_EducationSubjects_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_EducationSubjects_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_StudyTime
        public SolidColorBrush Stu_StudyTimeShift_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_StudyTimeShift_BorderBrush));
            }
        }
        private void ValidateStu_StudyTimeShift()
        {
            if (string.IsNullOrEmpty(Stu_StudyTimeShift))
            {
                Stu_StudyTimeShift_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_StudyTimeShift_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_TypeStudy
        public SolidColorBrush Stu_TypeStudy_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_TypeStudy_BorderBrush));
            }
        }
        private void ValidateStu_Stu_TypeStudy()
        {
            if (string.IsNullOrEmpty(Stu_EducationType))
            {
                Stu_TypeStudy_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_TypeStudy_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_PhoneNumber
        public SolidColorBrush Stu_PhoneNumber_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_PhoneNumber_BorderBrush));
            }
        }
        private void ValidateStu_PhoneNumber()
        {
            if (string.IsNullOrEmpty(Stu_PhoneNumber))
            {
                Stu_PhoneNumber_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_PhoneNumber_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_NationalID
        public SolidColorBrush Stu_NationalID_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_NationalID_BorderBrush));
            }
        }
        private void ValidateStu_NationalID()
        {
            if (string.IsNullOrEmpty(Stu_NationalID))
            {
                Stu_NationalID_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_NationalID_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_StudyingTime
        public SolidColorBrush Stu_StudyingTime_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_StudyingTime_BorderBrush));
            }
        }
        private void ValidateStu_StudyingTime()
        {
            if (string.IsNullOrEmpty(Stu_StudyingTime))
            {
                Stu_StudyingTime_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_StudyingTime_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Birth_Province
        public SolidColorBrush Stu_Birth_Province_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Birth_Province_BorderBrush));
            }
        }
        private void ValidateStu_Birth_Province()
        {
            if (string.IsNullOrEmpty(Stu_Birth_Province))
            {
                Stu_Birth_Province_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Birth_Province_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Birth_Distric
        public SolidColorBrush Stu_Birth_Distric_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Birth_Distric_BorderBrush));
            }
        }
        private void ValidateStu_Birth_Distric()
        {
            if (string.IsNullOrEmpty(Stu_Birth_Distric))
            {
                Stu_Birth_Distric_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Birth_Distric_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Birth_Commune
        public SolidColorBrush Stu_Birth_Commune_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Birth_Commune_BorderBrush));
            }
        }
        private void ValidateStu_Birth_Commune()
        {
            if (string.IsNullOrEmpty(Stu_Birth_Commune))
            {
                Stu_Birth_Commune_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Birth_Commune_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Birth_Village
        public SolidColorBrush Stu_Birth_Village_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Birth_Village_BorderBrush));
            }
        }
        private void ValidateStu_Birth_Village()
        {
            if (string.IsNullOrEmpty(Stu_Birth_Village))
            {
                Stu_Birth_Village_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Birth_Village_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Live_Pro
        public SolidColorBrush Stu_Live_Pro_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Live_Pro_BorderBrush));
            }
        }
        private void ValidateStu_Live_Pro()
        {
            if (string.IsNullOrEmpty(Stu_Live_Pro))
            {
                Stu_Live_Pro_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Live_Pro_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Live_Dis
        public SolidColorBrush Stu_Live_Dis_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Live_Dis_BorderBrush));
            }
        }
        private void ValidateStu_Live_Dis()
        {
            if (string.IsNullOrEmpty(Stu_Live_Dis))
            {
                Stu_Live_Dis_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Live_Dis_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Live_Comm
        public SolidColorBrush Stu_Live_Comm_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Live_Comm_BorderBrush));
            }
        }
        private void ValidateStu_Live_Comm()
        {
            if (string.IsNullOrEmpty(Stu_Live_Comm))
            {
                Stu_Live_Comm_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Live_Comm_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Live_Vill
        public SolidColorBrush Stu_Live_Vill_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Live_Vill_BorderBrush));
            }
        }
        private void ValidateStu_Live_Vill()
        {
            if (string.IsNullOrEmpty(Stu_Live_Vill))
            {
                Stu_Live_Vill_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Live_Vill_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_StudyYear
        public SolidColorBrush Stu_StudyYear_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_StudyYear_BorderBrush));
            }
        }
        private void ValidateStu_StudyYear()
        {
            if (string.IsNullOrEmpty(Stu_StudyYear))
            {
                Stu_StudyYear_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_StudyYear_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Semester
        public SolidColorBrush Stu_Semester_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Semester_BorderBrush));
            }
        }
        private void ValidateStu_Semester()
        {
            if (string.IsNullOrEmpty(Stu_Semester))
            {
                Stu_Semester_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Semester_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Image_Total_Big
        public SolidColorBrush Stu_Image_Total_Big_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Image_Total_Big_BorderBrush));
            }
        }
        private void ValidateStu_Image_Total_Big()
        {
            if (string.IsNullOrEmpty(Stu_Image_Total_Big))
            {
                Stu_Image_Total_Big_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Image_Total_Big_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //Validation Stu_Image_TotalSmall
        public SolidColorBrush Stu_Image_TotalSmall_BorderBrush
        {
            get => _ErrorBorderBrush;
            set
            {
                _ErrorBorderBrush = value;
                OnPropertyChanged(nameof(Stu_Image_TotalSmall_BorderBrush));
            }
        }
        private void ValidateStu_Image_TotalSmall()
        {
            if (string.IsNullOrEmpty(Stu_Image_TotalSmall))
            {
                Stu_Image_TotalSmall_BorderBrush = new SolidColorBrush(Colors.Red);  // Set red border on empty
            }
            else
            {
                Stu_Image_TotalSmall_BorderBrush = new SolidColorBrush(Colors.Green); // Set green border on valid
            }
        }

        //
        //Stu_ID
        private string _Stu_ID;
        public string Stu_ID
        {
            get => _Stu_ID;
            set { 
                _Stu_ID = value;
                OnPropertyChanged();
                ValidateStuID();  // Validate in real-time as the user types
            }
            

        }

        //Stu_FirstName_KH
        private string _Stu_FirstName_KH;
        public string Stu_FirstName_KH
        {
            get => _Stu_FirstName_KH;
            set 
            { 
                _Stu_FirstName_KH = value;
                OnPropertyChanged(); 
                ValidateStu_FirstName_KH(); 
            }
        }


        //Stu_LastName_KH
        private string _Stu_LastName_KH;
        public string Stu_LastName_KH
        {
            get => _Stu_LastName_KH;
            set 
            { _Stu_LastName_KH = value;
                OnPropertyChanged();
                ValidateStu_LastName_KH();

            }
        }

        //Stu_FirstName_EN
        private string _Stu_FirstName_EN;
        public string Stu_FirstName_EN
        {
            get => _Stu_FirstName_EN;
            set 
            { 
                _Stu_FirstName_EN = value;
                OnPropertyChanged();
                ValidateStu_FirstName_EN();
            }
        }

        //Stu_LastName_EN
        private string _Stu_LastName_EN;
        public string Stu_LastName_EN
        {
            get => _Stu_LastName_EN;
            set 
            { 
                _Stu_LastName_EN = value; 
                OnPropertyChanged();
                ValidateStu_LastName_EN();
            }
        }

        //Stu_Birthday
        private DateTimeOffset? _Stu_Birthday;
        // DatePicker binding property
        public DateTimeOffset? Stu_Birthday
        {
            get => _Stu_Birthday;
            set
            {
                if (_Stu_Birthday != value)
                {
                    _Stu_Birthday = value;
                    OnPropertyChanged(nameof(Stu_Birthday)); // Notify binding update
                    OnPropertyChanged(nameof(Stu_BirthdayInKhmer)); // Update the Khmer date
                    OnPropertyChanged(nameof(Stu_BirthdayDateOnly)); // Notify that the date-only property has changed
                }
            }
        }
        // Property that returns the date in Khmer format
        public string Stu_BirthdayInKhmer
        {
            get
            {
                if (_Stu_Birthday.HasValue)
                {
                    var date = _Stu_Birthday.Value;
                    var khmerMonth = KhmerCalendarHelper.GetKhmerMonthName(date.Month);
                    return $"{date.Day} {khmerMonth} {date.Year}";
                }
                return "មិនជ្រើសរើសថ្ងៃ"; // "No date selected" in Khmer
            }
        }
        // Property that returns only the date part (DateTime) without the time
        public string Stu_BirthdayDateOnly
        {
            get => SelectedDate?.ToString("dd/MM/yyyy") ?? "No Date Selected";  // .Date returns only the date part, stripping out time and offset
        }

        //Stu_Gender
        private bool _IsMale;
        public bool IsMale
        {
            get => _IsMale;
            set 
            {
                if (_IsMale != value)
                {
                    _IsMale = value;
                    OnPropertyChanged(nameof(IsMale));
                }
            }
        }
        //String gender in Khmer
        public string Stu_Gender
        {
            get => _IsMale ? "ស្រី":"ប្រុស" ; // Return "ប្រុស" if IsMale is true, else "ស្រី"
        }

        //Stu_StateFamily
        private bool _IsSingle;
        public bool IsSingle
        {
            get => _IsSingle;
            set 
            { 
                if(_IsSingle !=value)
                {
                    _IsSingle = value;
                    OnPropertyChanged(nameof(IsSingle));
                }
            }
        }
        //String Stu_StateFamily in Khmer
        public string Stu_StateFamily
        {
            get => _IsSingle ? "មានគ្រួសារ":"នៅលីវ" ; //Return "នៅលីវ" if IsSingle is true, else "មានគ្រួសារ"
        }

        //Stu_Levels
        private string _Stu_EducationLevels;
        public string Stu_EducationLevels
        {
            get => _Stu_EducationLevels;
            set
            {
                if(_Stu_EducationLevels != value)
                {
                    _Stu_EducationLevels = value;
                    OnPropertyChanged(nameof(_Stu_EducationLevels));
                    ValidateStu_EducationLevels();
                }
            }
        }

        //Stu_Subject
        private string _Stu_EducationSubjects;
        public string Stu_EducationSubjects
        {
            get => _Stu_EducationSubjects;
            set
            {
                if(_Stu_EducationSubjects!=value)
                {
                    _Stu_EducationSubjects = value;
                    OnPropertyChanged(nameof(_Stu_EducationSubjects));
                    ValidateStu_EducationSubjects();
                }
            }
        }

        //Stu_StudyTimeShift
        private string _Stu_StudyTimeShift;
        public string Stu_StudyTimeShift
        {
            get => _Stu_StudyTimeShift;
            set
            {
                if(_Stu_StudyTimeShift != value)
                {
                    _Stu_StudyTimeShift = value;
                    OnPropertyChanged(nameof(Stu_StudyTimeShift));
                    ValidateStu_StudyTimeShift();
                }
            }
        }

        //Stu_TypeStudy
        private string _Stu_EducationType;
        public string Stu_EducationType
        {
            get => _Stu_EducationType;
            set
            {
                if(_Stu_EducationType != value)
                {
                    _Stu_EducationType = value;
                    OnPropertyChanged(nameof(Stu_EducationType));
                    ValidateStu_Stu_TypeStudy();
                }
            }
        }

        //Stu_PhoneNumber
        private string _Stu_PhoneNumber;
        public string Stu_PhoneNumber
        {
            get => _Stu_PhoneNumber;
            set
            {
                if (_Stu_PhoneNumber != value)
                {
                    _Stu_PhoneNumber = value;
                    OnPropertyChanged(nameof(Stu_PhoneNumber));
                    ValidateStu_PhoneNumber();
                }
            }
        }

        //Stu_NationalID
        private string _Stu_NationalID;
        public string Stu_NationalID
        {
            get => _Stu_NationalID;
            set
            {
                if (_Stu_NationalID != value)
                {
                    _Stu_NationalID = value;
                    OnPropertyChanged(nameof(Stu_NationalID));
                    ValidateStu_NationalID();
                }
            }
        }

        //Stu_StudyingTime
        private string _Stu_StudyingTime;
        public string Stu_StudyingTime
        {
            get => _Stu_StudyingTime;
            set
            {
                if( _Stu_StudyingTime != value)
                {
                    _Stu_StudyingTime=value;
                    OnPropertyChanged(nameof(Stu_StudyingTime));
                    ValidateStu_StudyingTime();
                }
            }
        }

        //Stu_Birth_Province
        private string _Stu_Birth_Province;
        public string Stu_Birth_Province
        {
            get => _Stu_Birth_Province;
            set
            {
                if(_Stu_Birth_Province!= value)
                {
                    _Stu_Birth_Province=value;
                    OnPropertyChanged(nameof(Stu_Birth_Province));
                    ValidateStu_Birth_Province();
                }
            }
        }

        //Stu_Birth_Distric
        private string _Stu_Birth_Distric;
        public string Stu_Birth_Distric
        {
            get => _Stu_Birth_Distric;
            set
            {
                if(_Stu_Birth_Distric!= value)
                {
                    _Stu_Birth_Distric=value;
                    OnPropertyChanged(nameof(Stu_Birth_Distric));
                    ValidateStu_Birth_Distric();
                }
            }
        }

        //Stu_Birth_Commune
        private string _Stu_Birth_Commune;
        public string Stu_Birth_Commune
        {
            get => _Stu_Birth_Commune;
            set
            {
                if(_Stu_Birth_Commune!= value)
                {
                    _Stu_Birth_Commune=value;
                    OnPropertyChanged(nameof(Stu_Birth_Commune));
                    ValidateStu_Birth_Commune();
                }
            }
        }

        //Stu_Birth_Village
        private string _Stu_Birth_Village;
        public string Stu_Birth_Village
        {
            get => _Stu_Birth_Village;
            set
            {
                if(_Stu_Birth_Village!= value)
                {
                    _Stu_Birth_Village=value;
                    OnPropertyChanged(nameof(Stu_Birth_Village));
                    ValidateStu_Birth_Village();
                }
            }
        }

        //Stu_Live_Pro
        private string _Stu_Live_Pro;
        public string Stu_Live_Pro
        {
            get => _Stu_Live_Pro;
            set
            {
                if(_Stu_Live_Pro!= value)
                {
                    _Stu_Live_Pro=value;
                    OnPropertyChanged(nameof(Stu_Live_Pro));
                    ValidateStu_Live_Pro();
                }
            }
        }

        //Stu_Live_Dis
        private string _Stu_Live_Dis;
        public string Stu_Live_Dis
        {
            get => _Stu_Live_Dis;
            set
            {
                if(_Stu_Live_Dis!= value)
                {
                    _Stu_Live_Dis=value;
                    OnPropertyChanged(nameof(Stu_Live_Dis));
                    ValidateStu_Live_Dis();
                }
            }
        }

        //Stu_Live_Comm
        private string _Stu_Live_Comm;
        public string Stu_Live_Comm
        {
            get => _Stu_Live_Comm;
            set
            {
                if(_Stu_Live_Comm!= value)
                {
                    _Stu_Live_Comm=value;
                    OnPropertyChanged(nameof(Stu_Live_Comm));
                    ValidateStu_Live_Comm();
                }
            }
        }

        //Stu_Live_Vill
        private string _Stu_Live_Vill;
        public string Stu_Live_Vill
        {
            get => _Stu_Live_Vill;
            set
            {
                if(_Stu_Live_Vill!= value)
                {
                    _Stu_Live_Vill=value;
                    OnPropertyChanged(nameof(Stu_Live_Vill));
                    ValidateStu_Live_Vill();
                }
            }
        }

        //Stu_Jobs
        private string _Stu_Jobs;
        public string Stu_Jobs
        {
            get => _Stu_Jobs;
            set
            {
                if(_Stu_Jobs!= value)
                {
                    _Stu_Jobs=value;
                    OnPropertyChanged(nameof(Stu_Jobs));
                }
            }
        }

        //Stu_School
        private string _Stu_School;
        public string Stu_School
        {
            get => _Stu_School;
            set
            {
                if (_Stu_School != value)
                {
                    _Stu_School = value;
                    OnPropertyChanged(nameof(Stu_School));
                }
            }
        }

        //Stu_StudyYear
        private string _Stu_StudyYear;
        public string Stu_StudyYear
        {
            get => _Stu_StudyYear;
            set
            {
                if (_Stu_StudyYear != value)
                {
                    _Stu_StudyYear = value;
                    OnPropertyChanged(nameof(Stu_StudyYear));
                    ValidateStu_StudyYear();
                }
            }
        }

        //Stu_Semester
        private string _Stu_Semester;
        public string Stu_Semester
        {
            get => _Stu_Semester;
            set
            {
                if (_Stu_Semester != value)
                {
                    _Stu_Semester = value;
                    OnPropertyChanged(nameof(Stu_Semester));
                    ValidateStu_Semester();
                }
            }
        }

        //Stu_Mother_Name
        private string _Stu_Mother_Name;
        public string Stu_Mother_Name
        {
            get => _Stu_Mother_Name;
            set
            {
                if (_Stu_Mother_Name != value)
                {
                    _Stu_Mother_Name = value;
                    OnPropertyChanged(nameof(Stu_Mother_Name));
                }
            }
        }

        //Stu_Mother_Phone
        private string _Stu_Mother_Phone;
        public string Stu_Mother_Phone
        {
            get => _Stu_Mother_Phone;
            set
            {
                if (_Stu_Mother_Phone != value)
                {
                    _Stu_Mother_Phone = value;
                    OnPropertyChanged(nameof(Stu_Mother_Phone));
                }
            }
        }

        //Stu_Mother_Job
        private string _Stu_Mother_Job;
        public string Stu_Mother_Job
        {
            get => _Stu_Mother_Job;
            set
            {
                if (_Stu_Mother_Job != value)
                {
                    _Stu_Mother_Job = value;
                    OnPropertyChanged(nameof(Stu_Mother_Job));
                }
            }
        }

        //Stu_Father_Name
        private string _Stu_Father_Name;
        public string Stu_Father_Name
        {
            get => _Stu_Father_Name;
            set
            {
                if (_Stu_Father_Name != value)
                {
                    _Stu_Father_Name = value;
                    OnPropertyChanged(nameof(Stu_Father_Name));
                }
            }
        }

        //Stu_Father_Phone
        private string _Stu_Father_Phone;
        public string Stu_Father_Phone
        {
            get => _Stu_Father_Phone;
            set
            {
                if (_Stu_Father_Phone != value)
                {
                    _Stu_Father_Phone = value;
                    OnPropertyChanged(nameof(Stu_Father_Phone));
                }
            }
        }

        //Stu_Image_YesNo
        private bool _IsStuImage_Yes;
        public bool IsStuImage_Yes
        {
            get => _IsStuImage_Yes;
            set
            {
                if (_IsStuImage_Yes != value)
                {
                    _IsStuImage_Yes = value;
                    OnPropertyChanged(nameof(IsStuImage_Yes));
                }
            }
        }
        //String IsImage_Yes_No in Khmer
        public string Stu_Image_YesNo
        {
            get => _IsStuImage_Yes ? "មានរូបថត" : "គ្មានរូបថត"; 
        }

        //Stu_Father_Job
        private string _Stu_Father_Job;
        public string Stu_Father_Job
        {
            get => _Stu_Father_Job;
            set
            {
                if (_Stu_Father_Job != value)
                {
                    _Stu_Father_Job = value;
                    OnPropertyChanged(nameof(Stu_Father_Job));
                }
            }
        }

        //Stu_ImageIDegree_YesNo
        private bool _Is_ImageIDegree_YesNo;
        public bool Is_ImageIDegree_YesNo
        {
            get => _Is_ImageIDegree_YesNo;
            set
            {
                if (_Is_ImageIDegree_YesNo != value)
                {
                    _Is_ImageIDegree_YesNo = value;
                    OnPropertyChanged(nameof(Is_ImageIDegree_YesNo));
                }
            }
        }
        //String Stu_ImageDegree_Yes_No in Khmer
        public string Stu_ImageIDegree_YesNo
        {
            get => _Is_ImageIDegree_YesNo ? "មានរូបថត" : "គ្មានរូបថត"; 
        }

        //Stu_ImageIDegree_YesNo
        private bool _Is_ImageIBirth_Cert_YesNo;
        public bool Is_ImageIBirth_Cert_YesNo
        {
            get => _Is_ImageIBirth_Cert_YesNo;
            set
            {
                if (_Is_ImageIBirth_Cert_YesNo != value)
                {
                    _Is_ImageIBirth_Cert_YesNo = value;
                    OnPropertyChanged(nameof(Is_ImageIBirth_Cert_YesNo));
                }
            }
        }
        //String Stu_ImageDegree_Yes_No in Khmer
        public string Stu_ImageIBirth_Cert_YesNo
        {
            get => _Is_ImageIBirth_Cert_YesNo ? "មានរូបថត" : "គ្មានរូបថត";
        }

        //Stu_ImageIDNation_YesNo
        private bool _Is_Stu_ImageIDNation_YesNo;
        public bool Is_Stu_ImageIDNation_YesNo
        {
            get => _Is_Stu_ImageIDNation_YesNo;
            set
            {
                if (_Is_Stu_ImageIDNation_YesNo != value)
                {
                    _Is_Stu_ImageIDNation_YesNo = value;
                    OnPropertyChanged(nameof(Is_Stu_ImageIDNation_YesNo));
                }
            }
        }
        //String IsImage_Yes_No in Khmer
        public string Stu_ImageIDNation_YesNo
        {
            get => _Is_Stu_ImageIDNation_YesNo ? "មានរូបថត" : "គ្មានរូបថត";
        }

        //Stu_ImageIDNation_YesNo
        private bool _Is_ImagePoor_Card_YesNo;
        public bool Is_ImagePoor_Card_YesNo
        {
            get => _Is_ImagePoor_Card_YesNo;
            set
            {
                if (_Is_ImagePoor_Card_YesNo != value)
                {
                    _Is_ImagePoor_Card_YesNo = value;
                    OnPropertyChanged(nameof(Is_ImagePoor_Card_YesNo));
                }
            }
        }
        //String IsImage_Yes_No in Khmer
        public string Stu_ImagePoor_Card_YesNo
        {
            get => _Is_ImagePoor_Card_YesNo ? "មានរូបថត" : "គ្មានរូបថត";
        }


        //Stu_Image_Total_Big
        private string _Stu_Image_Total_Big;
        public string Stu_Image_Total_Big
        {
            get => _Stu_Image_Total_Big;
            set
            {
                if (_Stu_Image_Total_Big != value)
                {
                    _Stu_Image_Total_Big = value;
                    OnPropertyChanged(nameof(Stu_Image_Total_Big));
                    ValidateStu_Image_Total_Big();
                }
            }
        }

        //Stu_Image_TotalSmall
        private string _Stu_Image_TotalSmall;
        public string Stu_Image_TotalSmall
        {
            get => _Stu_Image_TotalSmall;
            set
            {
                if (_Stu_Image_TotalSmall != value)
                {
                    _Stu_Image_TotalSmall = value;
                    OnPropertyChanged(nameof(Stu_Image_TotalSmall));
                    ValidateStu_Image_TotalSmall();
                }
            }
        }

        //Get Error Message
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
               UpdateMessageColor(); // Update the color based on the message content
            }
        }

        //Get Dynamic color to textblock
        private SolidColorBrush _messageColor;
        public SolidColorBrush MessageColor
        {
            get => _messageColor;
            set
            {
                _messageColor = value;
                OnPropertyChanged(nameof(MessageColor));
            }
        }

        //Error Message Icon

        //Database Icon
        private ImageSource _ErrorImageSource;
        public ImageSource ErrorImageSource
        {
            get => _ErrorImageSource;
            set
            {
                _ErrorImageSource = value;
                OnPropertyChanged(nameof(ErrorImageSource));
            }
        }

        // Command to handle the form submission
        public ICommand SubmitCommand { get; }

        //Update Color
        private void UpdateMessageColor()
        {
            //    // Change the message color depending on the message content
            if (ErrorMessage.Contains("ជោគជ័យ")) // Check for success keyword in Khmer
            {
                MessageColor = new SolidColorBrush(Colors.Green); // Success: Green color
            }
            else
            {
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
            }
        }

            //Focus on TextBox
            // Define an event to notify the View
        public event EventHandler RequestStuIDFocus;
        //Validation Check TextBox
        public async Task SubmitAsync()
        {
            ValidateStuID(); // Call Validate again on Submit
            ValidateStu_FirstName_KH();
            ValidateStu_LastName_KH();
            ValidateStu_FirstName_EN();
            ValidateStu_LastName_EN();
            ValidateStu_EducationLevels();
            ValidateStu_EducationSubjects();
            ValidateStu_StudyTimeShift();
            ValidateStu_PhoneNumber();
            ValidateStu_Stu_TypeStudy();
            ValidateStu_StudyingTime();
            ValidateStu_NationalID();
            ValidateStu_Birth_Province();
            ValidateStu_Birth_Distric();
            ValidateStu_Birth_Commune();
            ValidateStu_Birth_Village();
            ValidateStu_Live_Pro();
            ValidateStu_Live_Dis();
            ValidateStu_Live_Comm();
            ValidateStu_Live_Vill();
            ValidateStu_StudyYear();
            ValidateStu_Semester();
            ValidateStu_Image_Total_Big();
            ValidateStu_Image_TotalSmall();


            // Clear any previous error message
            ErrorMessage = string.Empty;
            MessageColor = null;
            // Validate Stu_ID
            if (string.IsNullOrEmpty(Stu_ID))
            {
                ErrorMessage = "ID សិស្សនិស្សិត ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red);
                return;
            }


            // Validate Stu_FirstName_KH
            if (string.IsNullOrEmpty(Stu_FirstName_KH))
            {
                ErrorMessage = "គោត្តនាម ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red);
                return;
            }

            // Validate Stu_LastName_KH
            if (string.IsNullOrEmpty(Stu_LastName_KH))
            {
                ErrorMessage = "នាម ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_FirstName_EN
            if (string.IsNullOrEmpty(Stu_FirstName_EN))
            {
                ErrorMessage = "ត្រកូល (ឡាតាំង) ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_LastName_EN
            if (string.IsNullOrEmpty(Stu_LastName_EN))
            {
                ErrorMessage = "ឈ្មោះ (ឡាតាំង) ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Levels
            if (string.IsNullOrEmpty(Stu_EducationLevels))
            {
                ErrorMessage = "កម្រិតសិក្សា ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Subject
            if (string.IsNullOrEmpty(Stu_EducationSubjects))
            {
                ErrorMessage = "ជំនាញ ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_StudyTime
            if (string.IsNullOrEmpty(Stu_EducationType))
            {
                ErrorMessage = "វេនសិក្សា ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_TypeStudy
            if (string.IsNullOrEmpty(Stu_StudyTimeShift))
            {
                ErrorMessage = "ប្រភេទសិក្សា ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_PhoneNumber
            if (string.IsNullOrEmpty(Stu_PhoneNumber))
            {
                ErrorMessage = "លេខទូរស័ព្ទផ្ទាល់ខ្លួនសិស្សនិស្សិត ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_NationalID
            if (string.IsNullOrEmpty(Stu_NationalID))
            {
                ErrorMessage = "លេខអត្តសញ្ញាណប័ណ្ណសិស្សនិស្សិត ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_StudyingTime
            if (string.IsNullOrEmpty(Stu_StudyingTime))
            {
                ErrorMessage = "ឆ្នាំទី ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Birth_Province
            if (string.IsNullOrEmpty(Stu_Birth_Province))
            {
                ErrorMessage = "ខេត្តកំណើត ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Birth_Distric
            if (string.IsNullOrEmpty(Stu_Birth_Distric))
            {
                ErrorMessage = "ស្រុក/ខណ្ឌកំណើត ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Birth_Commune
            if (string.IsNullOrEmpty(Stu_Birth_Commune))
            {
                ErrorMessage = "ឃុំ/សង្កាត់កំណើត ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Birth_Village
            if (string.IsNullOrEmpty(Stu_Birth_Village))
            {
                ErrorMessage = "ភូមិកំណើត ត្រូវតែបំពេញ !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Live_Pro
            if (string.IsNullOrEmpty(Stu_Live_Pro))
            {
                ErrorMessage = "ខេត្តរស់នៅបច្ចុប្បន្ន ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Live_Dis
            if (string.IsNullOrEmpty(Stu_Live_Dis))
            {
                ErrorMessage = "ស្រុក/ខណ្ឌរស់នៅបច្ចុប្បន្ន ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Live_Comm
            if (string.IsNullOrEmpty(Stu_Live_Comm))
            {
                ErrorMessage = "ឃុំ/សង្កាត់រស់នៅបច្ចុប្បន្ន ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_Live_Vill
            if (string.IsNullOrEmpty(Stu_Live_Vill))
            {
                ErrorMessage = "ភូមិរស់នៅបច្ចុប្បន្ន ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }

            // Validate Stu_StudyYear
            if (string.IsNullOrEmpty(Stu_StudyYear))
            {
                ErrorMessage = "ឆ្នាំសិក្សា ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }


            // Validate Stu_Semester
            if (string.IsNullOrEmpty(Stu_Semester))
            {
                ErrorMessage = "ឆមាស ត្រូវតែជ្រើសរើស !";
                ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-warning-100.png"));
                MessageColor = new SolidColorBrush(Colors.Red); // Error: Red color
                return;
            }


            Debug.WriteLine($"Stu_ID:{Stu_ID}");
            Debug.WriteLine($"Stu_FirstName_KH:{Stu_FirstName_KH}");
            Debug.WriteLine($"Stu_LastName_KH:{Stu_LastName_KH}");
            Debug.WriteLine($"First Name: {Stu_FirstName_EN}");
            Debug.WriteLine($"Last Name: {Stu_LastName_EN}");          
            Debug.WriteLine($"Stu_Date_Only: {Stu_BirthdayDateOnly}");
            Debug.WriteLine($"Gender: {Stu_Gender}");
            Debug.WriteLine($"FamelyS:{Stu_StateFamily}");
            Debug.WriteLine($"EducationLevel:{Stu_EducationLevels}");
            Debug.WriteLine($"EducationSubject:{Stu_EducationSubjects}");
            Debug.WriteLine($"StudyTimeShift:{Stu_StudyTimeShift}");
            Debug.WriteLine($"EducationType:{Stu_EducationType}");
            Debug.WriteLine($"Stu_PhoneNumber: {Stu_PhoneNumber}");
            Debug.WriteLine($"Stu_NationID:{Stu_NationalID}");
            Debug.WriteLine($"Stu_Year:{Stu_StudyingTime}");
            Debug.WriteLine($"Stu_Birth_Pro:{Stu_Birth_Province}");
            Debug.WriteLine($"Stu_Birth_Distric: {Stu_Birth_Distric}");
            Debug.WriteLine($"Stu_Birth_Commune:{Stu_Birth_Commune}");
            Debug.WriteLine($"Stu_Birth_Village:{Stu_Birth_Village}");
            Debug.WriteLine($"Stu_Live_Pro:{Stu_Live_Pro}");
            Debug.WriteLine($"Stu_Live_Dis: {Stu_Live_Dis}");
            Debug.WriteLine($"Stu_Live_Comm:{Stu_Live_Comm}");
            Debug.WriteLine($"Stu_Live_Vill:{Stu_Live_Vill}");
            Debug.WriteLine($"Stu_Jobs:{Stu_Jobs}");
            Debug.WriteLine($"Stu_School:{Stu_School}");
            Debug.WriteLine($"Stu_StudyYear:{Stu_StudyYear}");
            Debug.WriteLine($"Stu_Semester:{Stu_Semester}");
            Debug.WriteLine($"Stu_Mother_Name:{Stu_Mother_Name}");
            Debug.WriteLine($"Stu_Mother_Phone:{Stu_Mother_Phone}");
            Debug.WriteLine($"Stu_Mother_Job:{Stu_Mother_Job}");
            Debug.WriteLine($"Stu_Father_Name:{Stu_Father_Name}");
            Debug.WriteLine($"Stu_Father_Phone:{Stu_Father_Phone}");
            Debug.WriteLine($"Stu_Father_Job:{Stu_Father_Job}");
            Debug.WriteLine($"Stu_StudyYear: {Stu_StudyYear}");
            Debug.WriteLine($"Stu_Semester: {Stu_Semester}");
            Debug.WriteLine($"Stu_Image_YesNo:{Stu_Image_YesNo}");
            Debug.WriteLine($"Stu_ImageIDegree_YesNo:{Stu_ImageIDegree_YesNo}");
            Debug.WriteLine($"Stu_ImageIBirth_Cert_YesNo:{Stu_ImageIBirth_Cert_YesNo}");
            Debug.WriteLine($"Stu_ImageIDNation_YesNo:{Stu_ImageIDNation_YesNo}");
            Debug.WriteLine($"Stu_ImagePoor_Card_YesNo:{Stu_ImagePoor_Card_YesNo}");
            Debug.WriteLine($"Stu_Image_Total_Big:{Stu_Image_Total_Big}");
            Debug.WriteLine($"Stu_Image_TotalSmall:{Stu_Image_TotalSmall}");


            // If everything is valid
            ErrorMessage = Stu_ID+" បានរក្សាទុកជោគជ័យ !";
            ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Assets/icons8-check-96.png"));
            MessageColor = new SolidColorBrush(Colors.Green);
            await Task.CompletedTask;
            
        }

        

        // Example method to display error messages
        private Task ShowErrorMessageAsync(string message)
        {
            // Show an error dialog or message to the user (this is just an example)
            System.Diagnostics.Debug.WriteLine(message);
            return Task.CompletedTask;
        }

        //KH_Date
        // Khmer month names
        public List<string> KhmerMonths { get; } = new List<string>
    {
        "មករា", "កុម្ភៈ", "មីនា", "មេសា", "ឧសភា", "មិថុនា",
        "កក្កដា", "សីហា", "កញ្ញា", "តុលា", "វិច្ឆិកា", "ធ្នូ"
    };

        // Days (1-31)
        public List<int> Days { get; } = new List<int>();
        public List<int> Years { get; } = new List<int>();

        private string _selectedKhmerMonth;
        public string SelectedKhmerMonth
        {
            get => _selectedKhmerMonth;
            set
            {
                if (_selectedKhmerMonth != value)
                {
                    _selectedKhmerMonth = value;
                    OnPropertyChanged(nameof(SelectedKhmerMonth));
                    UpdateSelectedDate();
                }
            }
        }

        private int _selectedDay;
        public int SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (_selectedDay != value)
                {
                    _selectedDay = value;
                    OnPropertyChanged(nameof(SelectedDay));
                    UpdateSelectedDate();
                }
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    UpdateSelectedDate();
                }
            }
        }

        // Full selected date property
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            private set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }
        // Updates the SelectedDate property based on the selected day, month, and year
        private void UpdateSelectedDate()
        {
            if (!string.IsNullOrEmpty(SelectedKhmerMonth) && SelectedDay > 0 && SelectedYear > 0)
            {
                int month = KhmerMonths.IndexOf(SelectedKhmerMonth) + 1; // Convert Khmer month to index
                SelectedDate = new DateTime(SelectedYear, month, SelectedDay);
            }
        }
        //End
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    
}
