using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneOS
{
    /// <summary>
    /// Administración de Orientación de Pantalla [Horizontal / Vertical]
    /// </summary>
    public static class ornMgmt
    {
        public static Orientation GetOrientation()
        {
            if (flagMgmt.ForceVertical)
            {
                return Orientation.Vertical;
            }
            // If: Width>Height = Horizontal; If: Width<Height = Vertical
            var screenBounds = Screen.PrimaryScreen.Bounds;

            if (screenBounds.Width > screenBounds.Height)
            {
                return Orientation.Horizontal;
            }
            else
            {
                return Orientation.Vertical;
            }
        }
    }
}
