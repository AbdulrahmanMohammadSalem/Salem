using Salem.Controls.Internal_Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Salem.Controls {
    /// <summary>
    /// Provides static methods for displaying and removing a semi-transparent overlay panel on a Windows Form.
    /// </summary>
    /// <remarks>The overlay panel can be used to block user interaction with the underlying form, such as
    /// during loading operations or modal workflows. All members of this class are static and thread affinity with the
    /// UI thread must be maintained when calling these methods.</remarks>
    public static class SalOverlayPanel {
        public static void Show(Form parent, Color color) {
            OverlayPanelBase _internalPanel = new OverlayPanelBase(color) {
                Dock = DockStyle.Fill
            };

            parent.Controls.Add(_internalPanel);
            _internalPanel.BringToFront();
            _internalPanel.Focus();
        }

        public static void Clear(Form parent) {
            List<OverlayPanelBase> _allPanels = parent.Controls.OfType<OverlayPanelBase>().ToList();

            foreach (OverlayPanelBase P in _allPanels) {
                parent.Controls.Remove(P);
                P.Dispose();
            }
        }
    }
}
