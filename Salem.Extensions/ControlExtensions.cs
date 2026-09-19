using System.Windows.Forms;

namespace Salem.Extensions {
    public static class ControlExtensions {
        public static bool IsVScrollVisible(this DataGridView dgv) => dgv.DisplayedRowCount(false) < dgv.RowCount;

        public static bool IsHScrollVisible(this DataGridView dgv) => dgv.DisplayedColumnCount(false) < dgv.ColumnCount;

        public static void ReverseTabsOrder(this Manina.Windows.Forms.TabControl tc) {
            foreach (Control t in tc.Controls)
                t.BringToFront();
        }
    }
}
