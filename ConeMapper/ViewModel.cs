using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace ConeMapper
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ConeViewModel> Cones { get { return _cones; } }

        private ObservableCollection<ConeViewModel> _cones = new ObservableCollection<ConeViewModel>();

        public ObservableCollection<ConnectionViewModel> Connections { get { return _connections; } }

        private ObservableCollection<ConnectionViewModel> _connections = new ObservableCollection<ConnectionViewModel>();

        public ImageBrush ImageBrush { get { return _ImageBrush; } set { _ImageBrush = value; OnPropertyChanged(); } }
        ImageBrush _ImageBrush = null;

        Canvas Canvas;

        public double OriginScreenPointX
        {
            get { return DataModel?.OriginXp * Canvas?.ActualWidth ?? double.NaN; }
        }

        public double OriginScreenPointY
        {
            get { return DataModel?.OriginYp * Canvas?.ActualHeight ?? double.NaN; }
        }

        public double Align1ScreenPointX
        {
            get { return DataModel?.Align1Xp * Canvas?.ActualWidth ?? double.NaN; }
        }

        public double Align1ScreenPointY
        {
            get { return DataModel?.Align1Yp * Canvas?.ActualHeight ?? double.NaN; }
        }

        public double Align2ScreenPointX
        {
            get { return DataModel?.Align2Xp * Canvas?.ActualWidth ?? double.NaN; }
        }

        public double Align2ScreenPointY
        {
            get { return DataModel?.Align2Yp * Canvas?.ActualHeight ?? double.NaN; }
        }

        DataModel DataModel;

        public ViewModel()  // for initialize only
        {
        }

        public ViewModel(DataModel d, Canvas c)
        {
            DataModel = d;
            Canvas = c;

            //
            // todo stub - Populate the view model with some example data.
            //
            var r1 = new ConeViewModel(10, 10, 40, 40, Colors.Blue);
            _cones.Add(r1);
            var r2 = new ConeViewModel(70, 70, 40, 40, Colors.Green);
            _cones.Add(r2);
            var r3 = new ConeViewModel(130, 130, 40, 40, Colors.Purple);
            _cones.Add(r3);

            //
            // Add some connections between cones.
            //
            _connections.Add(new ConnectionViewModel(r1, r2));
            _connections.Add(new ConnectionViewModel(r2, r3));

            ImageBrush = new ImageBrush(new BitmapImage(new Uri(d.ImageFileName)));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        #endregion
    }
}
