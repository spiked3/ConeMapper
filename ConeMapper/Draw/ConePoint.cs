using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConeMapper
{

    public class ConePoint : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        #endregion INotifyPropertyChanged

        public double X { get { return _X; } set { _X = value; OnPropertyChanged(); } }
        double _X = 0;
        public double Y { get { return _Y; } set { _Y = value; OnPropertyChanged(); } }
        double _Y = 0;
        public int Sequence { get { return _Sequence; } set { _Sequence = value; OnPropertyChanged(); } }
        int _Sequence = -1; 

        public static implicit operator Point(ConePoint cp)
        {
            return new Point(cp._X, cp._Y);
        }
    }

    public class ConePointViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        #endregion INotifyPropertyChanged

        public ConePoint ConePoint { get { return _ConePoint; } set { _ConePoint = value; OnPropertyChanged(); } } private ConePoint _ConePoint;

        public double X { get { return _X; } set { _X = value; OnPropertyChanged(); } } private double _X = 0.0;

        public double Y { get { return _Y; } set { _Y = value; OnPropertyChanged(); } } private double _Y = 0.0;

        public double Width { get { return _Width; } set { _Width = value; OnPropertyChanged(); } } private double _Width = 20.0;

        public double Height { get { return _Height; } set { _Height = value; OnPropertyChanged(); } } private double _Height = 20.0;

        //public Color Color { get { return _ConePoint.isAction ? Colors.Red : Colors.Yellow; } }

        public Point ConnectorHotspot { get { return connectorHotspot; } set { if (connectorHotspot == value) { return; } connectorHotspot = value; OnPropertyChanged(); } } private Point connectorHotspot;

        public override string ToString()
        {
            return string.Format($"CP{_ConePoint.Sequence} ({_ConePoint.X}, {_ConePoint.Y})");
        }

        //public ConePointViewModel(NavigationPlanningModel navigationPlan, WayPoint wayPoint)
        //{
        //    NavigationPlan = navigationPlan;
        //    WayPoint = wayPoint;
        //    wayPoint.PropertyChanged += wp_PropertyChanged;
        //    var t = NavigationPlan.WorldPointToImage(wayPoint.Position);
        //    X = t.X;
        //    Y = t.Y;
        //}

        //private void wp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    var t = NavigationPlan.WorldPointToImage(_WayPoint.Position);
        //    _X = t.X;
        //    _Y = t.Y;
        //    OnPropertyChanged("");      // everybody may have changed
        //}
    }

    public class ConePointConnectionViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        #endregion INotifyPropertyChanged

        public ConePointViewModel MapWayPoint1 { get { return _p1; } set { _p1 = value; OnPropertyChanged(); } } private ConePointViewModel _p1;
        public ConePointViewModel MapWayPoint2 { get { return _p2; } set { _p2 = value; OnPropertyChanged(); } } private ConePointViewModel _p2;

        public ConePointConnectionViewModel(ConePointViewModel mapWayPoint1, ConePointViewModel mapWayPoint2)
        {
            MapWayPoint1 = mapWayPoint1;
            MapWayPoint2 = mapWayPoint2;
        }
    }

    public class ConePointConnectorItem : ContentControl
    {
        public static readonly DependencyProperty HotspotProperty =
            DependencyProperty.Register("Hotspot", typeof(Point), typeof(ConePointConnectorItem));

        public Point Hotspot { get { return (Point)GetValue(HotspotProperty); } set { SetValue(HotspotProperty, value); } }

        public ConePointConnectorItem()
        {
            Focusable = false;
            LayoutUpdated += new EventHandler(ConnectorItem_LayoutUpdated);
            SizeChanged += new SizeChangedEventHandler(ConnectorItem_SizeChanged);
        }

        private void ConnectorItem_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateHotspot();
        }

        private void ConnectorItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateHotspot();
        }

        private void UpdateHotspot()
        {
            var parentCanvas = this.TryFindParent<Canvas>();
            if (parentCanvas == null)
                return;
            var center = new Point(ActualWidth / 2, ActualHeight / 2);
            var centerRelativeToAncestor = TransformToAncestor(parentCanvas).Transform(center);
            Hotspot = centerRelativeToAncestor;
        }
    }
}