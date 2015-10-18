using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Controls.Primitives;

namespace ConeMapper
{
    public class ConnectorItem : ContentControl
    {
        public static readonly DependencyProperty HotspotProperty =
            DependencyProperty.Register("Hotspot", typeof(Point), typeof(ConnectorItem));
        public Point Hotspot { get { return (Point)GetValue(HotspotProperty); } set { SetValue(HotspotProperty, value); } }

        public ConnectorItem()
        {
            Focusable = false;
            this.LayoutUpdated += new EventHandler(ConnectorItem_LayoutUpdated);
            this.SizeChanged += new SizeChangedEventHandler(ConnectorItem_SizeChanged);
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
            this.Hotspot = centerRelativeToAncestor;
        }
    }
}
