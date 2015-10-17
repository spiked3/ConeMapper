using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConeMapper
{
    public partial class MainWindow : RibbonWindow
    {
        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(MainWindow));

        public string ConeMapFileName
        {
            get { return (string)GetValue(ConeMapFileNameProperty); }
            set { SetValue(ConeMapFileNameProperty, value); TitleText = $"MiniMagellan ConeMapper - {value}"; }
        }
        public static readonly DependencyProperty ConeMapFileNameProperty =
            DependencyProperty.Register("ConeMapFileName", typeof(string), typeof(MainWindow));

        
        public DataModel DataModel
        {
            get { return (DataModel)GetValue(DataModelProperty); }
            set { SetValue(DataModelProperty, value); }
        }

        public static readonly DependencyProperty DataModelProperty =
            DependencyProperty.Register("DataModel", typeof(DataModel), typeof(MainWindow), new PropertyMetadata(new DataModel()));

        public ViewModel ViewModel
        {
            get { return (ViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ViewModel), typeof(MainWindow), new PropertyMetadata(new ViewModel()));

        public MainWindow()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Settings1.Default.LastConeMap))
            {
                DataModel = DataModel.Load(Settings1.Default.LastConeMap);
                ViewModel = new ViewModel(DataModel, canvas1);
                canvas1.Background = ViewModel.ImageBrush;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog { Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*" };
            if (d.ShowDialog() ?? false)
            {
                DataModel.ImageFileName = d.FileName;
                ViewModel.ImageBrush = new ImageBrush(new BitmapImage(new Uri(DataModel.ImageFileName)));
                canvas1.Background = ViewModel.ImageBrush;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DataModel.Save(this, ConeMapFileName);
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            ConeMapFileName = DataModel.SaveAs(this);
            Settings1.Default.LastConeMap = ConeMapFileName;
            Settings1.Default.Save();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog { Filter = "JSON Files|*.json|All Files|*.*", DefaultExt = "json" };
            if (d.ShowDialog() ?? false)
            {
                ConeMapFileName = d.FileName;
                DataModel = DataModel.Load(d.FileName);
                canvas1.Background = ViewModel.ImageBrush;
                Settings1.Default.LastConeMap = ConeMapFileName;
                Settings1.Default.Save();
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            DataModel = new DataModel();
            canvas1.Background = null;
        }

        private void Align1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Align2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Origin_Click(object sender, RoutedEventArgs e)
        {
            DataModel.OriginXp = lastRightMouseDownAt.X / canvas1.ActualWidth;
            DataModel.OriginYp = lastRightMouseDownAt.Y / canvas1.ActualHeight;
            ViewModel.OnPropertyChanged("OriginScreenPointX");
            ViewModel.OnPropertyChanged("OriginScreenPointY");
        }

        private void Cone_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OnPropertyChanged(null);
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // todo prompt save if changed
        }

        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        Point lastRightMouseDownAt = new Point(-1,-1), lastLeftMouseDownAt = new Point(-1,-1);

        private void canvas1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            lastLeftMouseDownAt = e.GetPosition(canvas1);
        }

        private void canvas1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            lastRightMouseDownAt = e.GetPosition(canvas1);
        }

        private void canvas1_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel.OnPropertyChanged(null);
        }

        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
