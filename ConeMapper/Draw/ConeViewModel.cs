using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;

namespace ConeMapper
{
    public class ConeViewModel : INotifyPropertyChanged
    {
        #region Data Members

        private double x = 0;
        private double y = 0;
        private double width = 0;
        private double height = 0;
        private Color color;
        private Point connectorHotspot;

        #endregion Data Members

        public ConeViewModel()
        {
        }

        public ConeViewModel(double x, double y, double width, double height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x == value) return;
                x = value;
                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y == value) return;
                y = value;
                OnPropertyChanged("Y");
            }
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width == value) return;
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height == value) return;
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color == value) return;
                color = value;
                OnPropertyChanged("Color");
            }
        }

        public Point ConnectorHotspot
        {
            get
            {
                return connectorHotspot;
            }
            set
            {
                if (connectorHotspot == value) return;
                connectorHotspot = value;
                OnPropertyChanged("ConnectorHotspot");
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// 'PropertyChanged' event that is raised when the value of a property of the view model has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
