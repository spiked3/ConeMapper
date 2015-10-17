using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConeMapper
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        #endregion

        public ConeViewModel Obj1 { get { return _Obj1; } set { _Obj1 = value; OnPropertyChanged(); } } ConeViewModel _Obj1;
        public ConeViewModel Obj2 { get { return _Obj2; } set { _Obj2 = value; OnPropertyChanged(); } } ConeViewModel _Obj2; 

        public ConnectionViewModel(ConeViewModel o1, ConeViewModel o2)
        {
            _Obj1 = o1;
            _Obj2 = o2;
        }
    }
}
