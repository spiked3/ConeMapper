﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ConeMapper
{
    public class DataModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        #endregion

        public double MapWidth { get { return _MapWidth; } set { _MapWidth = value; OnPropertyChanged(); } }
        double _MapWidth = 0;
        public double MapHeight { get { return _MapHeight; } set { _MapHeight = value; OnPropertyChanged(); } }
        double _MapHeight = 0;

        public double OriginXp { get { return _OriginXp; } set { _OriginXp = value; Recalc(); OnPropertyChanged(); } }
        double _OriginXp = 0;
        public double OriginYp { get { return _OriginYp; } set { _OriginYp = value; Recalc(); OnPropertyChanged(); } }
        double _OriginYp = 0;

        public double Align1Xp { get { return _Align1Xp; } set { _Align1Xp = value; OnPropertyChanged(); } }
        double _Align1Xp = 0;
        public double Align1Yp { get { return _Align1Yp; } set { _Align1Yp = value; OnPropertyChanged(); } }
        double _Align1Yp = 0;

        public double Align2Xp { get { return _Align2Xp; } set { _Align2Xp = value; OnPropertyChanged(); } }
        double _Align2Xp = 0;
        public double Align2Yp { get { return _Align2Yp; } set { _Align2Yp = value; OnPropertyChanged(); } }
        double _Align2Yp = 0;

        public int Align1X { get { return _Align1X; } set { _Align1X = value; OnPropertyChanged(); } }
        int _Align1X = 0;
        public int Align1Y { get { return _MyProperty; } set { _MyProperty = value; OnPropertyChanged(); } }
        int _MyProperty = 0;

        public int Align2X { get { return _Align2X; } set { _Align2X = value; OnPropertyChanged(); } }
        int _Align2X = 0;
        public int Align2Y { get { return _Align2Y; } set { _Align2Y = value; OnPropertyChanged(); } }
        int _Align2Y = 0; 

        public string ImageFileName { get { return _ImageFileName; } set { _ImageFileName = value; OnPropertyChanged(); } }
        string _ImageFileName;

        public event EventHandler RecalcNeeded;

        //public Point Pct2Local(Point a)
        //{
        //    return Utm2Local(Pct2Utm(a));
        //}

        void Recalc()
        {
            if (RecalcNeeded != null)
                RecalcNeeded(this, null);
        }

        internal void ResetSequenceNumbers()
        {
            //for (var i = 0; i < Waypoints.Count; i++)
            //    Waypoints[i].Sequence = i + 1;
        }

        internal string GetNavCode(float initialHeading)
        {
            StringBuilder b = new StringBuilder();
            // todo xml/JSON dump of Cone Positions
            return b.ToString();
        }

        public void Save(MainWindow owner, string fn)
        {
            if (string.IsNullOrEmpty(fn))
                SaveAs(owner);
            else
            {
                string s = JsonConvert.SerializeObject(this);
                File.WriteAllText(fn, s);
            }
        }

        public string SaveAs(MainWindow owner)
        {
            SaveFileDialog d = new SaveFileDialog { Filter = "JSON Files|*.json|All Files|*.*", DefaultExt = "json" };
            if (d.ShowDialog(owner) ?? false)
            {
                Save(owner, d.FileName);
                return d.FileName;
            }
            return null;
        }

        public static DataModel Load(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    string s = File.ReadAllText(filename);
                    DataModel d = JsonConvert.DeserializeObject<DataModel>(s);
                    if (!string.IsNullOrEmpty(d.ImageFileName))
                    {
                        var b = new BitmapImage(new Uri(d.ImageFileName));
                        d.MapWidth = b.Width;
                        d.MapHeight = b.Height;                        
                    }
                    return d;
                }
                catch (Exception)
                {
                    return new DataModel();
                }
            }
            return null;
        }
    }

    public class AlignPoint : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        #endregion

        public double Xp { get { return _Xp; } set { _Xp = value; OnPropertyChanged(); } }
        double _Xp = 0;
        public double Yp { get { return _Yp; } set { _Yp = value; OnPropertyChanged(); } }
        double _Yp = 0;
        public Point Local { get { return _Local; } set { _Local = value; OnPropertyChanged(); } }
        Point _Local = new Point(0,0); 
    }
}
