using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ConeMapper
{
    public partial class ResourceDictionary
    {

        public void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            ConeViewModel myRectangle = (ConeViewModel)thumb.DataContext;

            //
            // Update the the position of the rectangle in the view-model.
            //
            myRectangle.X += e.HorizontalChange;
            myRectangle.Y += e.VerticalChange;
        }
    }
}

